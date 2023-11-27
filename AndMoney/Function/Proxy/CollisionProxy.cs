using System;
using UnityEngine;

public class CollisionProxy : MonoBehaviour {
    public Action<Collision> EnterAction;
    public Action<Collision> ExitAction;
    public Action<Collision> StayAction;

    private void OnCollisionEnter(Collision other) {
        EnterAction?.Invoke(other);
    }
    private void OnCollisionExit(Collision other) {
        ExitAction?.Invoke(other);
    }
    private void OnCollisionStay(Collision other) {
        StayAction?.Invoke(other);
    }
}
