using System;
using AndMoney;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GainCoinCtrl : SingletonMonoBehavior<GainCoinCtrl> {
    Camera camera;
    float leftScreen = 0f;
    float rightScreen = 0f;
    float upScreen = 0f;
    float downScreen = 0f;
    const float offset = 0f;
    const float scaleOffset = 100f;
    protected override void Awake() {
        base.Awake();
        camera = Camera.main;
        leftScreen = ((-Screen.width / 2f) + (offset * Screen.width)) / scaleOffset;
        rightScreen = ((Screen.width / 2f) - (offset * Screen.width)) / scaleOffset;
        upScreen = ((-Screen.height / 2f) + (offset * Screen.height)) / scaleOffset;
        downScreen = ((Screen.height / 2f) - (offset * Screen.height)) / scaleOffset;
    }

    public void ShowFlyEffect(Vector3 start, Vector3 final, int count = 15, float radius = 0.75f, string iconStr = "money") {
        Transform UIParent = UIRootTwoD.Instance.systemCanvas.transform;
        start = new Vector3(Mathf.Clamp(start.x, leftScreen, rightScreen), Mathf.Clamp(start.y, upScreen, downScreen), start.z);
        final = new Vector3(Mathf.Clamp(final.x, leftScreen, rightScreen), Mathf.Clamp(final.y, upScreen, downScreen), final.z);
        for (int i = 0; i < count; i++) {
            FxCoinProxy touchCoin = ObjectPool.Instance.Get("Particle", "MoneyFx").AddMissingComponent<FxCoinProxy>();
            touchCoin.transform.localScale = Vector3.one;
            touchCoin.transform.SetParent(UIParent);
            touchCoin.Init(iconStr);
            touchCoin.Spawn(start, final, radius, 1);
        }
    }
    public void ShowFlyEffect(Vector3 start, Vector3 final, string iconStr = "money") {
        start = new Vector3(Mathf.Clamp(start.x, leftScreen, rightScreen), Mathf.Clamp(start.y, upScreen, downScreen), start.z);
        final = new Vector3(Mathf.Clamp(final.x, leftScreen, rightScreen), Mathf.Clamp(final.y, upScreen, downScreen), final.z);
        Transform UIParent = UIRootTwoD.Instance.systemCanvas.transform;
        FxCoinProxy touchCoin = ObjectPool.Instance.Get("Particle", "MoneyFx").AddMissingComponent<FxCoinProxy>();
        touchCoin.transform.localScale = Vector3.one;
        touchCoin.transform.SetParent(UIParent);
        touchCoin.Init(iconStr);
        touchCoin.Collect(start, final);

    }
}
