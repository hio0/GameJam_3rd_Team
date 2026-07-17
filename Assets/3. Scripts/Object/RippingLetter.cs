using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class RippingLetter : MonoBehaviour
{
    public Ending ending;
    CanvasGroup can;
    List<RippingLetter> others;
    public CircleCollider2D realCol;

    public void Initialize(Ending ending)
    {
        this.ending = ending;
    }

    private void OnEnable()
    {
        can = GetComponent<CanvasGroup>();

        float r = Random.Range(0.5f, 2f);
        DOTween.DOFade(can, 1, r);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.TryGetComponent<RippingLetter>(out RippingLetter letter);

        if(letter != null)
        {
            others.Add(letter);
        }

        if (others.Count >= 5)
        {
            ending.completeLetters.Add(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        other.gameObject.TryGetComponent<RippingLetter>(out RippingLetter letter);

        if (letter != null)
        {
            others.Remove(letter);
        }

        if (others.Count >= 5)
        {
            ending.completeLetters.Remove(this);
        }
    }
}
