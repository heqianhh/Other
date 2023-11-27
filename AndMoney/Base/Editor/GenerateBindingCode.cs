using System;
using UnityEditor;
using UnityEngine;

public class GenerateBindingCode : Editor {
    static string canvasName = "Canvas (Environment)";
    static string canvasNamePrefab = "Prefab Mode in Context";
    [MenuItem("CONTEXT/Component/GenerateBindingCode")]
    private static void CONTEXTGenerateCode(MenuCommand command) {
        Component component = command.context as Component;
        if (component != null) {
            Type scriptType = component.GetType();
            string str = GenerateBindingTool.GetComponetScriptType(scriptType, GetHierarchyIndex(component.gameObject));
            EditorGUIUtility.systemCopyBuffer = str;
            Debug.Log(str);
        }
        else {
            Debug.LogError("不是Component");
        }
    }
    [MenuItem("CONTEXT/Component/GenerateBindingPath")]
    private static void CONTEXTGeneratePath(MenuCommand command) {
        Component component = command.context as Component;

        if (component != null) {
            string str = GetHierarchyIndex(component.gameObject);
            EditorGUIUtility.systemCopyBuffer = str;
            Debug.Log(str);
        }
        else {
            Debug.LogError("不是Component");
        }
    }
    [MenuItem("CONTEXT/Component/GenerateBindingPathFather")]
    private static void CONTEXTGeneratePathFather(MenuCommand command) {
        Component component = command.context as Component;
        if (component != null) {
            string str = GetHierarchyIndexFather(component.gameObject);
            EditorGUIUtility.systemCopyBuffer = str;
            Debug.Log(str);
        }
        else {
            Debug.LogError("不是Component");
        }
    }

    [MenuItem("GameObject/Custom Option/GenerateBindingPath", false, 0)]
    private static void GameObjectGeneratePath(MenuCommand menuCommand) {
        GameObject selectedObject = menuCommand.context as GameObject;
        if (selectedObject != null) {
            string str = GetHierarchyIndex(selectedObject);
            EditorGUIUtility.systemCopyBuffer = str;
            Debug.Log(str);
        }
    }
    [MenuItem("GameObject/Custom Option/GenerateBindingPathFather", false, 1)]
    private static void GameObjectGeneratePathFather(MenuCommand menuCommand) {
        GameObject selectedObject = menuCommand.context as GameObject;
        if (selectedObject != null) {
            string str = GetHierarchyIndexFather(selectedObject);
            EditorGUIUtility.systemCopyBuffer = str;
            Debug.Log(str);
        }
    }
    private static bool JudgeCanvas(string name) {
        return name == canvasName || name == canvasNamePrefab;
    }
    private static string GetHierarchyIndex(GameObject child) {
        if (child == null) {
            return null;
        }
        Transform parent = child.transform.parent;
        if (parent == null || JudgeCanvas(parent.name)) {
            return null;
        }
        else {
            string childName = child.name;
            string parentName = GetHierarchyIndex(parent.gameObject);

            if (parentName == null || JudgeCanvas(parent.name)) {
                return childName;
            }
            else {
                return parentName + "/" + childName;
            }
        }
    }
    private static string GetHierarchyIndexFather(GameObject child) {
        if (child == null) {
            return null;
        }
        Transform parent = child.transform.parent;
        if (parent == null || JudgeCanvas(parent.name)) {
            return child.name;
        }
        else {
            string childName = child.name;
            string parentName = GetHierarchyIndexFather(parent.gameObject);

            if (parentName == null || JudgeCanvas(parent.name)) {
                return childName;
            }
            else {
                return parentName + "/" + childName;
            }
        }


    }
}