using System;
using UnityEngine;
using UnityEngine.UI;

public class Guitar : MonoBehaviour
{
    [SerializeField] private AudioSource song;
    [SerializeField] private TuningBar progressBar;

    [SerializeField] private RectTransform pick;          // P2가 위아래로 훑음
    [SerializeField] private RectTransform chordMarker;   // P1이 좌우로 옮김
    [SerializeField] private RectTransform targetMarker;  // 목표 코드 표시
    [SerializeField] private Image chordImage;            // chordMarker의 Image

    [SerializeField] private float strumRate = 500f;      // 픽셀/초
    [SerializeField] private float pickRange = 90;      // 위아래 한계
    [SerializeField] private float strumThreshold = 100f; // 이 속도 이상이어야 연주로 인정

    [SerializeField] private float chordRate = 2f;        // 코드칸/초
    [SerializeField] private float chordTolerance = 0.35f;
    [SerializeField] private float neckMinY = -220f;
    [SerializeField] private float neckMaxY = 40f;

    [SerializeField] private float chordDuration = 4f;
    [SerializeField] private int[] chordSequence = { 0, 2, 1, 3 };
    [SerializeField] private int chordCount = 4;
    [SerializeField] private float graceTime = 0.2f;

    [SerializeField] private Color chordNormal = Color.white;
    [SerializeField] private Color okColor = new Color(0.11f, 0.62f, 0.46f);

    private float pickPos;
    private float lastPickPos;
    private float chordPos;
    private float graceTimer;
    private bool isFixed;

    public event Action onFixed;
    public bool IsFixed => isFixed;

    private int TargetChord =>
        chordSequence[Mathf.FloorToInt(song.time / chordDuration) % chordSequence.Length];

    private void OnEnable()
    {
        FixManager.fix.p1_AMove += CordUp;
        FixManager.fix.p1_DMove += CordDown;
        FixManager.fix.p2_LeftMove += PickLeft;
        FixManager.fix.p2_RightMove += PickRight;
    }

    private void OnDisable() => UnSubscribe();

    private void Start()
    {
        song.loop = false;
        song.Play();
        song.Pause();     // UnPause가 동작하려면 반드시 한 번 재생 후 일시정지
    }

    private void CordUp()  => chordPos = Mathf.Clamp(chordPos - chordRate * Time.deltaTime, 0f, chordCount - 1);
    private void CordDown() => chordPos = Mathf.Clamp(chordPos + chordRate * Time.deltaTime, 0f, chordCount - 1);
    private void PickLeft()     => pickPos = Mathf.Clamp(pickPos + strumRate * Time.deltaTime, -pickRange, pickRange);
    private void PickRight()   => pickPos = Mathf.Clamp(pickPos - strumRate * Time.deltaTime, -pickRange, pickRange);

    private void Update()
    {
        // 그리기
    pick.anchoredPosition = new Vector2(pickPos, pick.anchoredPosition.y);
    chordMarker.anchoredPosition = new Vector2(chordMarker.anchoredPosition.x, ChordY(chordPos));
    targetMarker.anchoredPosition = new Vector2(targetMarker.anchoredPosition.x, ChordY(TargetChord));

        // 판정
        bool strumming = Mathf.Abs(pickPos - lastPickPos) > strumThreshold * Time.deltaTime;
        lastPickPos = pickPos;

        bool chordOk = Mathf.Abs(chordPos - TargetChord) < chordTolerance;
        chordImage.color = chordOk ? okColor : chordNormal;

        graceTimer = (strumming && chordOk) ? graceTime : graceTimer - Time.deltaTime;
        bool shouldPlay = graceTimer > 0f && !isFixed;

        if (shouldPlay && !song.isPlaying) song.UnPause();
        else if (!shouldPlay && song.isPlaying) song.Pause();

        progressBar.SetFill(song.time / song.clip.length);

        if (isFixed) return;
        if (song.time < song.clip.length - 0.05f) return;

        isFixed = true;
        UnSubscribe();
        onFixed?.Invoke();

        if (FixManager.fix != null) FixManager.fix.FixCompleted();
        Debug.Log("기타 수리 완료!");
    }

    private float ChordY(float index) => Mathf.Lerp(neckMinY, neckMaxY, index / (chordCount - 1));

    private void UnSubscribe()
    {
        if (FixManager.fix == null) return;

        FixManager.fix.p1_AMove -= CordUp;
        FixManager.fix.p1_DMove -= CordDown;
        FixManager.fix.p2_LeftMove -= PickLeft;
        FixManager.fix.p2_RightMove -= PickRight;
    }
}