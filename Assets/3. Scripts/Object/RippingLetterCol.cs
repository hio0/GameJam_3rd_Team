using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippingLetterCol : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("RipLetter"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.GetComponent<Collider2D>());
        }
    }
}
