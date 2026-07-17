using UnityEngine;
using UnityEngine.UI;

public class TuningBar : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private float smoothSpeed = 3.5f;

    private float targetFill;

    public void SetFill(float value) => targetFill = value;

    // Visual smoothing only — no game logic.
    private void Update()
    {
        fillImage.fillAmount = Mathf.MoveTowards(fillImage.fillAmount, targetFill, smoothSpeed * Time.deltaTime);
    }
}