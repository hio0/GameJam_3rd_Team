using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FixCompleteObject : MonoBehaviour
{
    public static FixCompleteObject fixcomplete;

    public CanvasGroup buttons;

    CanvasGroup can;

    private void Awake()
    {
        fixcomplete = this;
        can = GetComponent<CanvasGroup>();
    }

    // Start is called before the first frame update
    void Start()
    {
        can.alpha = 0;
        buttons.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Completed()
    {
        StartCoroutine(CPAnimation());
    }

    IEnumerator CPAnimation()
    {
        float speed = 0.5f;
        DOTween.DOFade(can, 1, speed);

        yield return new WaitForSeconds(speed + 1.5f);

        buttons.gameObject.SetActive(true);
        DOTween.DOFade(buttons, 1, 1f);
    }

    public void Exit()
    {
        StartCoroutine(ExitAnimation());
    }

    IEnumerator ExitAnimation()
    {
        float speed = 0.5f;
        FadeObject.fade.FadeOut(speed);
        yield return new WaitForSeconds(speed + 1.5f);

        SceneMove.LoadScene("Loading");
    }
}
