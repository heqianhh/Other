using AndMoney;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FxCoinProxy : MonoBehaviour {

    private Image imgCoin;

    private Vector3 spawnPos;

    private Vector3 flyPos;
    private Vector3 targetPos;

    private Tweener tweener;

    private float speed = 10f;

    public void Init(string _iconStr = "Coin05") {
        if (imgCoin == null) {
            imgCoin = gameObject.GetComponent<Image>();
        }

        imgCoin.SetSprite(_iconStr);
        gameObject.SetActive(true);

        InitMsg();
    }

    public void Clear() {
        tweener?.Kill();
        ObjectPool.Instance.Recycle(gameObject);

        ClearMsg();
    }

    private void InitMsg() {

    }

    private void ClearMsg() {

    }

    public void Spawn(Vector3 _spawnPos, Vector3 _targetPos, float _radius, float size = 1.0f) {
        transform.position = _spawnPos;
        spawnPos = _spawnPos;
        targetPos = _targetPos;
        transform.localScale = Vector3.one * size;

        flyPos = spawnPos;
        if (_radius != 0) {
            flyPos = EMath.GetRotPos(spawnPos + new Vector3(0, _radius, 0), spawnPos, ToolMgr.Instance.RangeWithMax(0, 360), false);
        }

        tweener = transform.DOMove(flyPos, Vector3.Distance(spawnPos, flyPos) / 2).OnComplete(() => {
            Collect();
        });
    }
    public void Collect(Vector3 _spawnPos, Vector3 _targetPos, float size = 1.0f) {
        transform.position = _spawnPos;
        targetPos = _targetPos;
        transform.localScale = Vector3.one * size;
        Collect();
    }

    public void Collect() {
        tweener?.Kill();
        tweener = transform.DOMove(targetPos, Vector3.Distance(transform.position, targetPos) / speed).SetEase(Ease.Linear).OnComplete(() => {
            Clear();
        });
    }
}
