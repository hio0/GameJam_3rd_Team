using System;
using UnityEngine;
using UnityEngine.UI;

public class PhoneDial : MonoBehaviour
{
    [SerializeField] private RectTransform plate;   // NumberPlate — P2
    [SerializeField] private RectTransform wheel;   // HolePlate — P1
    [SerializeField] private TuningBar holdBar;

    [SerializeField] private float rotateRate = 90f;    
    [SerializeField] private float tolerance = 10f;
    [SerializeField] private float holdDuration = 1f;

    [SerializeField] private Color plateNormal = new Color(0.94f, 0.45f, 0.40f);
    [SerializeField] private Color wheelNormal = Color.black;
    [SerializeField] private Color okColor = new Color(0.11f, 0.62f, 0.46f);

    private float wheelAngle = 42f;    
    private float plateAngle = -27f;
    private float holdTimer;
    private bool isFixed;

    private Image[] plateImages;
    private Image[] wheelImages;

    public event Action onFixed;

    private void OnEnable()
    {
        FixManager.fix.p1_AMove += WheelLeft;
        FixManager.fix.p1_DMove += WheelRight;
        FixManager.fix.p2_LeftMove += PlateLeft;
        FixManager.fix.p2_RightMove += PlateRight;
    }

    private void OnDisable()
    {
        if (FixManager.fix == null) return;

        FixManager.fix.p1_AMove -= WheelLeft;
        FixManager.fix.p1_DMove -= WheelRight;
        FixManager.fix.p2_LeftMove -= PlateLeft;
        FixManager.fix.p2_RightMove -= PlateRight;
    }

    // DialBuilder가 Awake에서 만든 복제본을 Start에서 수집
    private void Start()
    {
        plateImages = plate.GetComponentsInChildren<Image>();
        wheelImages = wheel.GetComponentsInChildren<Image>();
    }

    private void WheelLeft()  => wheelAngle -= rotateRate * Time.deltaTime;
    private void WheelRight() => wheelAngle += rotateRate * Time.deltaTime;
    private void PlateLeft()  => plateAngle -= rotateRate * Time.deltaTime;
    private void PlateRight() => plateAngle += rotateRate * Time.deltaTime;

    private void Update()
    {
        wheel.localRotation = Quaternion.Euler(0f, 0f, -wheelAngle);
        plate.localRotation = Quaternion.Euler(0f, 0f, -plateAngle);

        bool meshed = Mathf.Abs(Mathf.DeltaAngle(wheelAngle, plateAngle)) < tolerance;
        bool homed = Mathf.Abs(Mathf.DeltaAngle(plateAngle, 0f)) < tolerance;

        SetColor(wheelImages, meshed ? okColor : wheelNormal);
        SetColor(plateImages, homed ? okColor : plateNormal);

        if (isFixed) return;

        holdTimer = (meshed && homed) ? holdTimer + Time.deltaTime : 0f;
        holdBar.SetFill(holdTimer / holdDuration);

        if (holdTimer < holdDuration) return;

        isFixed = true;

        onFixed?.Invoke();
        FixManager.fix.ClearEvent();
        FixManager.fix.FixCompleted();
        Debug.Log("플레이어가 전화기를 수리 하였습니다... ");
    }

    private void SetColor(Image[] images, Color c)
    {
        for (int i = 0; i < images.Length; i++)
            images[i].color = c;
    }
}