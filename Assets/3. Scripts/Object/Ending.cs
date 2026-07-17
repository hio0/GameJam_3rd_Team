using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.Image;

public class Ending : MonoBehaviour
{
    public static Ending ending;

    public RippingLetter[] rippingLetters;
    public CanvasGroup correct;
    public GameObject player1;
    public GameObject player2;
    
    public event Action OnPlayerOut;

    public List<RippingLetter> completeLetters;

    public CanvasGroup letterP;
    public CanvasGroup button;
    public GameObject creditP;
    public GameObject letter;
    public CanvasGroup creditB;
    public RectTransform credit;
    public RectTransform photo;

    private void Awake()
    {
        ending = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EndingAnimation());

        letterP.alpha = 0;
        creditP.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            foreach (RippingLetter letter in rippingLetters)
            {
                letter.TargetLock();
            }

            StartCoroutine(ForYouLetter());
        }
    }

    IEnumerator EndingAnimation()
    {
        yield return new WaitForSeconds(1f);

        foreach (RippingLetter letter in rippingLetters)
        {
            letter.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(1.5f);

        player1.gameObject.SetActive(true);
        player2.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        DOTween.DOFade(correct, 1, 5f);

        bool isok = false;
        while (!isok)
        {
            if (completeLetters.Count >= 5)
            {
                isok = true;
                StartCoroutine(ForYouLetter());
                break;
            }

            yield return null;
        }
    }


    IEnumerator ForYouLetter()
    {
        yield return new WaitForSeconds(1f);

        OnPlayerOut?.Invoke();

        yield return new WaitForSeconds(1.5f);

        float speed = 0.5f;
        DOTween.DOFade(letterP, 1, speed);

        yield return new WaitForSeconds(speed + 1.5f);

        button.gameObject.SetActive(true);
        DOTween.DOFade(button, 1, 1f);
    }

    public void LatterOpen()
    {
        StartCoroutine(LetterRead());
    }

    IEnumerator LetterRead()
    {
        float fin = 0.5f;
        FadeObject.fade.FadeOut(fin);

        yield return new WaitForSeconds(fin + 1.5f);

        FadeObject.fade.FadeIn(fin);

        creditP.gameObject.SetActive(true);

        float timer = 0;
        float duration = 30f;

        RectTransform latter = letter.GetComponent<RectTransform>();

        while (true)
        {
            timer += Time.deltaTime;

            float t = Mathf.Clamp01(timer / duration);

            // 감속 곡선 (처음 빠르고 끝에서 느려짐)
            float smoothT = Mathf.SmoothStep(0, 1, t);

            // 이동
            latter.anchoredPosition = Vector3.Lerp(
                latter.anchoredPosition,
                new Vector2(437.9f, 0),
                smoothT
            );

            // Z축 회전
            float z = Mathf.LerpAngle(
                latter.eulerAngles.z,
                -4.58f,
                smoothT
            );

            latter.rotation = Quaternion.Euler(0, 0, z);

            if (t >= 1f)
            {
                break;
            }

            yield return null;
        }

        Vector2 target = new Vector2(0, -359.82f);
        while ((photo.anchoredPosition - target).sqrMagnitude > 0.001)
        {
            float y = Mathf.Lerp(photo.anchoredPosition.y, target.y, Time.deltaTime * 3f);

            photo.anchoredPosition = new Vector2(photo.anchoredPosition.x, y);
            yield return null;
        }

        yield return new WaitForSeconds(10f);

        DOTween.DOFade(creditB, 1, 1f);
    }

    public void ExitToMain()
    {
        StartCoroutine(ExitAnimation("Loading"));
    }

    IEnumerator ExitAnimation(string sceneName)
    {
        float speed = 0.5f;
        FadeObject.fade.FadeOut(speed);

        yield return new WaitForSeconds(speed + 1.5f);

        SceneMove.LoadScene(sceneName);
    }
}
