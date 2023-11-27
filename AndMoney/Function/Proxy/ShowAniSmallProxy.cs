using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ShowAniSmallProxy : MonoBehaviour {
    float MinScale = 0.2f;
    float time = 0.5f;
    Sequence sequence;
    Vector3 initScale;
    public void Awake() {
        initScale = transform.localScale;
    }
    public void OnEnable() {
        Func();
    }
    public void Func() {
        sequence.Kill();
        sequence = DOTween.Sequence();
        transform.localScale = initScale * MinScale;
        sequence.Insert(0f, transform.DOScale(initScale, time));
    }
}
