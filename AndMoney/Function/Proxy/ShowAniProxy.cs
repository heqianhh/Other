using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAniProxy : MonoBehaviour {
    float MaxScale = 1.5f;
    float time = 0.2f;
    Sequence sequence;
    Vector3 initScale;
    public void Awake() {
        initScale = Vector3.one;
    }
    public void OnEnable() {
        Func();
    }
    public void Func() {
        if (!gameObject.activeSelf) {
            return;
        }
        sequence.Kill();
        sequence = DOTween.Sequence();
        sequence.Insert(0f, transform.DOScale(initScale * MaxScale, time / 2f));
        sequence.Insert(time / 2f, transform.DOScale(initScale, time / 2f));
    }
}
