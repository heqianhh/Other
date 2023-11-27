using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookThisProxy : MonoBehaviour
{
    public void OnEnable() {
        Send.SendMsg(SendType.CameraLookThis, transform.position);
    }
}
