using System;
using UnityEngine;

public class MelodyParticleFeedback : MonoBehaviour
{
    [SerializeField] private Radio radio;
    [SerializeField] private ParticleSystem Melody;
    [SerializeField] private Color farColor = new Color(0.85f, 0.25f, 0.2f);
    [SerializeField] private Color closeColor = new Color(0.2f, 0.8f, 0.5f);

    // 시각 표현만 — 로직 없음
    private void Update()
    {
        if (radio.IsTuned) 
        {
            Melody.Stop();
            
            enabled = false;
            return;
        }
        float accuracy = Mathf.Min(radio.PitchAccuracy, radio.DistortionAccuracy);

        var main = Melody.main;
        main.startColor = Color.Lerp(farColor, closeColor, accuracy);
        
    }
    
}
