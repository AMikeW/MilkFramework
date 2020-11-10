using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using XLua;

namespace StarForce
{
    /// <summary>
    /// 将C#的UIForm逻辑处理转嫁到Lua中</summary>
    /// <remarks>
    ///
    /// </remarks>
    [XLua.LuaCallCSharp]
    public class UIFormLuaWorker
    {
        public delegate void DelegateInit(LuaTable self, object userData);

        public delegate void DelegateRecycle(LuaTable self);

        public delegate void DelegateOpen(LuaTable self, object userData);

        public delegate void DelegateClose(LuaTable self, bool isShutdown, object userData);

        public delegate void DelegateUpdate(LuaTable self, float elapseSeconds, float realElapseSeconds);

        public delegate void DelegatePause(LuaTable self);

        public delegate void DelegateResume(LuaTable self);

        public delegate void DelegateCover(LuaTable self);

        public delegate void DelegateReveal(LuaTable self);

        public delegate void DelegateRefocus(LuaTable self, object userData);

        public delegate void DelegateDepthChanged(LuaTable self, int uiGroupDepth, int depthInUIGroup);

        private LuaTable _scriptEnv = null;
        private LuaTable _workerTable = null;

        private DelegateInit _luaInit = null;
        private DelegateRecycle _luaRecycle = null;
        private DelegateOpen _luaOpen = null;
        private DelegateClose _luaClose = null;
        private DelegateUpdate _luaUpdate = null;
        private DelegatePause _luaPause = null;
        private DelegateResume _luaResume = null;
        private DelegateCover _luaCover = null;
        private DelegateReveal _luaReveal = null;
        private DelegateRefocus _luaRefocus = null;
        private DelegateDepthChanged _luaDepthChanged = null;

        /// <summary>
        /// 构造函数.</summary>
        /// <param name="target"> 要转嫁的目标Procedure.</param>
        /// <param name="scriptFile"> 对应的Lua版Procedure脚本.</param>
        public UIFormLuaWorker(string scriptName)
        {
            try
            {
                _scriptEnv = GameEntry.Lua.NewTable();
                _scriptEnv.Set("self", this);

                var objs = GameEntry.Lua.DoScript(scriptName, scriptName, _scriptEnv);
                _workerTable = objs[0] as LuaTable;                

                _workerTable.Get("Init", out _luaInit);
                _workerTable.Get("Recycle", out _luaRecycle);
                _workerTable.Get("Open", out _luaOpen);
                _workerTable.Get("Close", out _luaClose);
                _workerTable.Get("Update", out _luaUpdate);
                _workerTable.Get("Pause", out _luaPause);
                _workerTable.Get("Resume", out _luaResume);
                _workerTable.Get("Cover", out _luaCover);
                _workerTable.Get("Reveal", out _luaReveal);
                _workerTable.Get("Refocus", out _luaRefocus);
                _workerTable.Get("DepthChanged", out _luaDepthChanged);
            }
            catch (SystemException e)
            {
                Log.Error("UIFormLuaWorker.Initialize failed: " + e.Message);
                Cleanup();
            }
        }

        /// <summary>
        /// 最后清理.</summary>
        public void Cleanup()
        {
            _luaInit = null;
            _luaRecycle = null;
            _luaOpen = null;
            _luaClose = null;
            _luaUpdate = null;
            _luaPause = null;
            _luaResume = null;
            _luaCover = null;
            _luaReveal = null;
            _luaRefocus = null;
            _luaDepthChanged = null;

            _workerTable = null;

            if (_scriptEnv != null)
                _scriptEnv.Dispose();
            _scriptEnv = null;
        }

        public void Init(object userData)
        {
            if (_luaInit != null)
                _luaInit(_workerTable, userData);
        }

        public void Recycle()
        {
            if (_luaRecycle != null)
                _luaRecycle(_workerTable);
        }

        public void Open(object userData)
        {
            if (_luaOpen != null)
                _luaOpen(_workerTable, userData);
        }

        public void Close(bool isShutdown, object userData)
        {
            if (_luaClose != null)
                _luaClose(_workerTable, isShutdown, userData);
        }

        public void Update(float elapseSeconds, float realElapseSeconds)
        {
            if (_luaUpdate != null)
                _luaUpdate(_workerTable, elapseSeconds, realElapseSeconds);
        }

        public void Pause()
        {
            if (_luaPause != null)
                _luaPause(_workerTable);
        }

        public void Resume()
        {
            if (_luaResume != null)
                _luaResume(_workerTable);
        }

        public void Cover()
        {
            if (_luaCover != null)
                _luaCover(_workerTable);
        }

        public void Reveal()
        {
            if (_luaReveal != null)
                _luaReveal(_workerTable);
        }

        public void Refocus(object userData)
        {
            if (_luaRefocus != null)
                _luaRefocus(_workerTable, userData);
        }

        public void DepthChanged(int uiGroupDepth, int depthInUIGroup)
        {
            if (_luaDepthChanged != null)
                _luaDepthChanged(_workerTable, uiGroupDepth, depthInUIGroup);
        }

    }
}
