
using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AndMoney {
    public class EditorTool {

        public static bool GetSetBool(string label, string savePrefsKey, bool value) {
            value = EditorPrefs.GetBool(savePrefsKey, value);
            EditorGUI.BeginChangeCheck();
            value = EditorGUILayout.Toggle(label, value);
            if (EditorGUI.EndChangeCheck()) {
                EditorPrefs.SetBool(savePrefsKey, value);
            }
            return value;
        }

        public static int GetSetInt(string label, string savePrefsKey, int value) {
            value = EditorPrefs.GetInt(savePrefsKey, value);
            EditorGUI.BeginChangeCheck();
            value = EditorGUILayout.IntField(label, value);
            if (EditorGUI.EndChangeCheck()) {
                EditorPrefs.SetInt(savePrefsKey, value);
            }
            return value;
        }
        public static Vector2Int GetSetVectorInt2(string label, string savePrefsKey, Vector2Int value) {
            value = new Vector2Int(
                EditorPrefs.GetInt(savePrefsKey + "_x", value.x),
                EditorPrefs.GetInt(savePrefsKey + "_y", value.y)
            );
            EditorGUI.BeginChangeCheck();
            value = EditorGUILayout.Vector2IntField(label, value);
            if (EditorGUI.EndChangeCheck()) {
                EditorPrefs.SetInt(savePrefsKey + "_x", value.x);
                EditorPrefs.SetInt(savePrefsKey + "_y", value.y);
            }
            return value;
        }
        public static Vector2Int GetSetVectorInt2(string label, string savePrefsKey, int valueX, int valueY) {
            int savedValueX = EditorPrefs.GetInt(savePrefsKey + "_x", valueX);
            int savedValueY = EditorPrefs.GetInt(savePrefsKey + "_y", valueY);

            EditorGUI.BeginChangeCheck();
            Vector2Int endValue = EditorGUILayout.Vector2IntField(label, new Vector2Int(savedValueX, savedValueY));

            if (EditorGUI.EndChangeCheck()) {
                EditorPrefs.SetInt(savePrefsKey + "_x", endValue.x);
                EditorPrefs.SetInt(savePrefsKey + "_y", endValue.y);
            }
            return endValue;
        }
        public static Vector2Int GetSetVectorInt2(string label, int valueX, int valueY) {
            EditorGUI.BeginChangeCheck();
            Vector2Int endValue = EditorGUILayout.Vector2IntField(label, new Vector2Int(valueX, valueY));
            EditorGUI.EndChangeCheck();
            return endValue;
        }
        public static float GetSetFloat(string label, string savePrefsKey, float value) {
            value = EditorPrefs.GetFloat(savePrefsKey, value);
            EditorGUI.BeginChangeCheck();
            value = EditorGUILayout.FloatField(label, value);
            if (EditorGUI.EndChangeCheck()) {
                EditorPrefs.SetFloat(savePrefsKey, value);
            }
            return value;
        }
        public static float GetSetFloat(string label, float value) {
            EditorGUI.BeginChangeCheck();
            value = EditorGUILayout.FloatField(label, value);
            EditorGUI.EndChangeCheck();
            return value;
        }

        public static string GetSetString(string label, string savePrefsKey, string value) {
            value = EditorPrefs.GetString(savePrefsKey, value);
            EditorGUI.BeginChangeCheck();
            value = EditorGUILayout.TextField(label, value);
            if (EditorGUI.EndChangeCheck()) {
                EditorPrefs.SetString(savePrefsKey, value);
            }
            return value;
        }
        public static string GetSetString(string label, string savePrefsKey, string[] options, string value) {
            value = EditorPrefs.GetString(savePrefsKey, value);

            int selectedIndex = Array.IndexOf(options, value);
            selectedIndex = EditorGUILayout.Popup(label, selectedIndex, options);

            value = options[Mathf.Clamp(selectedIndex, 0, options.Length - 1)];

            if (EditorGUI.EndChangeCheck()) {
                EditorPrefs.SetString(savePrefsKey, value);
            }
            return value;
        }

        public static T GetSelectedGameObjectObject<T>(string label, string savePrefsKey, T gameObject) where T : UnityEngine.Object {
            string folderPath = EditorPrefs.GetString(savePrefsKey, "");
            gameObject = AssetDatabase.LoadAssetAtPath<T>(folderPath);
            EditorGUI.BeginChangeCheck();
            gameObject = EditorGUILayout.ObjectField(label, gameObject, typeof(T), false) as T;
            if (EditorGUI.EndChangeCheck()) {
                folderPath = AssetDatabase.GetAssetPath(gameObject);
                EditorPrefs.SetString(savePrefsKey, folderPath);
            }
            return gameObject;
        }

        public static Object GetSelectedFolderObject(string label, string savePrefsKey, Object folderObject) {
            string folderPath = EditorPrefs.GetString(savePrefsKey, "");
            folderObject = AssetDatabase.LoadAssetAtPath<Object>(folderPath);
            EditorGUI.BeginChangeCheck();
            folderObject = EditorGUILayout.ObjectField(label, folderObject, typeof(Object), false);
            if (EditorGUI.EndChangeCheck()) {
                folderPath = AssetDatabase.GetAssetPath(folderObject);
                EditorPrefs.SetString(savePrefsKey, folderPath);
            }
            return folderObject;
        }


    }
}




