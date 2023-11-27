using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(LayerMask))]
public class LayerMaskPropertyDrawer : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.BeginProperty(position, label, property);

        // ��ȡLayerMask��ֵ
        int layerMask = property.intValue;

        // ��ʾ��ǩ
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // ���ƿɱ༭��LayerMask�ֶ�
        layerMask = EditorGUI.MaskField(position, GUIContent.none, layerMask, UnityEditorInternal.InternalEditorUtility.layers);

        // ����LayerMask��ֵ
        property.intValue = layerMask;

        EditorGUI.EndProperty();
    }
}
