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
        //Close(true); //����true��ֱ�ӻ��յ�����أ�Ĭ��false�����ز�����
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
        //base.OnPause(); �����и���Ĳ������������壩 ��Ϊ��������ͣ��Э�̣���������Э���ﶨʱ���صġ�   
    }
}
