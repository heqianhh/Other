using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AndMoney {
    public class FxMgr : Singleton<FxMgr> {
        List<FxCtrl> fxCtrls = new List<FxCtrl>();
        /// <summary>
        /// 创建一个自动回收的物体
        /// </summary>
        /// <param name="strF">文件位置</param>
        /// <param name="strC">特效名称</param>
        /// <param name="trans">特效位置</param>
        /// <param name="time">播放时间</param>
        public Transform CreateFx(string strF, string strC, Transform trans, float time = 1f, Action<Transform> action = null) {
            Transform transTemp = ObjectPool.Instance.Get(strF, strC).transform;
            transTemp.position = trans.position;
            transTemp.rotation = trans.rotation;
            FxCtrl fxCtrl = transTemp.gameObject.GetComponent<FxCtrl>();
            fxCtrls.Add(fxCtrl);
            fxCtrl.StartPlayToEnd(time, () => {
                action?.Invoke(transTemp);
                ObjectPool.Instance.Recycle(transTemp.gameObject);
                fxCtrls.Remove(fxCtrl);
            });
            return transTemp;
        }
        public Transform CreateFx(string strF, string strC, Vector3 pos, float time = 1f, Action<Transform> action = null) {
            Transform transTemp = ObjectPool.Instance.Get(strF, strC).transform;
            transTemp.position = pos;
            FxCtrl fxCtrl = transTemp.gameObject.GetComponent<FxCtrl>();
            fxCtrls.Add(fxCtrl);
            fxCtrl.StartPlayToEnd(time, () => {
                action?.Invoke(transTemp);
                ObjectPool.Instance.Recycle(transTemp.gameObject);
                fxCtrls.Remove(fxCtrl);
            });
            return transTemp;
        }
        public void Clear() {
            for (int i = fxCtrls.Count - 1; i >= 0; i--) {
                fxCtrls[i].StopCoutDown();
            }
            fxCtrls.Clear();
        }


    }
}



