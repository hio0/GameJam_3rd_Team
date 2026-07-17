using UnityEngine;

public class RadioTuner : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioDistortionFilter distortionFilter;

    [SerializeField] private float pitchStep = 0.1f;
    [SerializeField] private float distortionStep = 0.1f;
    [SerializeField] private float tolerance = 0.05f;
    bool isRadioScene;
    private float currentPitch = 1.5f;    
    private float currentDistortion = 0.5f;
    void OnEnable()
    {
        FixManager.fix.p1_AMove += OnP1Pressed;
    }
    void OnDisable()
    {
        FixManager.fix.p1_AMove -= OnP1Pressed;
    }
    void Start()
    {
        Apply();
    }

    public void OnP1Pressed()
    {
        currentPitch += pitchStep;
        if (currentPitch > 2f) currentPitch = 0.5f;
        Debug.Log($"{currentPitch}");
        
        Apply();
    }


    public void OnP2Pressed()
    {
        currentDistortion += distortionStep;
        if (currentDistortion > 1f) currentDistortion = 0f;
        Debug.Log($"{currentDistortion}");
        Apply();
    }

    private void Apply()
    {
        audioSource.pitch = currentPitch;
        distortionFilter.distortionLevel = currentDistortion;
        CheckWin();
    }

    private void CheckWin()
    {
        bool pitchOk = Mathf.Abs(currentPitch - 1f) < tolerance;
        bool distortionOk = currentDistortion < tolerance;

        if (pitchOk && distortionOk)
        {
            Debug.Log("튜닝 성공!");
        }
    }

    void Update()
    {
        if (isRadioScene)
        {
            
        }
    } 
}