using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OfflineWindow : BaseWindowWrapper<OfflineWindow> {

    //private Button btnClose;
    private Text txtCash;

    private Button btnAD;

    private double rewardCash = 0;

    private ADRewardType adType = ADRewardType.Offline;

    private Button btnGet;

    protected override void InitCtrl() {
        //btnClose = gameObject.GetChildControl<Button>("Root/BG/btnClose");
        txtCash = gameObject.GetChildControl<UnityEngine.UI.Text>("Root/bg/bg02/txtmoney");


        btnAD = gameObject.GetChildControl<UnityEngine.UI.Button>("Root/bg/btnfree");

        btnGet = gameObject.GetChildControl<UnityEngine.UI.Button>("Root/bg/btncollect");
    }

    protected override void OnPreOpen() {

    }


    protected override void InitMsg() {
        //btnClose.onClick.AddListener(OnCloseClick);
        btnAD.onClick.AddListener(OnADClick);
        btnGet.onClick.AddListener(OnCloseClick);
    }

    protected override void ClearMsg() {
        //btnClose.onClick.RemoveListener(OnCloseClick);
        btnAD.onClick.RemoveListener(OnADClick);
        btnGet.onClick.RemoveListener(OnCloseClick);
    }

    private void OnCloseClick() {
        Collect(1);
        WindowMgr.Instance.CloseWindow<OfflineWindow>();
    }

    private void OnADClick() {
#if USE_SDKBUNDLE
        if (HWGames.AdManager.Instance.IsRewardADReady()) {
            HWGames.AdManager.Instance.ShowRewardAD("Offline", SuccessAD);
        }
        else {
            HWMsgTipWindow.Instance.ShowTip("Please wait, AD not ready");
        }

#else
        SuccessAD();
#endif
    }

    private void SuccessAD() {
        Collect(2);
        WindowMgr.Instance.CloseWindow<OfflineWindow>();
    }

    private void Collect(int mul) {
        Send.SendMsg(SendType.AddMoney, rewardCash * mul);
        //GainCoinCtrl.Instance.ShowFlyEffect(transform.position, MainWindow.Instance.imgMoney.transform.position, 15, 0.5f, "money2");
    }

    public void OpenWindow(long _offlineTime) {
        //rewardCash = RoadMgr.Instance.GetSecondIncome() * _offlineTime;
        if (rewardCash <= 0)
            return;
        txtCash.text = AcNumMgr.ACNum(rewardCash);
        WindowMgr.Instance.OpenWindow<OfflineWindow>();
    }
}
