using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseEventTriggerGet {
    public Vector3 point;
    public BaseEventTriggerGet() {
        
    }
    public virtual void InitMsg() {
        Send.RegisterMsg(SendType.CtrlUp, OnCtrlUp);
        Send.RegisterMsg(SendType.CtrlDown, OnCtrlDown);
        Send.RegisterMsg(SendType.CtrlDrag, OnCtrlDrag);
    }
    public virtual void OnCtrlUp(object[] _objs) {
        PointerEventData data = (PointerEventData)_objs[0];
        point = data.position;

    }
    public virtual void OnCtrlDown(object[] _objs) {
        PointerEventData data = (PointerEventData)_objs[0];
        point = data.position;
    }
    public virtual void OnCtrlDrag(object[] _objs) {
        PointerEventData data = (PointerEventData)_objs[0];
        point = data.position;
    }
}
