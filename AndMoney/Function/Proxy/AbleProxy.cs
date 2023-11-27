using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbleProxy : MonoBehaviour
{
    public Action EnableAction;
    public Action DisableAction;

    public void OnEnable() {
        EnableAction?.Invoke();
    }
    public void OnDisable() {
        DisableAction?.Invoke();
    }
}
