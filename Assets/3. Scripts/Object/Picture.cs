using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Picture : MonoBehaviour
{
    [Header("Image")]
    [SerializeField] private Image image;

    [Header("Brushes")]
    [SerializeField] private RectTransform brush1;
    [SerializeField] private RectTransform brush2;

    [SerializeField] private float moveSpeed = 300f;
    [SerializeField] private int brushRadius = 20;

    private Texture2D texture;
    private RectTransform imageRect;

    private int erasedPixelCount;
    private int totalPixelCount;

    void Start()
    {
        FadeObject.fade.FadeIn(1.5f);
        imageRect = image.rectTransform;

        texture = Instantiate(image.sprite.texture);

        image.sprite = Sprite.Create(
            texture,
            image.sprite.rect,
            new Vector2(0.5f, 0.5f),
            image.sprite.pixelsPerUnit);

        Color[] pixels = texture.GetPixels();

        totalPixelCount = 0;

        foreach (Color c in pixels)
        {
            if (c.a > 0.01f)
                totalPixelCount++;
        }
    }

    void Update()
    {
        MoveBrushes();

        Erase(brush1);
        Erase(brush2);

        texture.Apply();
    }

    void MoveBrushes()
    {
        Vector2 move1 = Vector2.zero;

        if (Input.GetKey(KeyCode.W)) move1.y += 1;
        if (Input.GetKey(KeyCode.S)) move1.y -= 1;
        if (Input.GetKey(KeyCode.A)) move1.x -= 1;
        if (Input.GetKey(KeyCode.D)) move1.x += 1;

        brush1.anchoredPosition += move1.normalized * moveSpeed * Time.deltaTime;
        ClampBrush(brush1);

        Vector2 move2 = Vector2.zero;

        if (Input.GetKey(KeyCode.UpArrow)) move2.y += 1;
        if (Input.GetKey(KeyCode.DownArrow)) move2.y -= 1;
        if (Input.GetKey(KeyCode.LeftArrow)) move2.x -= 1;
        if (Input.GetKey(KeyCode.RightArrow)) move2.x += 1;

        brush2.anchoredPosition += move2.normalized * moveSpeed * Time.deltaTime;
        ClampBrush(brush2);
    }

    void ClampBrush(RectTransform brush)
    {
        float halfWidth = brush.rect.width * 0.5f;
        float halfHeight = brush.rect.height * 0.5f;

        Vector2 pos = brush.anchoredPosition;

        pos.x = Mathf.Clamp(pos.x, -960 + halfWidth, 960 - halfWidth);
        pos.y = Mathf.Clamp(pos.y, -540 + halfHeight, 540 - halfHeight);

        brush.anchoredPosition = pos;
    }

    void Erase(RectTransform brush)
    {
        Vector2 localPos = brush.anchoredPosition;

        Rect rect = imageRect.rect;

        float u = Mathf.InverseLerp(rect.xMin, rect.xMax, localPos.x);
        float v = Mathf.InverseLerp(rect.yMin, rect.yMax, localPos.y);

        int px = Mathf.RoundToInt(u * texture.width);
        int py = Mathf.RoundToInt(v * texture.height);

        for (int x = -brushRadius; x <= brushRadius; x++)
        {
            for (int y = -brushRadius; y <= brushRadius; y++)
            {
                if (x * x + y * y > brushRadius * brushRadius)
                    continue;

                int tx = px + x;
                int ty = py + y;

                if (tx < 0 || tx >= texture.width ||
                    ty < 0 || ty >= texture.height)
                    continue;

                Color c = texture.GetPixel(tx, ty);
                if(c.a != 0)
                {
                    c.a = 0;
                    erasedPixelCount++;
                }
                texture.SetPixel(tx, ty, c);
            }
        }

        float percent = (float)erasedPixelCount / totalPixelCount;

        if (percent >= 0.93f)
        {
            FixManager.fix.FixCompleted();
        }
    }
}
