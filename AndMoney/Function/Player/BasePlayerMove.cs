using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BasePlayerMove : BaseEventTriggerGet {
    public Transform transPlayer;
    public Vector3 MouseEnter;
    public Vector3 MouseDrag;
    public Vector3 nowDirection;
    public Transform MouseControl;
    public RectTransform In;
    public RectTransform Out;
    public float initwidth;
    public float initheight;
    public PlayerMoveCdt playerMoveCdt = PlayerMoveCdt.Idle;
    public float StartSpeed;
    public float Initspeed;
    public float speed;
    public float extendspeed = 1f;
    public BasePlayerCtrl basePlayerCtrl;
    public CharacterController characterController;
    public bool UseRotateOffset = false;
    public float RotateOffset = 0f;
    public PlayerMoveThing playerMoveThing = PlayerMoveThing.None;
    Action hideForwardAct;

    public BasePlayerMove(float StartSpeed = 0.1f) {
        this.StartSpeed = StartSpeed;
        InitMsg();
        Initspeed = StartSpeed;
    }
    public virtual void LoadModel(BasePlayerCtrl basePlayerCtrl) {
        this.basePlayerCtrl = basePlayerCtrl;
        transPlayer = basePlayerCtrl.transform;
        characterController = transPlayer.gameObject.AddMissingComponent<CharacterController>();
    }
    public void ReSetModel() {
        Initspeed = StartSpeed;
    }
    public virtual void SetExtendSpeed(float speed) {
        extendspeed = speed;
    }
    public void SetRotateOffset(float RotateOffset) {
        UseRotateOffset = true;
        this.RotateOffset = RotateOffset;
    }
    public override void OnCtrlUp(object[] _objs) {
        base.OnCtrlUp(_objs);
        nowDirection = Vector3.zero;
        MouseControl.gameObject.SetActive(false);
    }
    public override void OnCtrlDown(object[] _objs) {
        base.OnCtrlDown(_objs);
        MouseEnter = point;
        MouseControl.gameObject.SetActive(true);
        In.SetPosX(MouseEnter.x / Screen.width * initwidth);
        In.SetPosY(MouseEnter.y / Screen.height * initheight);
    }
    public override void OnCtrlDrag(object[] _objs) {
        base.OnCtrlDrag(_objs);
        MouseDrag = point;
        Vector3 nowDrag = MouseDrag - MouseEnter;
        if (nowDrag.magnitude < 40f) {
            nowDirection = nowDrag;
        }
        else {
            nowDirection = nowDrag.normalized * 40f;
        }
        Out.SetPosX(nowDirection.x * 2f);
        Out.SetPosY(nowDirection.y * 2f);
    }
    public float GetMoveSpeed() {
        speed = Initspeed * extendspeed;
        return speed;
    }
    public void ForceToMove(Transform trans, System.Action action = null) {
        characterController.enabled = false;
        transPlayer.position = trans.position;
        transPlayer.rotation = trans.rotation;
        characterController.enabled = true;
        action?.Invoke();
    }
    public void ForceToMove(Vector3 pos, System.Action action = null) {
        characterController.enabled = false;
        transPlayer.position = pos;
        characterController.enabled = true;
        action?.Invoke();

    }
    public void ForceToMove(Transform trans, float time, System.Action action = null) {
        playerMoveThing = PlayerMoveThing.Wait;
        characterController.enabled = false;
        nowDirection = Vector3.zero;
        basePlayerCtrl.SetFloat("speed", 1f);
        transPlayer.DORotate(trans.eulerAngles, time);
        transPlayer.DOMove(trans.position, time).OnComplete(()=> {
            characterController.enabled = true;
            basePlayerCtrl.SetFloat("speed", 0f);
            playerMoveThing = PlayerMoveThing.None;
            action?.Invoke();
        });
    }
    public void ForceToMove(Vector3 pos, float time, System.Action action = null) {
        playerMoveThing = PlayerMoveThing.Wait;
        characterController.enabled = false;
        basePlayerCtrl.SetFloat("speed", 1f);
        transPlayer.DOMove(pos, time).OnComplete(() => {
            characterController.enabled = true;
            basePlayerCtrl.SetFloat("speed", 0f);
            playerMoveThing = PlayerMoveThing.None;
            action?.Invoke();

        });
    }
    public void HideToDoThing(Action action) {
        basePlayerCtrl.SetActive(false);
        playerMoveThing = PlayerMoveThing.Hide;
        hideForwardAct = action;
    }
    public void MiniGameToDoThing(Action action) {
        basePlayerCtrl.SetActive(false);
        playerMoveThing = PlayerMoveThing.MiniGame;
        hideForwardAct = action;
    }
    public void ToDoThing(Transform transEnd = null) {
        if (transEnd == null) {
            hideForwardAct?.Invoke();
            transPlayer.gameObject.SetActive(true);
            playerMoveThing = PlayerMoveThing.None;
        }
        else {
            hideForwardAct?.Invoke();
            transPlayer.gameObject.SetActive(true);
            Tween tw = transPlayer.DOMove(transEnd.position, 1f);
            tw.SetEase(Ease.Linear);
            tw.OnComplete(()=> {
                playerMoveThing = PlayerMoveThing.None;
            });
        }
    }
    public virtual void Update() {
        switch (playerMoveThing) {
            case PlayerMoveThing.None:
                float moveDirLen = nowDirection.sqrMagnitude;
                float moveDirtrue = moveDirLen / 1600f;
                if (moveDirtrue > 1f) {
                    moveDirtrue = 1f;
                    playerMoveCdt = PlayerMoveCdt.Move;
                    speed = GetMoveSpeed();
                }
                else if (moveDirtrue == 0f) {
                    playerMoveCdt = PlayerMoveCdt.Idle;
                    speed = 0f;
                }
                else {
                    playerMoveCdt = PlayerMoveCdt.Move;
                    speed = GetMoveSpeed();
                }
                basePlayerCtrl.SetFloat("speed", moveDirtrue);
                basePlayerCtrl.SetFloat("movespeed", speed / StartSpeed);
                float radians = Mathf.Acos(Vector3.Dot(new Vector3(0f, 1f, 0f), (MouseDrag - MouseEnter).normalized));//--빨똑
                float degrees;
                if (MouseDrag.x < MouseEnter.x) {
                    degrees = -radians * Mathf.Rad2Deg;//--실똑
                }
                else {
                    degrees = radians * Mathf.Rad2Deg;//--실똑
                }
                Vector3 nowPos = Vector3.zero;
                if (UseRotateOffset) {
                    nowPos = Quaternion.Euler(0, RotateOffset, 0) * new Vector3(nowDirection.x, 0f, nowDirection.y);
                    transPlayer.localEulerAngles = new Vector3(0f, degrees + RotateOffset, 0f);
                }
                else {
                    nowPos = new Vector3(nowDirection.x, 0f, nowDirection.y);
                    transPlayer.localEulerAngles = new Vector3(0f, degrees, 0f);
                }
      
                characterController.Move(nowPos * speed * Time.deltaTime);
                break;
            case PlayerMoveThing.Wait:
                break;
            case PlayerMoveThing.Hide:
                if (nowDirection.sqrMagnitude == 0f) {

                }
                else {
                    ToDoThing();
                }
                break;
            case PlayerMoveThing.MiniGame:

                break;
        }
    }
}
public enum PlayerMoveCdt {
    Idle,
    Move,
}
public enum PlayerMoveThing {
    None,
    Wait,
    Hide,
    MiniGame,
}