using UnityEngine;
using UnityEngine.UI;

public class BattleCountDown {
    public float nowTime;
    public float maxTime;
    public bool CanGo = false;
    GameObject ObjGo;
    Text txt;
    public CountDownCdt countDownCdt;
    System.Action action;
    public BattleCountDown(GameObject ObjGo, CountDownCdt countDownCdt) {
        this.ObjGo = ObjGo;
        txt = ObjGo.GetChildControl<Text>("Text");
        this.countDownCdt = countDownCdt;
    }
    public void StartCount(float maxTime, System.Action action) {
        this.maxTime = maxTime;
        this.action = action;
        nowTime = 0f;
        ObjGo.SetActive(true);
        CanGo = true;
    }
    public void EndCount() {
        action?.Invoke();
        ObjGo.SetActive(false);
        CanGo = false;
        action = null;
    }
    public void SetView() {
        float TempTime = maxTime - nowTime;
        int minute = (int)(TempTime / 60f);
        int seconds = (int)(TempTime % 60f);
        txt.text = minute.ToString("00") + ":" + seconds.ToString("00");
    }
    public void Update() {
        if (CanGo) {
            if (nowTime < maxTime) {
                nowTime += Time.deltaTime;
            }
            else {
                EndCount();
                nowTime = 0f;
            }
            SetView();
        }
    }
}
public enum CountDownCdt {
    Speed,
    Income,
}