using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AndMoney {
    public class ScaleProxy : MonoBehaviour {
        public float maxScale = 1;
        public float minScale = 1;
        public float speed = 1;
        private float timer = 0;
        private bool isScale = true;
        private Vector3 local;
        bool CanPlay = true;
        // Update is called once per frame
        void Awake() {
            local = transform.localScale;
            timer = local.x;
        }
        public void Stop() {
            CanPlay = false;
        }
        public void OnPlayToEnd(int times) {
            CanPlay = true;
            float time = speed * 2f * times;
            ToolMgr.Instance.DelayCallBack(() => {
                CanPlay = false;
            }, time);
        }

        void Update() {
            if (CanPlay == true) {
                if (isScale == true) {
                    timer += Time.deltaTime * speed;
                    transform.localScale = new Vector3(timer, timer, timer);
                    if (timer >= maxScale) {
                        isScale = false;
                    }
                }
                else {
                    timer -= Time.deltaTime * speed;
                    transform.localScale = new Vector3(timer, timer, timer);
                    if (timer <= minScale) {
                        isScale = true;
                    }
                }
            }
        }
    }
}