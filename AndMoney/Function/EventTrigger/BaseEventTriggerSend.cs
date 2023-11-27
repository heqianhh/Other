using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BaseEventTriggerSend
{
    public EventTrigger imgCtrl;
    Image img;
    public BaseEventTriggerSend(EventTrigger imgCtrl) {
        this.imgCtrl = imgCtrl;
        img = imgCtrl.gameObject.GetComponent<Image>();

    }
    public virtual void InitMsg() {
        imgCtrl.AddListener(EventTriggerType.Drag, OnDrag);
        imgCtrl.AddListener(EventTriggerType.PointerUp, OnPointUp);
        imgCtrl.AddListener(EventTriggerType.PointerDown, OnPointDown);
        Send.RegisterMsg(SendType.EventTriggerEnable, OnEventTriggerEnable);
    }

    public virtual void ClearMsg() {
        imgCtrl.RemoveListener(EventTriggerType.Drag, OnDrag);
        imgCtrl.RemoveListener(EventTriggerType.PointerUp, OnPointUp);
        imgCtrl.RemoveListener(EventTriggerType.PointerDown, OnPointDown);
        Send.UnregisterMsg(SendType.EventTriggerEnable, OnEventTriggerEnable);
    }
    public virtual void OnDrag(BaseEventData arg0) {
        Send.SendMsg(SendType.CtrlDrag, arg0);
    }
    public virtual void OnPointUp(BaseEventData arg0) {
        Send.SendMsg(SendType.CtrlUp, arg0);
    }
    public virtual void OnPointDown(BaseEventData arg0) {
        Send.SendMsg(SendType.CtrlDown, arg0);
    }
    public virtual void OnEventTriggerEnable(object[] objs) {
        bool enable = (bool)objs[0];
        img.raycastTarget = enable;
    }
}
