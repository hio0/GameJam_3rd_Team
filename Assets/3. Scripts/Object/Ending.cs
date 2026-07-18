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
    public float photoDuration;
    public event Action OnPhotoMove;

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
        Vector2 letterEnd = new Vector2(437.9f, 0);

        while (true)
        {
        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / duration);
        float smoothT = Mathf.SmoothStep(0, 1, t);

        latter.anchoredPosition = Vector3.Lerp(latter.anchoredPosition, letterEnd, smoothT);
        float z = Mathf.LerpAngle(latter.eulerAngles.z, -4.58f, smoothT);
        latter.rotation = Quaternion.Euler(0, 0, z);

        if (Vector2.Distance(latter.anchoredPosition, letterEnd) < 0.5f || t >= 1f)
        {
            latter.anchoredPosition = letterEnd;
            latter.rotation = Quaternion.Euler(0, 0, -4.58f);
            OnPhotoMove?.Invoke();
            break;
        }

        yield return null;
    }

        
    Debug.Log("fdf");
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
