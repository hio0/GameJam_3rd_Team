using System;
using UnityEngine;
using UnityEngine.UI;

public class PhoneDial : MonoBehaviour
{
    [SerializeField] private RectTransform plate;   // NumberPlate — P2
    [SerializeField] private RectTransform wheel;   // HoleWheel — P1
    [SerializeField] private TuningBar holdBar;

    [SerializeField] private float rotateRate = 90f;
    [SerializeField] private float tolerance = 12f;
    [SerializeField] private float holdDuration = 3f;

    [SerializeField] private float wheelDrift = 25f;
    [SerializeField] private float plateDrift = -18f;
    [SerializeField] private float drainRate = 2f;

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
    public bool IsFixed => isFixed;

    private void OnEnable()
    {
        
    }

    private void OnDisable() => UnSubscribe();

    // DialBuilder가 Awake에서 만든 복제본을 Start에서 수집
    private void Start()
    {
        plateImages = plate.GetComponentsInChildren<Image>();
        wheelImages = wheel.GetComponentsInChildren<Image>();

        FixManager.fix.p1_AMove += WheelLeft;
        FixManager.fix.p1_DMove += WheelRight;
        FixManager.fix.p2_LeftMove += PlateLeft;
        FixManager.fix.p2_RightMove += PlateRight;
    }

    private void WheelLeft()  => wheelAngle -= rotateRate * Time.deltaTime;
    private void WheelRight() => wheelAngle += rotateRate * Time.deltaTime;
    private void PlateLeft()  => plateAngle -= rotateRate * Time.deltaTime;
    private void PlateRight() => plateAngle += rotateRate * Time.deltaTime;

    private void Update()
    {
        if (!isFixed)
        {
            wheelAngle += wheelDrift * Time.deltaTime;
            plateAngle += plateDrift * Time.deltaTime;
        }

        wheel.localRotation = Quaternion.Euler(0f, 0f, -wheelAngle);
        plate.localRotation = Quaternion.Euler(0f, 0f, -plateAngle);

        bool meshed = Mathf.Abs(Mathf.DeltaAngle(wheelAngle, plateAngle)) < tolerance;
        bool homed = Mathf.Abs(Mathf.DeltaAngle(plateAngle, 0f)) < tolerance;

        SetColor(wheelImages, meshed ? okColor : wheelNormal);
        SetColor(plateImages, homed ? okColor : plateNormal);

        if (isFixed) return;

        holdTimer = (meshed && homed)
            ? holdTimer + Time.deltaTime
            : Mathf.Max(0f, holdTimer - Time.deltaTime * drainRate);

        holdBar.SetFill(holdTimer / holdDuration);

        if (holdTimer < holdDuration) return;

        isFixed = true;
        UnSubscribe();
        onFixed?.Invoke();

        if (FixManager.fix != null)
            FixManager.fix.FixCompleted();

        Debug.Log("플레이어가 전화기를 수리 하였습니다... ");
    }

    private void SetColor(Image[] images, Color c)
    {
        for (int i = 0; i < images.Length; i++)
            images[i].color = c;
    }

    private void UnSubscribe()
    {
        if (FixManager.fix == null) return;

        FixManager.fix.p1_AMove -= WheelLeft;
        FixManager.fix.p1_DMove -= WheelRight;
        FixManager.fix.p2_LeftMove -= PlateLeft;
        FixManager.fix.p2_RightMove -= PlateRight;
    }
}