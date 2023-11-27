using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniProxy : MonoBehaviour
{
    public Action<string> AniAction;

    private void AniTrigger(string str) {
        AniAction?.Invoke(str);
    }
}
