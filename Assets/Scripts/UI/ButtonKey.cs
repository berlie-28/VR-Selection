using System.Collections;
using UnityEngine;

// one keypad button, shows its digit and reacts when the ray hovers or presses it
public class ButtonKey : MonoBehaviour
{
    public int digit;
    public Color restColor = new Color(0.85f, 0.85f, 0.85f);
    public Color pressColor = new Color(0.25f, 0.25f, 0.25f);
    public float hoverScale = 1.15f;

    Renderer buttonRenderer;
    Vector3 normalScale;
    KeypadGrid keypad;
    Coroutine flashRoutine;

    void Awake()
    {
        buttonRenderer = GetComponent<Renderer>();
        buttonRenderer.material.color = restColor;
        normalScale = transform.localScale;
        keypad = FindFirstObjectByType<KeypadGrid>();
    }

    // called while the ray is aimed at this button, before pressing
    public void HoverStart()
    {
        transform.localScale = normalScale * hoverScale;
    }

    public void HoverEnd()
    {
        transform.localScale = normalScale;
    }

    // called by whichever selection technique is active (ray, dwell and so on)
    public void Press()
    {
        keypad.EnterDigit(digit);

        if (flashRoutine != null)
            StopCoroutine(flashRoutine);
        flashRoutine = StartCoroutine(FlashColor());
    }

    IEnumerator FlashColor()
    {
        float duration = 0.15f;
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            buttonRenderer.material.color = Color.Lerp(pressColor, restColor, t / duration);
            yield return null;
        }
        buttonRenderer.material.color = restColor;
        flashRoutine = null;
    }
}
