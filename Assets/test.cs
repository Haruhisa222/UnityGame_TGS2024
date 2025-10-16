using UnityEngine;
using System.Collections;

public class SimpleTest : MonoBehaviour
{
    public GameObject targetImage;
    public float displayDuration = 2f;

    void Start()
    {
        if (targetImage != null)
        {
            StartCoroutine(DisplayForDuration(displayDuration));
        }
    }

    IEnumerator DisplayForDuration(float duration)
    {
        targetImage.SetActive(true);
        Debug.Log("ターゲットオブジェクトが表示されました。アクティブ状態: " + targetImage.activeSelf);

        yield return new WaitForSeconds(duration);

        targetImage.SetActive(false);
        Debug.Log("ターゲットオブジェクトが非表示になりました。アクティブ状態: " + targetImage.activeSelf);
    }
}
