using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AndMoney
{
    public class PhyRotateProxy : MonoBehaviour
    {

        [SerializeField]
        [Header("旋转速度")]
        public Vector3 force = new Vector3();
        private Rigidbody rigidbody;
        private void Awake()
        {
            rigidbody = gameObject.GetComponent<Rigidbody>();
        }

        private void Update()
        {
            rigidbody.angularVelocity = force;
            if (transform.localEulerAngles.x != 0 || transform.localEulerAngles.z != 0)
            {
                transform.localEulerAngles = new Vector3(0f, transform.localEulerAngles.y, 0f);
            }
        }
    }
}