using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseJoystick : BaseEventTriggerSend
{
    BasePlayerMove basePlayerMove;
    Transform In;
    Transform Out;

    public BaseJoystick(BasePlayerMove basePlayerMove, EventTrigger imgCtrl, Transform transJoystick) :base(imgCtrl) {
        this.basePlayerMove = basePlayerMove;
        In = transJoystick.GetChild(0);
        Out = In.GetChild(0);
        basePlayerMove.MouseControl = transJoystick;
        basePlayerMove.In = In.gameObject.GetComponent<RectTransform>();
        basePlayerMove.Out = Out.gameObject.GetComponent<RectTransform>();
    }
    public void InitSizeDelta(float x, float y) {
        basePlayerMove.initwidth = x;
        basePlayerMove.initheight = y;
    }
}
