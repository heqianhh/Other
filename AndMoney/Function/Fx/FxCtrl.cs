using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxCtrl : MonoBehaviour {
    public List<ParticleSystem> particleSystems = new List<ParticleSystem>();
    public Coroutine COCountDown;
    Action action;
    public void OnPlay() {
        gameObject.SetActive(true);
        foreach (ParticleSystem temp in particleSystems) {
            temp.gameObject.SetActive(true);
            temp.Play();
        }
    }
    public void OnStop() {
        gameObject.SetActive(false);
        foreach (ParticleSystem temp in particleSystems) {
            temp.Stop();
            temp.gameObject.SetActive(false);

        }
    }
    public void OnPlayToEnd(float time) {
        OnPlay();
        ToolMgr.Instance.DelayCallBack(OnStop, time);
    }
    public void StartPlayToEnd(float time, Action action) {
        this.action = action;
        COCountDown = CoDelegator.Coroutine(OnPlayToEnd(time, action));
    }
    public IEnumerator OnPlayToEnd(float time, Action action) {

        OnPlay();
        yield return new WaitForSeconds(time);
        action?.Invoke();
        this.action = null;
        OnStop();

    }
    public IEnumerator OnPlayToEnd(float time, Action midAct, Action endAct) {
        OnPlay();
        yield return null;

        midAct.Invoke();
        yield return new WaitForSeconds(time);
        endAct.Invoke();
        OnStop();

    }
    public void OnPlayerEndEarly() {
        StopCoutDown();
    }
    private Coroutine StartCoutDown(System.Action callBack, float delayTime) {
        COCountDown = StartCoroutine(CoDelayCallBack(callBack, delayTime));
        return COCountDown;
    }
    public void StopCoutDown() {
        if (COCountDown != null) {
            action?.Invoke();
            action = null;
            CoDelegator.StopCoroutineEx(COCountDown);
        }
    }

    private IEnumerator CoDelayCallBack(System.Action callBack, float delayTime) {
        yield return new WaitForSeconds(delayTime);
        callBack?.Invoke();
        COCountDown = null;
    }
    public void OnEmission(bool active) {
        foreach (ParticleSystem temp in particleSystems) {
            ParticleSystem.EmissionModule temp2 = temp.emission;
            temp2.enabled = active;
        }
    }
}
