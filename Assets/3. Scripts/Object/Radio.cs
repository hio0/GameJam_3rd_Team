using System;
using System.Collections;
using UnityEngine;

public class Radio : MonoBehaviour
{
    [SerializeField] private AudioSource RadioAudio;
    [SerializeField] private AudioDistortionFilter distortionFilter;
    [SerializeField] private TuningBar tuningBar;


    [SerializeField] public float pitchRate = 0.4f;
    [SerializeField] public float distortionRate = 0.5f;
    [SerializeField] private float pitchErrorRange = 0.5f;
    [SerializeField] private float winThreshold = 0.99f;

    private float currentPitch = 1.7f;
    private float currentDistortion = 0.9f;
    private bool isTuned;

    public event Action onTuned;
    public bool IsTuned => isTuned;

    public float PitchAccuracy => 1f - Mathf.Clamp01(Mathf.Abs(currentPitch - 1f) / pitchErrorRange);
    public float DistortionAccuracy => 1f - currentDistortion;

    private void OnEnable()
    {
        FixManager.fix.p1_AMove += PitchDown;
        FixManager.fix.p1_DMove += PitchUp;
        FixManager.fix.p2_LeftMove += DistortionDown;
        FixManager.fix.p2_RightMove += DistortionUp;
    }

    private void OnDisable() => UnSubscribe();

    private void Start() => Apply();
    private void Awake()
    {


    } 
    private void PitchUp()   => SetPitch(currentPitch + pitchRate * Time.deltaTime);
    private void PitchDown() => SetPitch(currentPitch - pitchRate * Time.deltaTime);
    private void DistortionUp()   => SetDistortion(currentDistortion + distortionRate * Time.deltaTime);
    private void DistortionDown() => SetDistortion(currentDistortion - distortionRate * Time.deltaTime);

    private void SetPitch(float value)
    {
        currentPitch = Mathf.Clamp(value, 0.5f, 2f);
        Apply();
    }

    private void SetDistortion(float value)
    {
        currentDistortion = Mathf.Clamp01(value);
        Apply();
    }

    private void Apply()
    {
        RadioAudio.pitch = currentPitch;
        distortionFilter.distortionLevel = currentDistortion;
        tuningBar.SetFill(Mathf.Min(PitchAccuracy, DistortionAccuracy));
        CheckWin();
    }
    
    private void CheckWin()
    {
        if (isTuned) return;
        if (PitchAccuracy < winThreshold || DistortionAccuracy < winThreshold) return;
        UnSubscribe();
        isTuned = true;
        onTuned?.Invoke();
        SoundManager.soundManager.StartCoroutine(SoundManager.soundManager.FadeOutVol(RadioAudio));
        StartCoroutine(CompleteAfterDelay());
    }
    private void UnSubscribe()
    {
        if (FixManager.fix == null) return;

        FixManager.fix.p1_AMove -= PitchDown;
        FixManager.fix.p1_DMove -= PitchUp;
        FixManager.fix.p2_LeftMove -= DistortionDown;
        FixManager.fix.p2_RightMove -= DistortionUp;
    }
    private IEnumerator CompleteAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        FixManager.fix.FixCompleted();
        Debug.Log("튜닝 성공.");
    }
}