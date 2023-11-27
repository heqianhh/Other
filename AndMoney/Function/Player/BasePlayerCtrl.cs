using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayerCtrl : MonoBehaviour
{
    public Animator ani;
    string LastTrigger;
    bool IsCarry;
    string strNamenow;
    float floatnow;
    
    public virtual void OnStart() {
        LastTrigger = "";
        IsCarry = false;
        strNamenow = "";
        floatnow = 0f;
    }
    public void SetActive(bool active) {
        gameObject.SetActive(active);
        LastTrigger = null;
        IsCarry = false;
        strNamenow = "";
        floatnow = 0f;
    }
    public void SetTrigger(string trigger) {
        if (!string.IsNullOrEmpty(LastTrigger))
            ani.ResetTrigger(LastTrigger);

        ani.SetTrigger(trigger);
        LastTrigger = trigger;
    }
    public virtual void SetFloat(string strName, float floatValue) {
        if (ani == null) {
            return;
        }
        if (strNamenow != strName) {
            strNamenow = strName;
            floatnow = floatValue;
            ani.SetFloat(strName, floatValue);
        }
        else {
            if (floatnow == floatValue) {

            }
            else {
                floatnow = floatValue;
                ani.SetFloat(strName, floatValue);
            }
        }

    }

    public virtual void SetCarry(bool active) {
        if (IsCarry == active) {
            return;
        }
        else {
            IsCarry = active;
            ani.SetBool("Carry", active);
        }
    }
}
