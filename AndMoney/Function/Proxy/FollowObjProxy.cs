using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObjProxy : MonoBehaviour
{
    public Transform transF;
    public Vector3 posOffset;
    public Vector3 rotOffset;
    private void Update() {
        transform.position = transF.position + posOffset;
        transform.eulerAngles = rotOffset;
    }
}
