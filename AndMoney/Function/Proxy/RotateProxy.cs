using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AndMoney {
    public class RotateProxy : MonoBehaviour {

        [SerializeField]
        [Header("旋转速度")]
        public Vector3 speed = new Vector3();


        private void Update() {
            transform.Rotate(new Vector3(speed.x * Time.deltaTime, speed.y * Time.deltaTime, speed.z * Time.deltaTime));
        }
    }
}