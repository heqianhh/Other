using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RateWindow : BaseWindowWrapper<RateWindow> {
    Button btnYes;
    Button btnNo;

    private string ISSHOW;
    private string ISSHOW_ = "ISSHOW";
    private bool m_IsShow;
    public bool IsShow {
        get {
            return m_IsShow;
        }
        set {
            m_IsShow = value;
            LocalSave.SetBool(ISSHOW, value);
        }
    }
    protected override void InitCtrl() {
        btnYes = gameObject.GetChildControl<Button>("Root/BG/btnYes");
        btnNo = gameObject.GetChildControl<Button>("Root/BG/btnNo");
        LoadData();
    }
    private void LoadData() {
        ISSHOW = ISSHOW_;
        m_IsShow = LocalSave.GetBool(ISSHOW, false);
    }
    protected override void OnPreOpen() {
        IsShow = true;
        Debug.Log(IsShow);
    }
    protected override void OnOpen() {
    }
    protected override void InitMsg() {
        btnYes.onClick.AddListener(OnYesClick);
        btnNo.onClick.AddListener(OnNoClick);
    }
    protected override void ClearMsg() {
        btnYes.onClick.RemoveListener(OnYesClick);
        btnNo.onClick.RemoveListener(OnNoClick);
    }
    private void OnYesClick() {
        ToolMgr.Instance.ShowRate();
        CloseThis();
    }
    private void OnNoClick() {
        CloseThis();
    }
    public void OpenThis() {
        WindowMgr.Instance.OpenWindow<RateWindow>();
    }
    public void CloseThis() {
        WindowMgr.Instance.CloseWindow<RateWindow>();
    }
}
