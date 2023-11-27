using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InCameraViewProxy : MonoBehaviour
{
    public Action BecameVisible;
    public Action BecameInvisible;
    private void OnBecameVisible() {
        BecameVisible?.Invoke();
    }
    private void OnBecameInvisible() {
        BecameInvisible?.Invoke();
    }


   
}
