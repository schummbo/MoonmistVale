using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenTint : MonoBehaviour
{
    [SerializeField] private Color untintedColor;
    [SerializeField] private Color tintedColor;
    public float speed = 0.5f;

    private Image image;

    private float f;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void Tint()
    {
        f = 0;
        StopAllCoroutines();
        StartCoroutine(TintScreen());
    }

    public void Untint()
    {
        f = 0;
        StopAllCoroutines(); 
        StartCoroutine(UntintScreen());
    }

    private IEnumerator TintScreen()
    {
        while (f < 1)
        {
            f += Time.deltaTime * speed;
            f = Mathf.Clamp01(f);

            Color c = image.color;

            c = Color.Lerp(untintedColor, tintedColor, f);

            image.color = c;

            yield return new WaitForEndOfFrame();
        }
    }    
    
    private IEnumerator UntintScreen()
    {
        while (f < 1)
        {
            f += Time.deltaTime * speed;
            f = Mathf.Clamp01(f);

            Color c = image.color;

            c = Color.Lerp(tintedColor, untintedColor, f);

            image.color = c;

            yield return new WaitForEndOfFrame();
        }

    }
}
