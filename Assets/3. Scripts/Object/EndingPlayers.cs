using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingPlayers : MonoBehaviour
{
    RectTransform rec;
    CanvasGroup can;

    public bool isplayer1;

    Vector2 move1;
    Vector2 move2;

    public float movespeed;
    public bool isstop;

    // Start is called before the first frame update
    void Start()
    {
        rec = GetComponent<RectTransform>();
        can = GetComponent<CanvasGroup>();

        can.alpha = 0;

        DOTween.DOFade(can, 1, 1.5f);

        Action act = () => StartCoroutine(Disappier());
        Ending.ending.OnPlayerOut += act;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isstop)
        {
            if (isplayer1)
            {
                move1 = Vector2.zero;

                if (Input.GetKey(KeyCode.W))
                    move1.y += 1;

                if (Input.GetKey(KeyCode.S))
                    move1.y -= 1;

                if (Input.GetKey(KeyCode.A))
                    move1.x -= 1;

                if (Input.GetKey(KeyCode.D))
                    move1.x += 1;

                rec.anchoredPosition += move1.normalized * movespeed * Time.deltaTime;
            }
            else
            {
                move2 = Vector2.zero;

                if (Input.GetKey(KeyCode.UpArrow))
                    move2.y += 1;

                if (Input.GetKey(KeyCode.DownArrow))
                    move2.y -= 1;

                if (Input.GetKey(KeyCode.LeftArrow))
                    move2.x -= 1;

                if (Input.GetKey(KeyCode.RightArrow))
                    move2.x += 1;

                rec.anchoredPosition += move2.normalized * movespeed * Time.deltaTime;
            }
        }
    }

    IEnumerator Disappier()
    {
        float speed = 1f;

        DOTween.DOFade(can, 0f, speed);

        yield return new WaitForSeconds(speed + 0.2f);

        Destroy(gameObject);
    }
}
