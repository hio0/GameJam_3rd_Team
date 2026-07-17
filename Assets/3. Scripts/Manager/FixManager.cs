using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixManager : MonoBehaviour
{
    public static FixManager fix;

    public event Action p1_AMove;
    public event Action p1_DMove;
    public event Action p2_LeftMove;
    public event Action p2_RightMove;

    public CanvasGroup pre_completeP;

    private void Awake()
    {
        fix = this;
        ClearEvent();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            p1_AMove?.Invoke();
        }
        if (Input.GetKey(KeyCode.D))
        {
            p1_DMove?.Invoke();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            p2_LeftMove?.Invoke();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            p2_RightMove?.Invoke();
        }
    }

    void ClearEvent()
    {
        p1_AMove = null;
        p1_DMove = null;
        p2_LeftMove = null;
        p2_RightMove = null;
    }

    public void FixCompleted()
    {
        DOTween.DOFade(pre_completeP, 1, 0.3f);
    }
}
