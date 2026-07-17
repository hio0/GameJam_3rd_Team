using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DialTurn : MonoBehaviour
{
    [SerializeField] private GameObject pitch_Dial;
    [SerializeField] private GameObject distoration_Dial;
    [SerializeField] Radio radioScript;

    
    
    void OnEnable()
    {
        FixManager.fix.p1_AMove += LeftRotatePitchDial;
        FixManager.fix.p1_DMove += RightRotatePitchDial;
        FixManager.fix.p2_LeftMove += LeftRotateDistorationDial;
        FixManager.fix.p2_RightMove += RightRotateDistoration_Dial;
    }
    void OnDisable()
    {
        FixManager.fix.p1_AMove -= LeftRotatePitchDial;
        FixManager.fix.p1_DMove -= RightRotatePitchDial;
        FixManager.fix.p2_LeftMove -= LeftRotateDistorationDial;
        FixManager.fix.p2_RightMove -= RightRotateDistoration_Dial;
    }

    // Start is called before the first frame update
    void LeftRotatePitchDial()=> pitch_Dial.transform.Rotate(0,0, -(radioScript.pitchRate*400)*Time.deltaTime);
        
    
    void LeftRotateDistorationDial() => distoration_Dial.transform.Rotate(0,0, -(radioScript.distortionRate*400)*Time.deltaTime);
    
    void RightRotatePitchDial() => pitch_Dial.transform.Rotate(0,0, (radioScript.pitchRate*400)*Time.deltaTime);
    
    
    void RightRotateDistoration_Dial() => distoration_Dial.transform.Rotate(0,0, (radioScript.distortionRate*400)*Time.deltaTime);
    // Update is called once per frame
}
