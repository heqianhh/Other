using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonProxy : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, ISelectHandler, IDeselectHandler {
    public Action<PointerEventData> MouseEnterAction;
    public Action<PointerEventData> MouseExitAction;
    public Action<PointerEventData> MouseDownAction;
    public Action<PointerEventData> MouseUpAction;
    public Action<BaseEventData> MouseSelectAction;
    public Action<BaseEventData> MouseDeselectAction;
    public void OnPointerEnter(PointerEventData eventData) {
        MouseEnterAction?.Invoke(eventData);
    }
    public void OnPointerExit(PointerEventData eventData) {
        MouseExitAction?.Invoke(eventData);
    }
    public void OnPointerDown(PointerEventData eventData) {
        MouseDownAction?.Invoke(eventData);
    }
    public void OnPointerUp(PointerEventData eventData) {
        MouseUpAction?.Invoke(eventData);
    }
    public void OnSelect(BaseEventData eventData) {
        MouseSelectAction?.Invoke(eventData);
    }
    public void OnDeselect(BaseEventData eventData) {
        MouseDeselectAction?.Invoke(eventData);
    }
}
