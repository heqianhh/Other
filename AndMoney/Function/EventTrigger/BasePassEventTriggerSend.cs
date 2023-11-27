using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BasePassEventTriggerSend : BaseEventTriggerSend {
    public BasePassEventTriggerSend(EventTrigger imgCtrl) : base(imgCtrl) {
    }
    bool oncePointDown = false;
    public override void OnDrag(BaseEventData arg0) {
        Send.SendMsg(SendType.CtrlDrag, arg0);

    }
    public override void OnPointUp(BaseEventData arg0) {
        if (oncePointDown) {
            oncePointDown = false;
            Send.SendMsg(SendType.CtrlUp, arg0);
            PassEvent((PointerEventData)arg0, ExecuteEvents.pointerUpHandler);
        }

    }
    public override void OnPointDown(BaseEventData arg0) {
        oncePointDown = true;
        Send.SendMsg(SendType.CtrlDown, arg0);
        PassEvent((PointerEventData)arg0, ExecuteEvents.pointerDownHandler);
    }

    public void PassEvent<T>(PointerEventData data, ExecuteEvents.EventFunction<T> function)
where T : IEventSystemHandler {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(data, results);
        GameObject current = data.pointerCurrentRaycast.gameObject;
        for (int i = 0; i < results.Count; i++) {
            if (current != results[i].gameObject) {
                ExecuteEvents.Execute(results[i].gameObject, data, function);
                return;
                //RaycastAll后ugui会自己排序，如果你只想响应透下去的最近的一个响应，这里ExecuteEvents.Execute后直接break就行。
            }
        }
        return;
    }
}
