﻿using GameFramework;
using GameFramework.Resource;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityGameFramework.Runtime;
using XLua;

namespace StarForce
{
    [DisallowMultipleComponent]
    public class LuaComponent : GameFrameworkComponent
    {
        private readonly Dictionary<string, byte[]> m_LuaScriptCache = new Dictionary<string, byte[]>();
        private LoadAssetCallbacks m_LoadAssetCallbacks = null;

        private static LuaEnv m_LuaEnv = null;
        private LuaTable m_LuaTable = null;
        private LuaTable m_LuaMetaTable = null;

        private Action m_LuaOnStart = null;
        private Action<float, float> m_LuaOnUpdate = null;
        private Action<float, float> m_LuaOnFixedUpdate = null;
        private Action m_LuaOnDestroy = null;

        private float m_LastGCTime = 0;
        private float m_GCInterval = 1;

        public void StartVM()
        {
            if (m_LuaEnv != null)
            {
                m_LuaEnv.Dispose();
                m_LuaEnv = null;
            }

            m_LuaEnv = new LuaEnv();
            m_LuaEnv.AddLoader(CustomLuaScriptLoader);

            if (m_LuaMetaTable == null)
            {
                m_LuaMetaTable = m_LuaEnv.NewTable();
                m_LuaMetaTable.Set("__index", m_LuaEnv.Global);
            }            
        }

        /// <summary>
        /// 开始Lua逻辑!
        /// </summary>
        public void LoadAndStartLuaMain()
        {
            if (m_LuaTable == null)
            {
                m_LuaTable = NewTable();
                DoScript("LuaMain", "LuaComponent", m_LuaTable);

                m_LuaTable.Get("OnStart", out m_LuaOnStart);
                m_LuaTable.Get("OnUpdate", out m_LuaOnUpdate);
                m_LuaTable.Get("OnFixedUpdate", out m_LuaOnFixedUpdate);
                m_LuaTable.Get("OnDestroy", out m_LuaOnDestroy);
            }

            if (m_LuaOnStart != null)
            {
                m_LuaOnStart();
            }
        }

        public LuaTable NewTable()
        {
            if (m_LuaEnv == null)
            {
                Log.Error("LuaEnv is invalid.");
                return null;
            }

            if (m_LuaMetaTable == null)
            {
                Log.Error("LuaMetaTabl is invalid.");
                return null;
            }

            LuaTable luaTable = m_LuaEnv.NewTable();
            luaTable.SetMetaTable(m_LuaMetaTable);

            return luaTable;
        }

        public void LoadScript(string luaScriptName, object userData = null)
        {
            if (string.IsNullOrEmpty(luaScriptName))
            {
                Log.Warning("Lua script name is invalid.");
                return;
            }

            string luaScriptAssetName = AssetUtility.GetLuaScriptAsset(luaScriptName);
            if (GameEntry.Base.EditorResourceMode)
            {
                if (!System.IO.File.Exists(luaScriptAssetName))
                {
                    string appendErrorMessage = string.Format("Can not find lua script '{0}'.", luaScriptAssetName);
                    Debug.LogError(appendErrorMessage);
                    //GameEntry.Event.Fire(this, ReferencePool.Acquire<LoadLuaScriptFailureEventArgs>().Fill(luaScriptName, luaScriptAssetName, appendErrorMessage, userData));
                    return;
                }

                InternalLoadLuaScript(luaScriptName, luaScriptAssetName, System.IO.File.ReadAllBytes(luaScriptAssetName), 0f, userData);
            }
            else
            {
                GameEntry.Resource.LoadAsset(luaScriptAssetName, m_LoadAssetCallbacks, new LuaScriptInfo(luaScriptName, userData));
            }
        }

        public object[] DoScript(string luaScriptName, string chunkName, LuaTable env)
        {
            if (m_LuaEnv == null)
            {
                Log.Error("LuaEnv is invalid.");
                return null;
            }

            if (string.IsNullOrEmpty(luaScriptName))
            {
                Log.Error("Lua script name is invalid.");
                return null;
            }

            byte[] luaScriptBytes = null;
            if (!m_LuaScriptCache.TryGetValue(luaScriptName, out luaScriptBytes))
            {
                Log.Warning("Can not find lua script '{0}', please LoadScript first.", luaScriptName);
                return null;
            }

            return m_LuaEnv.DoString(luaScriptBytes, chunkName, env);
        }

        private void Start()
        {
            m_LoadAssetCallbacks = new LoadAssetCallbacks(LoadLuaScriptSuccessCallback, LoadLuaScriptFailureCallback);
        }

