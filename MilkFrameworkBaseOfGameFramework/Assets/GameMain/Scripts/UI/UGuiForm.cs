//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace StarForce
{
    public abstract class UGuiForm : UIFormLogic
    {
        public const int DepthFactor = 100;
        private const float FadeTime = 0.3f;

        private static Font s_MainFont = null;
        private Canvas m_CachedCanvas = null;
        private CanvasGroup m_CanvasGroup = null;
        private List<Canvas> m_CachedCanvasContainer = new List<Canvas>();

        private List<Coroutine> coroutineList;

        private Coroutine closeCoroutine;
        private Coroutine openCoroutine;
        private Coroutine resumeCoroutine;

        private UIFormLuaWorker _uiFormLuaWorker;

        public int OriginalDepth
        {
            get;
            private set;
        }

        public int Depth
        {
            get
            {
                return m_CachedCanvas.sortingOrder;
            }
        }

        public void Close()
        {
            Close(false);
        }

        public void Close(bool ignoreFade)
        {
            //StopAllCoroutines();
            TryStopAllCoroutine();

            if (ignoreFade)
            {
                GameEntry.UI.CloseUIForm(this);
            }
            else
            {
                closeCoroutine = StartCoroutine(CloseCo(FadeTime));
                coroutineList.Add(closeCoroutine);
            }
        }

        public void PlayUISound(int uiSoundId)
        {
            GameEntry.Sound.PlayUISound(uiSoundId);
        }

        public static void SetMainFont(Font mainFont)
        {
            if (mainFont == null)
            {
                Log.Error("Main font is invalid.");
                return;
            }

            s_MainFont = mainFont;
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnInit(object userData)
#else
        protected internal override void OnInit(object userData)
#endif
        {
            base.OnInit(userData);

            m_CachedCanvas = gameObject.GetOrAddComponent<Canvas>();
            m_CachedCanvas.overrideSorting = true;
            OriginalDepth = m_CachedCanvas.sortingOrder;

            m_CanvasGroup = gameObject.GetOrAddComponent<CanvasGroup>();

            RectTransform transform = GetComponent<RectTransform>();
            transform.anchorMin = Vector2.zero;
            transform.anchorMax = Vector2.one;
            transform.anchoredPosition = Vector2.zero;
            transform.sizeDelta = Vector2.zero;

            gameObject.GetOrAddComponent<GraphicRaycaster>();

            Text[] texts = GetComponentsInChildren<Text>(true);
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].font = s_MainFont;
                if (!string.IsNullOrEmpty(texts[i].text))
                {
                    texts[i].text = GameEntry.Localization.GetString(texts[i].text);
                }
            }
            coroutineList = new List<Coroutine>();
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnInit(string luaRelativePath, object userData)
#else
        protected internal override void OnInit(string luaRelativePath, object userData)
#endif
        {
            base.OnInit(userData);

            m_CachedCanvas = gameObject.GetOrAddComponent<Canvas>();
            m_CachedCanvas.overrideSorting = true;
            OriginalDepth = m_CachedCanvas.sortingOrder;

            m_CanvasGroup = gameObject.GetOrAddComponent<CanvasGroup>();

            RectTransform transform = GetComponent<RectTransform>();
            transform.anchorMin = Vector2.zero;
            transform.anchorMax = Vector2.one;
            transform.anchoredPosition = Vector2.zero;
            transform.sizeDelta = Vector2.zero;

            gameObject.GetOrAddComponent<GraphicRaycaster>();

            Text[] texts = GetComponentsInChildren<Text>(true);
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].font = s_MainFont;
                if (!string.IsNullOrEmpty(texts[i].text))
                {
                    texts[i].text = GameEntry.Localization.GetString(texts[i].text);
                }
            }
            coroutineList = new List<Coroutine>();

            _uiFormLuaWorker = new UIFormLuaWorker(luaRelativePath);
            _uiFormLuaWorker.Init(userData);
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnRecycle()
#else
        protected internal override void OnRecycle()
#endif
        {
            base.OnRecycle();

            if (_uiFormLuaWorker != null)
            {
                _uiFormLuaWorker.Recycle();
            }
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnOpen(object userData)
#else
        protected internal override void OnOpen(object userData)
#endif
        {
            base.OnOpen(userData);

            m_CanvasGroup.alpha = 0f;
            //StopAllCoroutines();
            TryStopAllCoroutine();
            openCoroutine = StartCoroutine(OpenFadeCoroutine());
            coroutineList.Add(openCoroutine);

            if (_uiFormLuaWorker != null)
            {
                _uiFormLuaWorker.Open(userData);
            }
        }

        private void TryStopAllCoroutine()
        {
            foreach (var v in coroutineList)
            {
                StopCoroutine(v);
            }
            coroutineList.Clear();
        }

        IEnumerator OpenFadeCoroutine()
        {
            yield return m_CanvasGroup.FadeToAlpha(1f, FadeTime);
            coroutineList.Remove(openCoroutine);
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnClose(bool isShutdown, object userData)
#else
        protected internal override void OnClose(bool isShutdown, object userData)
#endif
        {
            base.OnClose(isShutdown, userData);

            if (_uiFormLuaWorker != null)
            {
                _uiFormLuaWorker.Close(isShutdown, userData);
            }
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnPause()
#else
        protected internal override void OnPause()
#endif
        {
            if (_uiFormLuaWorker != null)
            {
                _uiFormLuaWorker.Pause();
            }
            else
            {
                base.OnPause();
            }
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnResume()
#else
        protected internal override void OnResume()
#endif
        {
            base.OnResume();

            m_CanvasGroup.alpha = 0f;
            //StopAllCoroutines();
            TryStopAllCoroutine();
            if (this.gameObject.activeSelf && this.isActiveAndEnabled)
            {
                resumeCoroutine = StartCoroutine(ResumeFadeCoroutine());
                coroutineList.Add(resumeCoroutine);
            }

            if (_uiFormLuaWorker != null)
            {
                _uiFormLuaWorker.Resume();
            }
        }

        IEnumerator ResumeFadeCoroutine()
        {
            yield return m_CanvasGroup.FadeToAlpha(1f, FadeTime);
            coroutineList.Remove(resumeCoroutine);
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnCover()
#else
        protected internal override void OnCover()
#endif
        {
            base.OnCover();

            if (_uiFormLuaWorker != null)
            {
                _uiFormLuaWorker.Cover();
            }
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnReveal()
#else
        protected internal override void OnReveal()
#endif
        {
            base.OnReveal();

            if (_uiFormLuaWorker != null)
            {
                _uiFormLuaWorker.Reveal();
            }
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnRefocus(object userData)
#else
        protected internal override void OnRefocus(object userData)
#endif
        {
            base.OnRefocus(userData);

            if (_uiFormLuaWorker != null)
            {
                _uiFormLuaWorker.Refocus(userData);
            }
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
#else
        protected internal override void OnUpdate(float elapseSeconds, float realElapseSeconds)
#endif
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            if (_uiFormLuaWorker != null)
            {
                _uiFormLuaWorker.Update(elapseSeconds, realElapseSeconds);
            }
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnDepthChanged(int uiGroupDepth, int depthInUIGroup)
#else
        protected internal override void OnDepthChanged(int uiGroupDepth, int depthInUIGroup)
#endif
        {
            int oldDepth = Depth;
            base.OnDepthChanged(uiGroupDepth, depthInUIGroup);
            int deltaDepth = UGuiGroupHelper.DepthFactor * uiGroupDepth + DepthFactor * depthInUIGroup - oldDepth + OriginalDepth;
            GetComponentsInChildren(true, m_CachedCanvasContainer);
            for (int i = 0; i < m_CachedCanvasContainer.Count; i++)
            {
                m_CachedCanvasContainer[i].sortingOrder += deltaDepth;
            }

            m_CachedCanvasContainer.Clear();

            if (_uiFormLuaWorker != null)
            {
                _uiFormLuaWorker.DepthChanged(uiGroupDepth, depthInUIGroup);
            }
        }

        private IEnumerator CloseCo(float duration)
        {
            yield return m_CanvasGroup.FadeToAlpha(0f, duration);
            GameEntry.UI.CloseUIForm(this);
            coroutineList.Remove(closeCoroutine);
        }
    }
}
