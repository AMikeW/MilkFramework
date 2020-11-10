using System.Collections.Generic;
using XLua;

namespace GFTest.Editor
{
    public static class XLuaConfig
    {
        [CSObjectWrapEditor.GenPath]
        public static readonly string XLuaGenPath = GameFramework.Utility.Path.GetCombinePath(UnityEngine.Application.dataPath, "GameMain/Lua/XLuaGen/");

        [LuaCallCSharp]
        public static readonly List<System.Type> LuaCallCSharp = new List<System.Type>()
        {
            // 添加 NOT_GEN_WARNING 预编译以收集使用了 C# 反射的代码
            // 插件类 - 将其添加到此处，注意区分名字空间
            // Project-S - 直接在对应的类名上增加 [LuaCallCSharp]

            #region System

            typeof(System.Delegate),
            typeof(System.Object),
            typeof(System.Type),

            #endregion System

            #region UnityEngine

            typeof(UnityEngine.Application),
            typeof(UnityEngine.Behaviour),
            typeof(UnityEngine.Color),
            typeof(UnityEngine.Component),
            typeof(UnityEngine.GameObject),
            typeof(UnityEngine.MonoBehaviour),
            typeof(UnityEngine.Object),
            typeof(UnityEngine.Transform),
            typeof(UnityEngine.Vector2),
            typeof(UnityEngine.Vector3),
            typeof(UnityEngine.Vector4),
            typeof(UnityEngine.Quaternion),
            typeof(UnityEngine.Matrix4x4),

            #endregion UnityEngine

            #region GameFramework
            
            typeof(GameFramework.ReferencePool),
            typeof(GameFramework.Utility.Assembly),
            typeof(GameFramework.Utility.Converter),
            typeof(GameFramework.Utility.Json),
            typeof(GameFramework.Utility.Nullable),
            typeof(GameFramework.Utility.Path),
            //typeof(GameFramework.Utility.Profiler),
            typeof(GameFramework.Utility.Random),
            typeof(GameFramework.Utility.Text),
            typeof(GameFramework.Utility.Verifier),
            typeof(GameFramework.Utility.Zip),

            #endregion GameFramework

            #region UnityGameFramework

            typeof(UnityGameFramework.Runtime.BaseComponent),
            typeof(UnityGameFramework.Runtime.ConfigComponent),
            typeof(UnityGameFramework.Runtime.DataNodeComponent),
            typeof(UnityGameFramework.Runtime.DataTableComponent),
            typeof(UnityGameFramework.Runtime.DebuggerComponent),
            typeof(UnityGameFramework.Runtime.DownloadComponent),
            typeof(UnityGameFramework.Runtime.EntityComponent),
            typeof(UnityGameFramework.Runtime.EventComponent),
            typeof(UnityGameFramework.Runtime.FsmComponent),
            typeof(UnityGameFramework.Runtime.GameFrameworkComponent),
            typeof(UnityGameFramework.Runtime.LocalizationComponent),
            typeof(UnityGameFramework.Runtime.NetworkComponent),
            typeof(UnityGameFramework.Runtime.ObjectPoolComponent),
            typeof(UnityGameFramework.Runtime.ProcedureComponent),
            typeof(UnityGameFramework.Runtime.ResourceComponent),
            typeof(UnityGameFramework.Runtime.SceneComponent),
            typeof(UnityGameFramework.Runtime.SettingComponent),
            typeof(UnityGameFramework.Runtime.SoundComponent),
            typeof(UnityGameFramework.Runtime.UIComponent),
            typeof(UnityGameFramework.Runtime.WebRequestComponent),

            #endregion UnityGameFramework

            #region UnityGameFramework.Events

            typeof(UnityGameFramework.Runtime.CloseUIFormCompleteEventArgs),
            typeof(UnityGameFramework.Runtime.DownloadFailureEventArgs),
            typeof(UnityGameFramework.Runtime.DownloadStartEventArgs),
            typeof(UnityGameFramework.Runtime.DownloadSuccessEventArgs),
            typeof(UnityGameFramework.Runtime.DownloadUpdateEventArgs),
            typeof(UnityGameFramework.Runtime.HideEntityCompleteEventArgs),
            typeof(UnityGameFramework.Runtime.LoadConfigDependencyAssetEventArgs),
            typeof(UnityGameFramework.Runtime.LoadConfigFailureEventArgs),
            typeof(UnityGameFramework.Runtime.LoadConfigSuccessEventArgs),
            typeof(UnityGameFramework.Runtime.LoadConfigUpdateEventArgs),
            typeof(UnityGameFramework.Runtime.LoadDataTableDependencyAssetEventArgs),
            typeof(UnityGameFramework.Runtime.LoadDataTableFailureEventArgs),
            typeof(UnityGameFramework.Runtime.LoadDataTableSuccessEventArgs),
            typeof(UnityGameFramework.Runtime.LoadDataTableUpdateEventArgs),
            typeof(UnityGameFramework.Runtime.LoadDictionaryDependencyAssetEventArgs),
            typeof(UnityGameFramework.Runtime.LoadDictionaryFailureEventArgs),
            typeof(UnityGameFramework.Runtime.LoadDictionarySuccessEventArgs),
            typeof(UnityGameFramework.Runtime.LoadDictionaryUpdateEventArgs),
            typeof(UnityGameFramework.Runtime.LoadSceneDependencyAssetEventArgs),
            typeof(UnityGameFramework.Runtime.LoadSceneFailureEventArgs),
            typeof(UnityGameFramework.Runtime.LoadSceneSuccessEventArgs),
            typeof(UnityGameFramework.Runtime.LoadSceneUpdateEventArgs),
            typeof(UnityGameFramework.Runtime.NetworkClosedEventArgs),
            typeof(UnityGameFramework.Runtime.NetworkConnectedEventArgs),
            typeof(UnityGameFramework.Runtime.NetworkCustomErrorEventArgs),
            typeof(UnityGameFramework.Runtime.NetworkErrorEventArgs),
            typeof(UnityGameFramework.Runtime.NetworkMissHeartBeatEventArgs),
            typeof(UnityGameFramework.Runtime.OpenUIFormDependencyAssetEventArgs),
            typeof(UnityGameFramework.Runtime.OpenUIFormFailureEventArgs),
            typeof(UnityGameFramework.Runtime.OpenUIFormSuccessEventArgs),
            typeof(UnityGameFramework.Runtime.OpenUIFormUpdateEventArgs),
            typeof(UnityGameFramework.Runtime.PlaySoundDependencyAssetEventArgs),
            typeof(UnityGameFramework.Runtime.PlaySoundFailureEventArgs),
            typeof(UnityGameFramework.Runtime.PlaySoundSuccessEventArgs),
            typeof(UnityGameFramework.Runtime.PlaySoundUpdateEventArgs),
            //typeof(UnityGameFramework.Runtime.ResourceCheckCompleteEventArgs),
            //typeof(UnityGameFramework.Runtime.ResourceInitCompleteEventArgs),
            //typeof(UnityGameFramework.Runtime.ResourceUpdateAllCompleteEventArgs),
            typeof(UnityGameFramework.Runtime.ResourceApplyFailureEventArgs),
            typeof(UnityGameFramework.Runtime.ResourceApplySuccessEventArgs),
            typeof(UnityGameFramework.Runtime.ResourceUpdateChangedEventArgs),
            typeof(UnityGameFramework.Runtime.ResourceUpdateFailureEventArgs),
            typeof(UnityGameFramework.Runtime.ResourceUpdateStartEventArgs),
            typeof(UnityGameFramework.Runtime.ResourceUpdateSuccessEventArgs),
            typeof(UnityGameFramework.Runtime.ShowEntityDependencyAssetEventArgs),
            typeof(UnityGameFramework.Runtime.ShowEntityFailureEventArgs),
            typeof(UnityGameFramework.Runtime.ShowEntitySuccessEventArgs),
            typeof(UnityGameFramework.Runtime.ShowEntityUpdateEventArgs),
            typeof(UnityGameFramework.Runtime.UnloadSceneFailureEventArgs),
            typeof(UnityGameFramework.Runtime.UnloadSceneSuccessEventArgs),
            //typeof(UnityGameFramework.Runtime.VersionListUpdateFailureEventArgs),
            //typeof(UnityGameFramework.Runtime.VersionListUpdateSuccessEventArgs),            
            typeof(UnityGameFramework.Runtime.WebRequestFailureEventArgs),
            typeof(UnityGameFramework.Runtime.WebRequestStartEventArgs),
            typeof(UnityGameFramework.Runtime.WebRequestSuccessEventArgs),

            #endregion UnityGameFramework.Events

            #region EasyTouch

            //typeof(ETCBase),
            //typeof(ETCJoystick),

            #endregion EasyTouch
        };

