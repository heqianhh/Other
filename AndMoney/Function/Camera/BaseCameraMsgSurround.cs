using UnityEngine;
using UnityEngine.EventSystems;

public class BaseCameraMsgSurround : BaseEventTriggerGet {
    Transform transF;
    Camera camera;
    float DragNow = 0f;
    float ScreenNow = 0f;
    float ScreenTemp = 0f;
    bool IsDrag = false;
    Vector3 MouseEnter;
    Vector3 MouseDrag;
    bool isCycle;
    public virtual float rotationSpeed => 0.1f;
    public virtual float DragStart => 0f;
    public virtual float DragEnd => 135f;
    public virtual float ScreenMax => Screen.width * 5f;
    float DragLen;
    public BaseCameraMsgSurround(Transform transF, Camera camera, bool isCycle = true) {
        this.transF = transF;
        this.camera = camera;
        this.isCycle = isCycle;
        DragLen = DragEnd - DragStart;
    }
    public override void OnCtrlUp(object[] _objs) {
        //PointerEventData data = (PointerEventData)_objs[0];

        IsDrag = false;
        ScreenNow = ScreenTemp;
    }
    public override void OnCtrlDown(object[] _objs) {
        PointerEventData data = (PointerEventData)_objs[0];
        float x = Mathf.Clamp(data.position.x, 0f, Screen.width);
        float y = Mathf.Clamp(data.position.x, 0f, Screen.height);
        Vector3 point = new Vector3(x, y, 0f);
        MouseEnter = point;
    }
    public override void OnCtrlDrag(object[] _objs) {
        PointerEventData data = (PointerEventData)_objs[0];
        float x = Mathf.Clamp(data.position.x, 0f, Screen.width);
        float y = Mathf.Clamp(data.position.x, 0f, Screen.height);
        Vector3 point = new Vector3(x, y, 0f);
        MouseDrag = point;
        IsDrag = true;
    }
    public void Update() {
        if (IsDrag) {
            float tempDrag = MouseEnter.x - MouseDrag.x;
       
            Quaternion rot = Quaternion.identity;
            ScreenTemp = ScreenNow - tempDrag;
            if (isCycle) {
                if (ScreenTemp > 1f) {
                    ScreenTemp -= 1f;
                }
                else if (ScreenTemp < 0f) {
                    ScreenTemp += 1f;
                }
            }
            else {
                if (ScreenTemp > 1f) {
                    ScreenTemp = 1f;
                }
                else if (ScreenTemp < 0f) {
                    ScreenTemp = 0f;
                }
            }
            DragNow = ScreenTemp / ScreenMax;
            rot = Quaternion.Euler(new Vector3(0f, DragStart + (DragEnd - DragStart) * DragNow, 0f));
            transF.rotation = Quaternion.Lerp(transF.rotation, rot, rotationSpeed);
        }
    }
}
