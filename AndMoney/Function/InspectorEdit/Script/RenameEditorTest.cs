using UnityEngine;
public class RenameEditorTest : MonoBehaviour {
    [Title("标题", "red")]

    [Rename("单个属性")]
    public int num;

    [RenameInEditor("属性列表")]
    public BulletAttr[] bulletAttrs;

    [System.Serializable]
    public class BulletAttr {
        [Rename("属性1")]
        public string name;
        [Rename("属性2")]
        public int BulletDamage;
    }
}
