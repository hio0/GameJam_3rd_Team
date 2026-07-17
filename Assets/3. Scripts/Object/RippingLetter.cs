using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class RippingLetter : MonoBehaviour
{
    CanvasGroup can;
    RectTransform rec;
    GameObject col;

    [SerializeField] Vector2 target;
    bool notyetposied;

    // Start is called before the first frame update
    void Start()
    {
        can = GetComponent<CanvasGroup>();
        can.alpha = 0;
        rec = GetComponent<RectTransform>();
        col = transform.GetChild(0).gameObject;

        notyetposied = true;

        float r = Random.Range(0.5f, 2f);
        DOTween.DOFade(can, 1, r);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(rec.anchoredPosition, target) <= 10f && notyetposied)
        {
            TargetLock();
        }
    }

    public void TargetLock()
    {
        rec.anchoredPosition = target;
        col.SetActive(false);
        GetComponent<Image>().color = new Color(255f, 255f, 255f);

        Ending.ending.completeLetters.Add(this);
        notyetposied = false;
    }
}
