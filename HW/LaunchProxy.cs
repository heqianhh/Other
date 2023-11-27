
using UnityEditor;
using UnityEngine;

namespace AndMoney {
    /// <summary>
    /// 游戏启动
    /// </summary>
    public class LaunchProxy : Singleton<LaunchProxy> {

        public void InitLogic() {
            AutoRewardMgr.Instance.Init();
        }

        public void Update() {
#if UNITY_EDITOR

            if (Input.GetKeyDown(KeyCode.V)) {
                Time.timeScale = 10f;
            }
            if (Input.GetKeyDown(KeyCode.C)) {
                Time.timeScale = 1f;
            }

            if (Input.GetKeyDown(KeyCode.S)) {
                AutoRewardMgr.Instance.ToMax();
            }

#endif
            AutoRewardMgr.Instance.Update();
        }
        private void FixedUpdate() {

        }
    }
}
