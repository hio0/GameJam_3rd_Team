using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public bool mComplete;
    public bool hComplete;

    float comcount;
    public Image comfill;

    // Start is called before the first frame update
    void Start()
    {
        FadeObject.fade.FadeIn(1.5f);
        SoundManager.soundManager.clockTickSFX.Play();
        SoundManager.soundManager.clockTickSFX.loop = true;
        SoundManager.soundManager.StartCoroutine(SoundManager.soundManager.FadeInVol(SoundManager.soundManager.clockTickSFX));
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

        comcount = 2f;
        comfill.transform.parent.gameObject.SetActive(false);

        StartCoroutine(SMove());
    }

    // Update is called once per frame
    void Update()
    {
        if(mComplete && hComplete && !isfinish)
        {
            comcount -= Time.deltaTime;
            comfill.transform.parent.gameObject.SetActive(true);
            comfill.fillAmount = comcount / 2f;

            if (comcount < 0)
            {
                comcount = 0;
                FixManager.fix.FixCompleted();
            }
        }
        else
        {
            comcount = 2f;
            comfill.transform.parent.gameObject.SetActive(false);
        }
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
            mMoveCount += 1 * gajunchi;
        }

        mArrow.Rotate(Vector3.forward, movelengh);

        if(mMoveCount >= 60 * mMoveLengh)
        {
            HMove(false, -120);
            mMoveCount = 0;
        }

        if(Mathf.Abs(Mathf.DeltaAngle(mArrow.eulerAngles.z, 100f)) < 5f)
        {
            mComplete = true;
            FixManager.fix.ClearEvent();
            SoundManager.soundManager.clockTickSFX.Stop();
        }
        else
        {
            mComplete = false;
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

        if (Mathf.Abs(Mathf.DeltaAngle(hArrow.eulerAngles.z, 140f)) < 5f)
        {
           hComplete = true;
        }
        else
        {
            hComplete = false;
        }
    }
}
