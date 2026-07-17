using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public Transform hArrow;
    public Transform mArrow;
    public Transform sArrow;

    public float sSpeed;
    public float mMoveLengh;
    public float hMoveLengh;

    float mMoveCount;
    bool isfinish;

    // Start is called before the first frame update
    void Start()
    {
        mMoveCount = UnityEngine.Random.Range(0, 61);
        isfinish = false;

        Action mmtr = () => MMove(true);
        FixManager.fix.p1_DMove += mmtr;

        Action mmfl = () => MMove(false);
        FixManager.fix.p1_DMove += mmtr;

        StartCoroutine(SMove());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SMove()
    {
        bool isact = false;

        while(!isfinish)
        {
            sArrow.Rotate(new Vector3(0, 0, -sSpeed * Time.deltaTime));
            if (sArrow.rotation.z < 0)
            {
                isact = false;
            }
            else if (Mathf.Round(sArrow.rotation.z) == 0 && !isact)
            {
                MMove(false);
                isact = true;
            }

            yield return null;
        }
    }

    void MMove(bool isback)
    {
        float movelengh = mMoveLengh;
        if(isback)
        {
            movelengh = -mMoveLengh;
        }

        mArrow.Rotate(new Vector3(0, 0, -mMoveLengh * Time.deltaTime));
        mMoveCount++;

        if(mMoveCount >= 60)
        {
            HMove(false);
        }
    }

    void HMove(bool isback)
    {
        float movelengh = hMoveLengh;
        if (isback)
        {
            movelengh = -hMoveLengh;
        }

        mArrow.Rotate(new Vector3(0, 0, -hMoveLengh * Time.deltaTime));
    }
}
