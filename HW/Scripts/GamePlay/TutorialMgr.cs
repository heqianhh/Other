using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMgr : Singleton<TutorialMgr> {

    private const string TUTORIAL_ID_KEY = "TutorialID";
    private int m_tutorialID;
    public int TutorialID {
        get {
            return m_tutorialID;
        }
        set {
            m_tutorialID = value;
            LocalSave.SetInt(TUTORIAL_ID_KEY, value);
        }
    }

    public TutorialType curTutorialType;

    public Transform transTutorial;

    public void Init() {
        m_tutorialID = LocalSave.GetInt(TUTORIAL_ID_KEY, 0);
        curTutorialType = (TutorialType)m_tutorialID;

        InitMsg();
    }

    public void Clear() {
        ClearMsg();
    }

    private void InitMsg() {
        
    }

    private void ClearMsg() {
        
    }

    public void NextTutorial(TutorialType curType, bool Memory = true) {
        if ((TutorialType)TutorialID == curType) {
            SetTutorialID(TutorialID + 1, Memory);
        }
    }

    public void SetTutorialID(int index, bool Memory = true) {
        if (Memory) {
            TutorialID = index;
        }
        else {
            m_tutorialID = index;
        }
        curTutorialType = (TutorialType)m_tutorialID;
        Send.SendMsg(SendType.CheckTutorial, (TutorialType)TutorialID);
    }
    public bool CanShowTrans() {
        if (transTutorial == null) {
            return false;
        }
        else {
            if (InTutorial()) {
                return true;
            }
            else {
                return false;
            }
        }
    }


    public bool InTutorial() {
        return curTutorialType <= TutorialType.CloseTask2;
    }
}

public enum TutorialType {
    ClickRobot1,
    AddTaskCutTree,
    CloseTask1,
    ClickRobot2,
    AddTaskGetWood,
    AddTaskPutWood,
    CloseTask2,
}
