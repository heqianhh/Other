using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCameraMsgDoubleFinger : BaseEventTriggerGet {
    private const float CAMERA_MAX_ORTHSIZE = 40f;
    private const float CAMERA_MIN_ORTHSIZE = 15f;
    private const float CAMERA_MAX_VIEW = 30f;
    private const float CAMERA_MIN_VIEW = 30f;
    private const float CAMERA_ORTHSIZE_SPEED = 2.5f;
    private const float CAMERA_CHANGE_TIME = 0.25f;
    private const float CAMERA_MOVE_SPEED = 5f;

    public Vector3 MouseEnter = Vector3.zero;
    public Vector3 MouseDrag = Vector3.zero;
    public Vector3 EnterPos;
    public Camera camera;
    public Transform transCamera;
    public float xMinLow = -7f;
    public float xMaxLow = 7f;
    public float zMinLow = -5.3f;
    public float zMaxLow = 21f;
    public float xMinHigh = -2f;
    public float xMaxHigh = 2f;
    public float zMinHigh = 9f;
    public float zMaxHigh = 25f;
    public bool Wait = false;
    bool isDrag = false;
    bool isOrthographic;
    bool IsScale = false;
    private static Vector2 windowScale = new Vector2(750, 1334);
    Quaternion newRotation;
    Vector3 TargerPos;
    public BaseCameraMsgDoubleFinger(Camera camera) {
        this.camera = camera;
        transCamera = camera.transform;
        isOrthographic = camera.orthographic;
        newRotation = Quaternion.Euler(new Vector3(0f, transCamera.eulerAngles.y, 0f));
        TargerPos = transCamera.position;
    }
    public override void InitMsg() {
        base.InitMsg();
        Send.RegisterMsg(SendType.CameraLookThis, OnCameraLookThis);
        Send.RegisterMsg(SendType.CameraLookThisBack, CameraLookThisBack);
    }
    private void OnCameraLookThis(object[] objs) {
        Vector3 pos = (Vector3)objs[0];
        MoveCameraLook(pos);
    }
    private void CameraLookThisBack(object[] objs) {
        Vector3 pos = (Vector3)objs[0];
        MoveCameraLookBack(pos);
    }
    private Vector3 GetCameraLookYPos(float Posy) {
        Vector3 lookDir = transCamera.forward;
        float CameraY = transCamera.position.y;
        float height = CameraY - Posy;
        float rate = height / lookDir.y;
        return lookDir * rate;
    }
    private Vector3 GetCameraMovePos(Vector3 LookPos) {
        Vector3 NowCameraLookPos = GetCameraLookYPos(LookPos.y);
        Vector3 Temp = LookPos + NowCameraLookPos;
        return Temp;
    }
    public void MoveCameraLook(Vector3 LookPos) {
        Vector3 MoveLookPos = GetCameraMovePos(LookPos);
        Wait = true;
        transCamera.DOMove(MoveLookPos, 1f).OnComplete(() => {
            Wait = false;
        });
    }
    public void MoveCameraLookBack(Vector3 LookPos) {
        Vector3 StartPos = transCamera.position;
        Vector3 EndPos = GetCameraMovePos(LookPos);
        Wait = true;
        Sequence sequence = DOTween.Sequence();
        sequence.Insert(0f, transCamera.DOMove(EndPos, 1f).SetEase(Ease.Linear));
        sequence.Insert(1f, DOTween.To(setter: value => { }, 0f, 0f, 1f));
        sequence.Insert(2f, transCamera.DOMove(StartPos, 1f).SetEase(Ease.Linear));
        sequence.OnComplete(() => {
            Wait = false;
        });
    }
    float NearCameraOffset = 10f;
    public bool JudgePosNearCamera(Vector3 Pos) {
        Vector3 Endpos = GetCameraMovePos(Pos);
        float nowOffset = Vector3.Distance(Endpos, transCamera.position);
        return nowOffset <= NearCameraOffset;
    }
    public override void OnCtrlUp(object[] _objs) {
        base.OnCtrlUp(_objs);
        if (isDrag) {
            isDrag = false;
        }
    }
    public override void OnCtrlDown(object[] _objs) {
        base.OnCtrlDown(_objs);
        EnterPos = transCamera.position;
        MouseDrag = point;
    }
    public override void OnCtrlDrag(object[] _objs) {
        base.OnCtrlDrag(_objs);
        MouseEnter = point;
        if (!isDrag) {
            isDrag = true;
        }
    }
    public void Update() {
        if (Wait) {
            return;
        }

#if UNITY_EDITOR
        IsScale = ScaleViewPC();
#else
        IsScale = ScaleViewMobile();
#endif
        MoveView();
        if (transCamera.position != TargerPos) {
            transCamera.position = Vector3.Lerp(transCamera.position, TargerPos, CAMERA_MOVE_SPEED * Time.deltaTime);
        }
    }
    private bool ScaleViewPC() {
        float nowY = isOrthographic ? camera.orthographicSize : camera.fieldOfView;
        float nowMouseScroll = Input.GetAxis("Mouse ScrollWheel");
        if (nowMouseScroll != 0f) {
            float offset = 3f;
            nowY += nowMouseScroll > 0 ? -offset : offset;
            if (isOrthographic) {
                nowY = Mathf.Clamp(nowY, CAMERA_MIN_ORTHSIZE, CAMERA_MAX_ORTHSIZE);
                camera.orthographicSize = nowY;
            }
            else {
                nowY = Mathf.Clamp(nowY, CAMERA_MIN_VIEW, CAMERA_MAX_VIEW);
                camera.fieldOfView = nowY;
            }
            ScaleHand();
            return true;
        }
        return false;
    }
    private Touch lastTouch1;
    private Touch lastTouch2;
    private bool ScaleViewMobile() {
        if (Input.touchCount > 1) {
            Touch newTouch1 = Input.GetTouch(0);
            Touch newTouch2 = Input.GetTouch(1);
            for (int i = 0; i < Input.touchCount; ++i) {
                if (Input.GetTouch(i).phase == TouchPhase.Began || Input.GetTouch(i).phase == TouchPhase.Canceled ||
                    Input.GetTouch(i).phase == TouchPhase.Ended) {
                    lastTouch2 = newTouch2;
                    lastTouch1 = newTouch1;
                    return false;
                }
            }
            float lastDistance = Vector2.Distance(lastTouch1.position, lastTouch2.position);
            float newDistance = Vector2.Distance(newTouch1.position, newTouch2.position);
            float offset = lastDistance - newDistance;
            ScaleHand();
            ChangeOrthSize(offset > 0);
            return true;
        }
        return false;
    }
    private void MoveView() {

        if (isDrag) {
            if (Input.touchCount > 1) {
                return;
            }
            float x, z;
            float nowY = isOrthographic ? camera.orthographicSize : Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad) * camera.transform.position.y;
            Vector3 posValue = (MouseDrag - MouseEnter) * nowY;
            Vector3 MouseOffset = new Vector3(posValue.x / Screen.height * 2f, 0f, posValue.y * windowScale[1] * windowScale[1] / windowScale[0] / windowScale[0] / Screen.height);
            MouseOffset = newRotation * MouseOffset;
            float rate = GetRate();
            x = EnterPos.x + MouseOffset.x;
            x = Mathf.Clamp(x, GetxMin(rate), GetxMax(rate));
            z = EnterPos.z + MouseOffset.z;
            z = Mathf.Clamp(z, GetzMin(rate), GetzMax(rate));
            MoveHand();
            TargerPos = new Vector3(x, transCamera.position.y, z);
        }
        if (IsScale) {
            float x, z;
            float rate = GetRate();
            x = Mathf.Clamp(transCamera.position.x, GetxMin(rate), GetxMax(rate));
            z = Mathf.Clamp(transCamera.position.z, GetzMin(rate), GetzMax(rate));
            TargerPos = new Vector3(x, transCamera.position.y, z);
        }

    }
    private float GetRate() {
        return isOrthographic ? ((camera.orthographicSize - CAMERA_MIN_ORTHSIZE) / (CAMERA_MAX_ORTHSIZE - CAMERA_MIN_ORTHSIZE)) : ((camera.fieldOfView - CAMERA_MIN_VIEW) / (CAMERA_MAX_VIEW - CAMERA_MIN_VIEW));
    }
    private float GetxMin(float rate) {
        return xMinLow == xMinHigh ? xMinLow : Mathf.Lerp(xMinLow, xMinHigh, rate);
    }
    private float GetxMax(float rate) {
        return xMaxLow == xMaxHigh ? xMaxLow : Mathf.Lerp(xMaxLow, xMaxHigh, rate);
    }
    private float GetzMin(float rate) {
        return zMinLow == zMinHigh ? zMinLow : Mathf.Lerp(zMinLow, zMinHigh, rate);
    }
    private float GetzMax(float rate) {
        return zMaxLow == zMaxHigh ? zMaxLow : Mathf.Lerp(zMaxLow, zMaxHigh, rate);
    }
    private void ChangeOrthSize(bool add = true) {
        if (camera != null) {
            if (isOrthographic) {
                float size = camera.orthographicSize + CAMERA_ORTHSIZE_SPEED * (add ? 1 : -1);
                size = Mathf.Clamp(size, CAMERA_MIN_ORTHSIZE, CAMERA_MAX_ORTHSIZE);
                camera.DOOrthoSize(size, CAMERA_CHANGE_TIME);
            }
            else {
                float size = camera.fieldOfView + CAMERA_ORTHSIZE_SPEED * (add ? 1 : -1);
                size = Mathf.Clamp(size, CAMERA_MIN_VIEW, CAMERA_MAX_VIEW);
                camera.DOFieldOfView(size, CAMERA_CHANGE_TIME);
            }
        }
    }

    public virtual void ScaleHand() {

    }
    public virtual void MoveHand() {

    }
}
