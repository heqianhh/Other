using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OfflineMgr : Singleton<OfflineMgr> {

    private const string OFFLINE_DATE_KEY = "OfflineDate";
    private long m_offlineDate;
    public long OfflineDate {
        get {
            return m_offlineDate;
        }
        set {
            m_offlineDate = value;
            LocalSave.SetString(OFFLINE_DATE_KEY, m_offlineDate.ToString());
        }
    }

    public bool hasInit = false;

    public int maxOffTime = 300;
    public int showWindowTime = 60;

    public float rewardMul = 0.1f;

    public void Init() {
        m_offlineDate = long.Parse(LocalSave.GetString(OFFLINE_DATE_KEY, "0"));
        InitMsg();

        hasInit = true;
    }

    public void Clear() {
        ClearMsg();
    }

    private void InitMsg() {

    }

    private void ClearMsg() {

    }

    /// <summary>
    /// 计算离线时间
    /// </summary>
    /// <returns></returns>
    public long CountOffTime() {
        long curTime = TimeMgr.GetTimeStamp(true);
        long value = m_offlineDate == 0 ? 0 : curTime - m_offlineDate;
        long max = (long)(maxOffTime);
        //限制最大离线时间
        if (value > max) {
            value = max;
        }
        return value;
    }

    public void OnPause(bool pause = true) {
        if (!hasInit)
            return;
        if (pause) {
            OfflineDate = TimeMgr.GetTimeStamp(true);
        }
        else {
            long offTime = CountOffTime();
            Debug.Log("OffTime :" + offTime);
            OfflineDate = TimeMgr.GetTimeStamp(true);

            if (offTime >= showWindowTime) {
                OfflineWindow.Instance.OpenWindow(offTime);
            }
        }
    }

    /// <summary>
    /// 获取离线收益倍率（管理员加成）
    /// </summary>
    /// <returns></returns>
    public float GetOffTimeRewardMul() {
        float value = rewardMul;
        return value;
    }
}
