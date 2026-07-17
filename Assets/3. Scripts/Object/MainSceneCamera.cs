using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneCamera : MonoBehaviour
{
    public float bojung;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 center = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        Vector2 delta = (Vector2)Input.mousePosition - center;

        float x = Mathf.Clamp(delta.y / bojung, -4f, 4f);
        float y = Mathf.Clamp(delta.x / bojung, -3f, 3f);

        transform.rotation = Quaternion.Euler(-x, y, 0);
    }
}
