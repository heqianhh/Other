using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRewardMgr : Singleton<AutoRewardMgr> {
    private const float MAX_SHOW_FLY_TIME = 60f;
    public bool IsShowFly {
        get; private set;
    }
    private float curShowFlyTime = 0;

    public bool IsBoost;
    public bool IsDouble;
    public void Init() {

        InitMsg();
    }
    public void Clear() {
        ClearMsg();

    }
    public void InitMsg() {

        Send.RegisterMsg(SendType.HideFlyReward, OnHideFlyReward);


    }
    public void ClearMsg() {

        Send.UnregisterMsg(SendType.HideFlyReward, OnHideFlyReward);

    }
    public void ToMax() {
        curShowFlyTime = MAX_SHOW_FLY_TIME;
    }
    private void OnHideFlyReward(object[] _objs) {
        IsShowFly = false;
    }
    public void Update() {
        // if (TutorialMgr.Instance.InTutorial()) {
        //     return;
        // }
        if (!IsShowFly) {
            curShowFlyTime += Time.deltaTime;
            if (curShowFlyTime >= MAX_SHOW_FLY_TIME) {
                curShowFlyTime = 0;
                IsShowFly = true;
                Send.SendMsg(SendType.ShowFlyReward);
            }
        }
    }

}
