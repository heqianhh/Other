using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AndMoney.SptObj {
    public class ScriptableTool : Singleton<ScriptableTool> {
        public void SaveObjChildPos(GameObject gameObject, string outName, string outfolder) {
            List<Vector3> pos = new List<Vector3>();
            foreach (Transform temp in gameObject.transform) {
                pos.Add(temp.position);
            }
            ListPos listPos = (ListPos)LocalEditorAssetMgr.Instance.LoadScriptableObject(outfolder, outName);
            if (listPos == null) {
                listPos = (ListPos)ListPos.CreateInstance(typeof(ListPos));
                listPos.name = "listPos";
                listPos.pos = pos;
                LocalEditorAssetMgr.Instance.SaveScriptableObject(listPos, outName, outfolder);
            }
            else {
                listPos.name = "listPos";
                listPos.pos = pos;
            }
        }
    }
}

