using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerProxy : MonoBehaviour {
    public Action<Collider> EnterAction;
    public Action<Collider> ExitAction;
    public Action<Collider> StayAction;

    private void OnTriggerEnter(Collider other) {
        EnterAction?.Invoke(other);
    }
    private void OnTriggerExit(Collider other) {
        ExitAction?.Invoke(other);
    }
    private void OnTriggerStay(Collider other) {
        StayAction?.Invoke(other);
    }

}
