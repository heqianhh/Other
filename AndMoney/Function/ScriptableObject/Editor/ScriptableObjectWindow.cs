using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace AndMoney.SptObj {
    public class ScriptableObjectWindow : EditorWindow {

        int dataListNum;
        private List<MyData> dataList = new List<MyData>();
        private Vector2 scrollPos;

        string SaveKey = "AndMoney.ScriptableObject";
        [MenuItem("AndMoney/ScriptableObject")]
        public static void ShowWindow() {
            GetWindow<ScriptableObjectWindow>("ScriptableObject");
        }
        private void OnGUI() {
            GUILayout.BeginVertical(GUI.skin.box);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("+")) {
                dataList.Add(new MyData());
                EditorPrefs.SetInt($"{SaveKey}_dataListNum", dataList.Count);
            }
            if (GUILayout.Button("-")) {
                if (dataList.Count > 0) {
                    dataList.RemoveAt(dataList.Count - 1);
                    EditorPrefs.SetInt($"{SaveKey}_dataListNum", dataList.Count);
                }
            }
            GUILayout.EndHorizontal();
            scrollPos = GUILayout.BeginScrollView(scrollPos);

            dataListNum = EditorPrefs.GetInt($"{SaveKey}_dataListNum", 0);
            for (int i = dataList.Count; i < dataListNum; i++) {
                dataList.Add(new MyData());
            }
            for (int i = 0; i < dataList.Count; i++) {
                GUILayout.BeginVertical();

                dataList[i].obj = EditorTool.GetSelectedGameObjectObject<GameObject>("Ŀ��Ԥ���壺", $"{SaveKey}_obj_{i}", dataList[i].obj);
                dataList[i].objname = EditorTool.GetSetString("������ƣ�", $"{SaveKey}_objname_{i}", dataList[i].objname);
                dataList[i].output_fold = EditorTool.GetSelectedFolderObject("����ļ��У�", $"{SaveKey}_output_fold_{i}", dataList[i].output_fold);

                if (GUILayout.Button("��ȡ������")) {
                    ScriptableTool.Instance.SaveObjChildPos(dataList[i].obj, dataList[i].objname, AssetDatabase.GetAssetPath(dataList[i].output_fold));
                }

                GUILayout.EndVertical();
            }

            GUILayout.EndScrollView();
            GUILayout.EndVertical();

            GUILayout.Space(10);
            if (GUILayout.Button("��ȡȫ��������")) {
                for (int i = 0; i < dataList.Count; i++) {
                    ScriptableTool.Instance.SaveObjChildPos(dataList[i].obj, dataList[i].objname, AssetDatabase.GetAssetPath(dataList[i].output_fold));
                }
            }
        }

        private class MyData {
            public GameObject obj;
            public string objname;
            public Object output_fold;
        }
    }


}

