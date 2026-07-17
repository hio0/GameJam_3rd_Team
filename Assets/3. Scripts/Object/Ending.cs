using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    public RippingLetter[] rippingLetters;
    public CanvasGroup player1;
    public CanvasGroup player2;

    public List<RippingLetter> completeLetters;

    public RectTransform letter;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EndingAnimation());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator EndingAnimation()
    {
        yield return new WaitForSeconds(1f);

        foreach(RippingLetter letter in rippingLetters)
        {
            letter.gameObject.SetActive(true);
            letter.Initialize(this);
        }

        /*
        yield return new WaitForSeconds(2f);

        player1.gameObject.SetActive(true);
        player2.gameObject.SetActive(true);

        bool isok = false;
        while(!isok)
        {
            if(completeLetters.Count >= 5)
            {
                foreach (CanvasGroup letter in rippingLetters)
                {
                    letter.gameObject.GetComponent<RippingLetter>().realCol.enabled = false;
                }
                isok = true;
                break;
            }
        }

        yield return new WaitForSeconds(1f);

        DOTween.DOFade(player1, 0, 1f);
        DOTween.DOFade(player2, 0, 1f);
        */

    }
}
