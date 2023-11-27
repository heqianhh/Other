using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialProxy : MonoBehaviour
{
    public TutorialType tutorialType;

    private GameObject go;

    private void Awake() {
        go = transform.GetChild(0).gameObject;
        go.SetActive(false);
        Refresh();
        InitMsg();
    }

    private void OnDestroy() {
        ClearMsg();
    }

    private void InitMsg() {
        Send.RegisterMsg(SendType.CheckTutorial, CheckTutorial);
    }

    private void ClearMsg() {
        Send.UnregisterMsg(SendType.CheckTutorial, CheckTutorial);
    }

    private void CheckTutorial(object[] objs) {
        Refresh();
    }

    public void Refresh() {
        bool show = TutorialMgr.Instance.curTutorialType == tutorialType;
        if (show != go.activeSelf) {
            go.SetActive(show);
        }
    }
}