        [CSharpCallLua]
        public static readonly List<System.Type> CSharpCallLua = new List<System.Type>()
        {
            typeof(System.Action),
            typeof(System.Action<object>),
            typeof(System.Action<UnityEngine.GameObject>),
            typeof(System.Action<int, int>),
            typeof(System.Action<float, float>),
            typeof(System.EventHandler<GameFramework.Event.GameEventArgs>),

            // Procedure相关
            typeof(StarForce.ProcedureLuaWorker.DelegateOnEnter),
            typeof(StarForce.ProcedureLuaWorker.DelegateOnLeave),
            typeof(StarForce.ProcedureLuaWorker.DelegateOnUpdate),
            typeof(StarForce.ProcedureLuaWorker.DelegateOnDestroy),

            //UI相关
            typeof(StarForce.UIFormLuaWorker.DelegateInit),
            typeof(StarForce.UIFormLuaWorker.DelegateRecycle),
            typeof(StarForce.UIFormLuaWorker.DelegateOpen),
            typeof(StarForce.UIFormLuaWorker.DelegateClose),
            typeof(StarForce.UIFormLuaWorker.DelegateUpdate),
            typeof(StarForce.UIFormLuaWorker.DelegatePause),
            typeof(StarForce.UIFormLuaWorker.DelegateResume),
            typeof(StarForce.UIFormLuaWorker.DelegateCover),
            typeof(StarForce.UIFormLuaWorker.DelegateReveal),
            typeof(StarForce.UIFormLuaWorker.DelegateRefocus),
            typeof(StarForce.UIFormLuaWorker.DelegateDepthChanged),
    };

        // 黑名单
        [BlackList]
        public static readonly List<List<string>> BlackList = new List<List<string>>()
        {
            // 屏蔽 Editor 专用代码，以避免打包编译错误
            new List<string>() {"UIWidget",  "showHandles"},
            new List<string>() {"UIWidget",  "showHandlesWithMoveTool"},
            new List<string>() {"UnityEngine.MonoBehaviour", "runInEditMode"},
            new List<string>() {"UnityEngine.UI.Graphic", "OnRebuildRequested"},
        };
    }
}
