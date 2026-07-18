using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Action act = () => StartCoroutine(Move());
        Ending.ending.OnPhotoMove += act;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Move()
    {
    RectTransform photo = GetComponent<RectTransform>();

        Vector2 target = new Vector2(0, -359.82f);
        while ((photo.anchoredPosition - target).sqrMagnitude > 0.001)
        {
            float y = Mathf.Lerp(photo.anchoredPosition.y, target.y, Time.deltaTime * 3f);

            photo.anchoredPosition = new Vector2(photo.anchoredPosition.x, y);
            yield return null;
        }
    }
}
