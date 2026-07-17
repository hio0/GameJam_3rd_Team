using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public RectTransform hArrow;
    public RectTransform mArrow;
    public RectTransform sArrow;

    public float sSpeed;
    public float mMoveLengh;
    public float hMoveLengh;

    float mMoveCount;
    bool isfinish;

    // Start is called before the first frame update
    void Start()
    {
        FadeObject.fade.FadeIn(1.5f);

        mMoveCount = UnityEngine.Random.Range(0, 61 * mMoveLengh);
        isfinish = false;

        Action mmtr = () => MMove(true, 1);
        FixManager.fix.p1_DMove += mmtr;

        Action mmfl = () => MMove(false, 1);
        FixManager.fix.p1_AMove += mmfl;

        Action hmtr = () => HMove(true, 1);
        FixManager.fix.p2_RightMove += hmtr;

        Action hmfl = () => HMove(false, 1);
        FixManager.fix.p2_LeftMove += hmfl;

        StartCoroutine(SMove());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SMove()
    {
        float prevAngle = 0;
        bool iscat = false;

        while(!isfinish)
        {
            sArrow.Rotate(Vector3.forward, -sSpeed * Time.deltaTime);

            float angle = sArrow.eulerAngles.z;

            if (prevAngle > 1f && angle < 1f && !iscat)
            {
                MMove(false, -50);
                iscat = true;
            }
            else
            {
                iscat = false;
            }

            prevAngle = angle;

            yield return null;
        }
    }

    void MMove(bool isback, float gajunchi)
    {
        float movelengh = mMoveLengh * gajunchi * Time.deltaTime;
        if(isback)
        {
            movelengh = -movelengh;
        }

        mArrow.Rotate(Vector3.forward, movelengh);
        mMoveCount++;

        if(mMoveCount >= 60 * mMoveLengh)
        {
            HMove(false, -120);
            mMoveCount = 0;
        }
    }

    void HMove(bool isback, float gajunchi)
    {
        float movelengh = hMoveLengh * gajunchi * Time.deltaTime;
        if (isback)
        {
            movelengh = -movelengh;
        }

        hArrow.Rotate(Vector3.forward, movelengh);
    }
}
