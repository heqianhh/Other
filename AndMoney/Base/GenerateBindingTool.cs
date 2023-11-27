using System;
using UnityEngine;
public class GenerateBindingTool {
    public static string GetComponetScriptType(Type type, string path) {
        if (type == typeof(GameObject)) {
            if (path == null) {
                return $"gameObject.GetComponent<{type}>();";
            }
            else {
                return $"gameObject.GetChildControl<Transform>(\"{path}\").gameobject;";
            }
        }
        else {
            if (path == null) {
                return $"gameObject.GetComponent<{type}>();";
            }
            else {
                return $"gameObject.GetChildControl<{type}>(\"{path}\");";
            }
        }
    }
    public static string GetComponetScriptType(string name, Type type, string path) {
        if (type == typeof(GameObject)) {
            if (path == null) {
                return $"{name} = gameObject.GetComponent<{type}>();";
            }
            else {
                return $"{name} = gameObject.GetChildControl<Transform>(\"{path}\").gameobject;";
            }
        }
        else {
            if (path == null) {
                return $"{name} = gameObject.GetComponent<{type}>();";
            }
            else {
                return $"{name} = gameObject.GetChildControl<{type}>(\"{path}\");";
            }
        }
    }
}
