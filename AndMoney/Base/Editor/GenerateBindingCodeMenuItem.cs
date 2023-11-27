using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class GenerateBindingCodeMenuItem : Editor {
    [MenuItem("CONTEXT/MonoBehaviour/GenerateBindingCode_AllChild")]
    private static void GenerateBinding(MenuCommand command) {
        MonoBehaviour monoBehaviour = command.context as MonoBehaviour;
        if (monoBehaviour != null) {
            GameObject boundObject = monoBehaviour.gameObject;
            string className = monoBehaviour.GetType().Name;

            Debug.Log($"Binding code for {className}:");

            FieldInfo[] fields = monoBehaviour.GetType().GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            string strEnd = string.Empty;
            foreach (var field in fields) {
                var fieldValue = field.GetValue(monoBehaviour);
                if (field.FieldType == typeof(GameObject)) {
                    GameObject obj = fieldValue as GameObject;
                    if (boundObject.transform == obj.transform) {
                        strEnd += $"{field.Name} = gameObject.GetComponent<{field.FieldType}>();" + "\n";
                        continue;
                    }
                    string pathName = GetHierarchyIndex(boundObject, obj);
                    if (pathName == null) {
                        continue;
                    }
                    strEnd += $"{field.Name} = gameObject.GetChildControl<Transform>(\"{pathName}\").gameobject;" + "\n";
                }
                else {
                    Component component = fieldValue as Component;
                    if (component != null) {
                        GameObject obj = component.gameObject;
                        if (boundObject.transform == obj.transform) {
                            strEnd += $"{field.Name} = gameObject.GetComponent<{field.FieldType}>();" + "\n";
                            continue;
                        }
                        string pathName = GetHierarchyIndex(boundObject, obj);
                        if (pathName == null) {
                            continue;
                        }
                        strEnd += $"{field.Name} = gameObject.GetChildControl<{field.FieldType}>(\"{pathName}\");" + "\n";
                    }
                }
            }
            Debug.Log(strEnd);
        }
    }
    private static string GetHierarchyIndex(GameObject parent, GameObject child) {
        if (child == null) {
            return null;
        }
        if (child.transform.IsChildOf(parent.transform)) {
            if (child.transform.parent.gameObject == parent) {
                return child.name;
            }
            string childName = child.name;
            string parentName = GetHierarchyIndex(parent, child.transform.parent.gameObject);
            if (parentName == null) {
                return null;
            }

            return parentName + "/" + childName;
        }
        return null;
    }

}