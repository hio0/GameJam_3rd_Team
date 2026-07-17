using TMPro;
using UnityEngine;
public class DialBuilder : MonoBehaviour
{
    [SerializeField] private GameObject numberPrefab;  
    [SerializeField] private GameObject holePrefab;     
    [SerializeField] private Transform plate;           
    [SerializeField] private Transform wheel;           

    [SerializeField] private float numberRadius = 220;
    [SerializeField] private float holeRadius = 150;
    [SerializeField] private float startAngle = 120f;
    [SerializeField] private float stepAngle = 30f;

    private const int Count = 10;

    private void Awake()
    {
        for (int i = 0; i < Count; i++)
        {
            float angle = startAngle - i * stepAngle;
            Quaternion rot = Quaternion.Euler(0f, 0f, -angle);

            GameObject number = Instantiate(numberPrefab, plate);
            number.transform.localPosition = rot * Vector3.up * numberRadius;
            number.transform.localRotation = Quaternion.identity;
            number.GetComponentInChildren<TMP_Text>().text = ((i + 1) % 10).ToString();

            GameObject hole = Instantiate(holePrefab, wheel);
            hole.transform.localPosition = rot * Vector3.up * holeRadius;
        }
    }
}