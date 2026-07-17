using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picture : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private int brushSize = 20;

    private Texture2D texture;

    void Start()
    {
        Texture2D original = spriteRenderer.sprite.texture;

        texture = Instantiate(original);

        spriteRenderer.sprite = Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f),
            spriteRenderer.sprite.pixelsPerUnit);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Erase();
        }
    }

    void Erase()
    {
        Vector3 world = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        world.z = 0;

        RaycastHit2D hit = Physics2D.Raycast(world, Vector2.zero);

        if (hit.collider == null) return;
        if (hit.collider.gameObject != gameObject) return;

        Vector2 local = transform.InverseTransformPoint(world);

        Sprite sprite = spriteRenderer.sprite;

        float px = local.x * sprite.pixelsPerUnit + sprite.rect.width / 2;
        float py = local.y * sprite.pixelsPerUnit + sprite.rect.height / 2;

        for (int x = -brushSize; x <= brushSize; x++)
        {
            for (int y = -brushSize; y <= brushSize; y++)
            {
                if (x * x + y * y > brushSize * brushSize)
                    continue;

                int tx = Mathf.RoundToInt(px + x);
                int ty = Mathf.RoundToInt(py + y);

                if (tx < 0 || tx >= texture.width ||
                    ty < 0 || ty >= texture.height)
                    continue;

                Color c = texture.GetPixel(tx, ty);
                c.a = 0;
                texture.SetPixel(tx, ty, c);
            }
        }

        texture.Apply();
    }
}
