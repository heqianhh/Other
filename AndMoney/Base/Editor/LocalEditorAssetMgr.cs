using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace AndMoney {
    public class LocalEditorAssetMgr : Singleton<LocalEditorAssetMgr> {
        public ScriptableObject LoadScriptableObject(string folderPath, string inname) {
            string assetPath = folderPath + $"/{inname}.asset";
            return AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPath);
        }
        public GameObject LoadPrefab(string folderPath) {
            string assetPath = folderPath + $"/{folderPath}.prefab";
            return AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
        }
        public List<GameObject> LoadAllPrefab(string folderPath) {
            List<GameObject> prefabs = new List<GameObject>();
            string[] assetPaths = Directory.GetFiles(folderPath, "*.prefab");
            foreach (string assetPath in assetPaths) {
                UnityEngine.Object asset = AssetDatabase.LoadAssetAtPath(assetPath, typeof(GameObject));
                GameObject gameObject = asset as GameObject;
                GameObject newObject = GameObject.Instantiate(asset as GameObject);
                newObject.name = gameObject.name;
                prefabs.Add(newObject);
            }
            return prefabs;
        }
        public List<Material> LoadAllMaterial(string folderPath) {
            List<Material> prefabs = new List<Material>();
            string[] assetPaths = Directory.GetFiles(folderPath, "*.mat");
            foreach (string assetPath in assetPaths) {
                UnityEngine.Object asset = AssetDatabase.LoadAssetAtPath(assetPath, typeof(Material));
                Material gameObject = asset as Material;
                Material newObject = GameObject.Instantiate(asset as Material);
                newObject.name = gameObject.name;
                prefabs.Add(newObject);
            }
            return prefabs;
        }

        public void SavePrefab(GameObject prefab, string folderPath) {
            AssetDatabase.CreateAsset(prefab, folderPath + $"/{prefab.name}.prefab");
        }
        public void SaveScriptableObject(ScriptableObject scriptableObject, string outname, string folderPath) {
            AssetDatabase.CreateAsset(scriptableObject, folderPath + $"/{outname}.asset");
        }
        public void SavePrefabs(List<GameObject> prefabs, string folderPath) {
            AssetDatabase.DeleteAsset(folderPath);
            Directory.CreateDirectory(folderPath);
            foreach (var temp in prefabs) {
                SavePrefab(temp, folderPath);
            }
        }

        public List<string> LoadAllName<T>(string folderPath) {
            List<string> prefabNames = new List<string>();

            UnityEngine.Object[] prefabs = AssetDatabase.LoadAllAssetsAtPath(folderPath);
            if (prefabs.Length == 0) {
                Debug.LogError("Failed LoadAllName from " + folderPath);
            }
            foreach (UnityEngine.Object prefab in prefabs) {
                prefabNames.Add(prefab.name);
            }

            return prefabNames;
        }
    }
}

