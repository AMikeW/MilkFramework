using GameFramework.ObjectPool;
using StarForce;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

public class UICommonTipForm : UGuiForm
{
    Text uiText;
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        uiText = transform.Find("TextBG/Text").GetComponent<Text>();
    }

#if UNITY_2017_3_OR_NEWER
    protected override void OnOpen(object userData)
#else
        protected internal override void OnOpen(object userData)
#endif
    {
        base.OnOpen(userData);

        string text = (string)userData;
        uiText.text = text;
        StartCoroutine(DelayCloseUI());
    }

    IEnumerator DelayCloseUI()
    {
        yield return new WaitForSeconds(0.9f);
        StarForce.GameEntry.UI.CloseUIForm(this);
        //Close(true); //传递true是直接回收到对象池，默认false是隐藏不回收
    }

#if UNITY_2017_3_OR_NEWER
    protected override void OnClose(bool isShutdown, object userData)
#else
        protected internal override void OnClose(bool isShutdown, object userData)
#endif
    {

        base.OnClose(isShutdown, userData);
    }

    protected override void OnPause()
    {
        //base.OnPause(); 不进行父类的操作（隐藏物体） 因为这样会暂停掉协程，物体是在协程里定时隐藏的。   
    }
}
