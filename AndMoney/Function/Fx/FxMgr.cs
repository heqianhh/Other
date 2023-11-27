using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AndMoney {
    public class FxMgr : Singleton<FxMgr> {
        List<FxCtrl> fxCtrls = new List<FxCtrl>();
        /// <summary>
        /// ����һ���Զ����յ�����
        /// </summary>
        /// <param name="strF">�ļ�λ��</param>
        /// <param name="strC">��Ч����</param>
        /// <param name="trans">��Чλ��</param>
        /// <param name="time">����ʱ��</param>
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



