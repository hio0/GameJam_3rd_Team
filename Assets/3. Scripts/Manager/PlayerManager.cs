using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager player;

    public bool isfixedScene;

    public event Action p1_AMove;
    public event Action p1_DMove;
    public event Action p2_LeftMove;
    public event Action p2_RightMove;

    private void Awake()
    {
        player = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isfixedScene)
        {
            if (Input.GetKey(KeyCode.A))
            {
                p1_AMove?.Invoke();
            }
            if(Input.GetKey(KeyCode.D))
            {
                p1_DMove?.Invoke();
            }

            if(Input.GetKey(KeyCode.LeftArrow))
            {
                p2_LeftMove?.Invoke();
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                p2_RightMove?.Invoke();
            }
        }
    }
}