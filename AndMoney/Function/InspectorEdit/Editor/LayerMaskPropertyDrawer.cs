using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(LayerMask))]
public class LayerMaskPropertyDrawer : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.BeginProperty(position, label, property);

        // 获取LayerMask的值
        int layerMask = property.intValue;

        // 显示标签
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // 绘制可编辑的LayerMask字段
        layerMask = EditorGUI.MaskField(position, GUIContent.none, layerMask, UnityEditorInternal.InternalEditorUtility.layers);

        // 设置LayerMask的值
        property.intValue = layerMask;

        EditorGUI.EndProperty();
    }
}
