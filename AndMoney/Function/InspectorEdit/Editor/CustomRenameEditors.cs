using UnityEditor;

[CustomEditor(typeof(RenameEditorTest))]
[CanEditMultipleObjects]

public class RenameEditors : RenameEditor {

}

[CustomEditor(typeof(RenameEditorTestOther))]
[CanEditMultipleObjects]

public class RenameEditors_1 : RenameEditor {

}