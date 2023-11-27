using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADMgr : Singleton<ADMgr> {
    public void GameStart(string level) {
#if USE_SDKBUNDLE
        SDKBundle.OnGameStarted(level);
#endif
    }
    public void GameSuccess(string level, int score = 0) {
#if USE_SDKBUNDLE
        SDKBundle.GameSuccess(level, score);
#endif
    }
    public void GameFailed(string level, int score = 0) {
#if USE_SDKBUNDLE
        SDKBundle.GameFailed(level, score);
#endif
    }
    public void Event(string eventName, params object[] _objs) {
#if USE_SDKBUNDLE
        SDKBundle.TrackCustomEvent(eventName, _objs);
#endif
    }
    public void ShowInter() {
#if USE_SDKBUNDLE
        HWGames.AdManager.Instance.ShowInterstitialAD();
#endif
    }
    public void ShowRewardAD(string str, Action action) {
#if USE_SDKBUNDLE
        if (HWGames.AdManager.Instance.IsRewardADReady()) {
            HWGames.AdManager.Instance.ShowRewardAD(str, action);
        }
        else {
            HWMsgTipWindow.Instance.ShowTip("Please wait, AD not ready");
        }
#else
        action?.Invoke();
#endif
    }



}
