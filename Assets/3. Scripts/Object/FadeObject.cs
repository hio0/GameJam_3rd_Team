using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeObject : MonoBehaviour
{
    public static FadeObject fade;

    CanvasGroup can;

    private void Awake()
    {
        fade = this;
        can = GetComponent<CanvasGroup>();
    }

    // Start is called before the first frame update
    void Start()
    { 
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeIn(float speed)
    {
        can.alpha = 1f;
        DOTween.DOFade(can, 0, speed);
    }

    public void FadeOut(float speed)
    {
        can.alpha = 0f;
        DOTween.DOFade(can, 1, speed);
    }
}
