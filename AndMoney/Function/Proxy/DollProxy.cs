using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollProxy : MonoBehaviour
{
    List<Rigidbody> rigidbodies = new List<Rigidbody>();
    List<Transform> trans = new List<Transform>();
    List<Vector3> localPos = new List<Vector3>();
    List<Vector3> localRot = new List<Vector3>();

    public void Awake() {
        var rigis = gameObject.GetComponentsInChildren<Rigidbody>();
        foreach (var temp in rigis) {
            rigidbodies.Add(temp);
            trans.Add(temp.transform);
            localPos.Add(temp.transform.localPosition);
            localRot.Add(temp.transform.localEulerAngles);
        }

    }
    public void OnEnable() {
        Reset();
    }
    private void Reset() {
        
        foreach(Rigidbody temp in rigidbodies) {
            temp.velocity = Vector3.zero;
            temp.angularVelocity = Vector3.zero;
        }
        for (int i = 0; i < trans.Count; i++) {
            trans[i].localPosition = localPos[i];
            trans[i].localEulerAngles = localRot[i];
        }
    }
    public void AddForce(Vector3 force) {
        foreach (Rigidbody temp in rigidbodies) {
            temp.velocity = force;
        }
    }
}
