using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DOTween
{ 
    public static void DOShaking(Transform what, float duration, float strength, int vibrato, int randomess)
    {
        what.DOShakePosition(duration, strength, vibrato, randomess);
    }

    public static void DOFade(CanvasGroup what, float howmuch, float time)
    {
        what.DOFade(howmuch, time);
    }
}