        private void Update()
        {
            if (m_LuaOnUpdate != null)
            {
                m_LuaOnUpdate(Time.deltaTime, Time.unscaledDeltaTime);
            }

            if (m_LuaEnv != null && Time.time - m_LastGCTime > m_GCInterval)
            {
                m_LuaEnv.Tick();
                m_LastGCTime = Time.time;
            }
        }

        private void FixedUpdate()
        {
            if (m_LuaOnFixedUpdate != null)
            {
                m_LuaOnFixedUpdate(Time.deltaTime, Time.unscaledDeltaTime);
            }
        }

        private void OnDestroy()
        {
            if (m_LuaOnDestroy != null)
            {
                m_LuaOnDestroy();
            }

            if (m_LuaMetaTable != null)
            {
                m_LuaMetaTable.Dispose();
                m_LuaMetaTable = null;
            }

            if (m_LuaTable != null)
            {
                m_LuaTable.Dispose();
                m_LuaTable = null;
            }

            m_LuaScriptCache.Clear();

            m_LuaOnStart = null;
            m_LuaOnUpdate = null;
            m_LuaOnFixedUpdate = null;
            m_LuaOnDestroy = null;
        }

        private byte[] CustomLuaScriptLoader(ref string luaScriptName)
        {
            byte[] luaScriptBytes = null;
            if (!m_LuaScriptCache.TryGetValue(luaScriptName, out luaScriptBytes))
            {
                Log.Warning("Can not find lua script '{0}'.", luaScriptName);
                return null;
            }

            return luaScriptBytes;
        }

        private void LoadLuaScriptSuccessCallback(string luaScriptAssetName, object asset, float duration, object userData)
        {
            TextAsset luaScriptAsset = asset as TextAsset;
            if (luaScriptAsset == null)
            {
                Log.Warning("Lua script asset '{0}' is invalid.", luaScriptAssetName);
                return;
            }

            LuaScriptInfo luaScriptInfo = (LuaScriptInfo)userData;
            byte[] bytes = luaScriptAsset.bytes;
            InternalLoadLuaScript(luaScriptInfo.LuaScriptName, luaScriptAssetName, bytes, duration, luaScriptInfo.UserData);
        }

        private void LoadLuaScriptFailureCallback(string luaScriptAssetName, LoadResourceStatus status, string errorMessage, object userData)
        {
            LuaScriptInfo luaScriptInfo = (LuaScriptInfo)userData;
            string appendErrorMessage = string.Format("Load lua script failure, asset name '{0}', status '{1}', error message '{2}'.", luaScriptAssetName, status.ToString(), errorMessage);
            GameEntry.Event.Fire(this, ReferencePool.Acquire<LoadLuaScriptFailureEventArgs>().Fill(luaScriptInfo.LuaScriptName, luaScriptAssetName, appendErrorMessage, luaScriptInfo.UserData));
        }

        private void InternalLoadLuaScript(string luaScriptName, string luaScriptAssetName, byte[] bytes, float duration, object userData)
        {
            if (bytes[0] == 239 && bytes[1] == 187 && bytes[2] == 191)
            {
                // 处理UFT-8 BOM头
                bytes[0] = bytes[1] = bytes[2] = 32;
            }

            m_LuaScriptCache[luaScriptName] = bytes;
            GameEntry.Event.Fire(this, ReferencePool.Acquire<LoadLuaScriptSuccessEventArgs>().Fill(luaScriptName, luaScriptAssetName, duration, userData));
        }

        private sealed class LuaScriptInfo
        {
            private readonly string m_LuaScriptName;
            private readonly object m_UserData;

            public LuaScriptInfo(string luaScriptName, object userData)
            {
                m_LuaScriptName = luaScriptName;
                m_UserData = userData;
            }

            public string LuaScriptName
            {
                get
                {
                    return m_LuaScriptName;
                }
            }

            public object UserData
            {
                get
                {
                    return m_UserData;
                }
            }
        }
#if UNITY_EDITOR

        /// <summary>
        /// 重新加载指定的Lua脚本,只在编辑器状态下可用
        /// </summary>
        public void LoadScriptInEditor(string luaScriptName, object userData = null)
        {
            if (GameEntry.Base.EditorResourceMode)
            {
                string luaScriptAssetName = AssetUtility.GetLuaScriptAsset(luaScriptName);
                if (!File.Exists(luaScriptAssetName))
                {
                    Log.Error("Can not find lua script '{0}'.", luaScriptAssetName);
                    return;
                }
                InternalLoadLuaScript(luaScriptName, luaScriptAssetName, File.ReadAllBytes(luaScriptAssetName), 0f, userData);
                m_LuaEnv.DoString(string.Format("package.loaded[\"{0}\"] = nil", luaScriptName), luaScriptName);
                Debug.Log(luaScriptName);
            }
        }

#endif
    }
}
