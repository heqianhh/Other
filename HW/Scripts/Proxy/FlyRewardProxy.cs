using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class FlyRewardProxy {

    private const float ANIM_TIME = 4f;
    private const float WAIT_TIME = 3f;
    private const float Z_POS = 99f;
    private const float REWARD_DISAPPEAR_TIME = 30f;

    private GameObject ItemGO { get; }
    private Transform Trans { get; }

    private int dir = 1;
    private float startTime;
    private Button btnReward;
    private Vector3 spawnPos = new Vector3(4, 0, Z_POS);
    private Vector3 repePos = new Vector3(2f, 3, Z_POS);
    private Vector3 offsetPos = new Vector3(0, -1, 0);
    private Tweener moveTW;
    private Tweener repeTW;
    private bool isRecycle = false;
    private Coroutine coDelay;
    public double reward;
    private ADRewardType curRewardType = ADRewardType.Coin;
    private List<Transform> rewardModelList = new List<Transform>();
    private List<Vector3> posList = new List<Vector3>() { new Vector3(2, 3, Z_POS), new Vector3(0, 0, 100), new Vector3(2, -3, Z_POS) };

    public FlyRewardProxy(GameObject _itemGO) {
        ItemGO = _itemGO;
        Trans = ItemGO.transform;
        btnReward = ItemGO.GetChildControl<Button>("Button");
        foreach (Transform child in ItemGO.GetChildControl<Transform>("Button/fly")) {
            rewardModelList.Add(child);
        }
        //Æ«²î×ø±ê
        spawnPos += offsetPos;
        repePos += offsetPos;
        for (int i = 0; i < posList.Count; i++) {
            posList[i] += offsetPos;
        }
        ItemGO.SetActive(false);
    }

    public void OnOpen() {
        ItemGO.SetActive(false);
        btnReward.onClick.AddListener(OnRewardClick);
        Send.RegisterMsg(SendType.ShowFlyReward, OnShowFlyReward);
    }

    public void OnClose() {
        ItemGO.SetActive(false);
        btnReward.onClick.RemoveListener(OnRewardClick);
        Send.UnregisterMsg(SendType.ShowFlyReward, OnShowFlyReward);
    }

    private void OnShowFlyReward(object[] _objs) {
        isRecycle = false;
        Show();
    }

    private void OnRewardClick() {
        Hide();
        ClickFunc(curRewardType);
    }
    public virtual void ClickFunc(ADRewardType flyRewardProxy) {
        switch (curRewardType) {
            case ADRewardType.Coin:
                //IncomeWindow.Instance.money = reward;
                //WindowMgr.Instance.OpenWindow<IncomeWindow>();
                break;
            case ADRewardType.Boost:
                //WindowMgr.Instance.OpenWindow<SpeedWindow>();
                break;
            case ADRewardType.DoubleIncome:
                //WindowMgr.Instance.OpenWindow<BoostWindow>();
                break;
        }
    }

    public void Update() {
        if (isRecycle) {//!ItemGO.activeSelf || 
            return;
        }
        if (Time.time - startTime >= REWARD_DISAPPEAR_TIME) {
            isRecycle = true;
            Recycle();
        }
    }
    public virtual double GetSecondIncome() {
        return 1f;//FactoryMgr.Instance.FactoryIncome() * 0.5f;//CulMgr.Instance.GetAllIncome() * (UpgradeMgr.Instance.GetProfit(ProfitType.FreeCash) + 1f);
    }

    private void Show() {
        Reset();
        double income = GetSecondIncome();
        if (income <= 0) {
            Send.SendMsg(SendType.HideFlyReward);
            return;
        }

        List<ADRewardType> list = new List<ADRewardType>() { ADRewardType.Coin, ADRewardType.Boost, ADRewardType.DoubleIncome };
        if (AutoRewardMgr.Instance.IsBoost) {
            list.Remove(ADRewardType.Boost);
        }
        if (AutoRewardMgr.Instance.IsDouble) {
            list.Remove(ADRewardType.DoubleIncome);
        }
        if (list.Count == 0) {
            Send.SendMsg(SendType.HideFlyReward);
            return;
        }
        curRewardType = list[Random.Range(0, list.Count)];

        string modelName = "";
        switch (curRewardType) {
            case ADRewardType.Coin:
                float rewardTime = 180f;
                reward = income * rewardTime;
                modelName = "Bonus_3";
                break;
            case ADRewardType.Boost:
                modelName = "Bonus_2";
                break;
            case ADRewardType.DoubleIncome:
                modelName = "Bonus_1";
                break;
        }
        for (int i = 0; i < rewardModelList.Count; i++) {
            rewardModelList[i].gameObject.SetActive(rewardModelList[i].name == modelName);
        }
        ItemGO.SetActive(true);
    }

    private void Hide() {
        Send.SendMsg(SendType.HideFlyReward);
        StopAnim();
        ItemGO.SetActive(false);
    }

    private void Reset() {
        RandomPos();
        isRecycle = false;
        Trans.position = spawnPos;
        startTime = Time.time;
        dir = 1;
        moveTW = Trans.DOMove(repePos, 2f).SetEase(Ease.InOutQuad).OnComplete(() => RepeMove());
    }

    private void RepeMove() {
        RandomPos();
        dir = -dir;
        repeTW = Trans.DOPath(GetMovePath(Trans.position, new Vector3(dir * repePos.x, repePos.y, repePos.z), 5), ANIM_TIME, PathType.CatmullRom, PathMode.Ignore, 10, null).SetEase(Ease.InOutQuad);
        coDelay = CoDelegator.Coroutine(CoRepeMove());
    }

    private void RecycleMove() {
        repeTW = Trans.DOPath(GetMovePath(Trans.position, new Vector3(dir * spawnPos.x, spawnPos.y, spawnPos.z), 5), ANIM_TIME / 2, PathType.CatmullRom, PathMode.Ignore, 10, null).SetEase(Ease.InOutQuad).OnComplete(() => {
            Hide();
        });
    }

    public static Vector3[] GetMovePath(Vector3 StartVec, Vector3 EndVec, int number) {
        float x = 1;
        float changeValue1 = 0.1f;
        float changeValue2 = 0.1f;
        float endY = EndVec.y;
        StartVec.y *= x + changeValue1 + changeValue2;
        EndVec.y *= x + changeValue1 + changeValue2;
        StartVec.x *= x - changeValue1 + changeValue2;
        EndVec.x *= x - changeValue1 + changeValue2;
        Vector3[] array = new Vector3[number];
        array[0] = StartVec;
        float num = EndVec.x - StartVec.x;
        for (int i = 1; i < number; i++) {
            array[i].x = array[i - 1].x + num / (float)number;
            array[i].y = StartVec.y + GetRamdomLength(i);
            array[i].z = Z_POS;
        }
        array[number - 1] = new Vector3(EndVec.x, endY, EndVec.z);
        return array;
    }

    private static float GetRamdomLength(int i) {
        return (i % 2 != 0) ? Random.Range(0.05f, 0.15f) : Random.Range(-0.05f, -0.15f);
    }

    private void RandomPos() {
    aa:
        Vector3 pos = posList[Random.Range(0, posList.Count)];
        if (pos == repePos)
            goto aa;
        else
            repePos = pos;
    }

    IEnumerator CoRepeMove() {
        yield return new WaitForSeconds(ANIM_TIME + WAIT_TIME);
        RepeMove();
    }

    private void Recycle() {
        StopAnim();
        RecycleMove();
    }

    private void StopAnim() {
        moveTW?.Kill();
        repeTW?.Kill();
        if (coDelay != null) {
            CoDelegator.StopCoroutineEx(coDelay);
        }
    }
}

public enum ADRewardType {
    Coin,
    AutoCollect,
    Boost,
    DoubleIncome,
    Spin,
    Offline,
}