using UnityEngine;
public class RenameEditorTest : MonoBehaviour {
    [Title("����", "red")]

    [Rename("��������")]
    public int num;

    [RenameInEditor("�����б�")]
    public BulletAttr[] bulletAttrs;

    [System.Serializable]
    public class BulletAttr {
        [Rename("����1")]
        public string name;
        [Rename("����2")]
        public int BulletDamage;
    }
}
