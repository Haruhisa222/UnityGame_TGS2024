using UnityEngine;

public class DisplaySpriteOnObject : MonoBehaviour
{
    public GameObject targetObject; // 画像を表示する対象のGameObject
    public Sprite spriteToDisplay;  // 表示する画像のSprite

    void Start()
    {
        if (targetObject != null && spriteToDisplay != null)
        {
            // Sprite Rendererを取得または追加
            SpriteRenderer spriteRenderer = targetObject.GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                spriteRenderer = targetObject.AddComponent<SpriteRenderer>();
            }

            // Spriteを設定
            spriteRenderer.sprite = spriteToDisplay;
        }
    }
}

