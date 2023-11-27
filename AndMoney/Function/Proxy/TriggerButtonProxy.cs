using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TriggerButtonProxy : MonoBehaviour {

    public Action MouseDownAction;
    public Action MouseEnterAction;
    public Action MouseUpAction;

    private void OnMouseDown() {
        MouseDownAction?.Invoke();
    }
    private void OnMouseEnter() {
        MouseEnterAction?.Invoke();
    }
    private void OnMouseUp() {
        MouseUpAction?.Invoke();
    }
}

