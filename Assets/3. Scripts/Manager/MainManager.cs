using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FadeObject.fade.FadeIn(1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FixObject(string sceneName)
    {
        StartCoroutine(GoToFixScene(sceneName));
    }

    IEnumerator GoToFixScene(string sceneName)
    {
        float speed = 1.5f;

        FadeObject.fade.FadeOut(speed);
        
        yield return new WaitForSeconds(speed + 1f);

        SceneMove.LoadScene(sceneName);
    }
}
