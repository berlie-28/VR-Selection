using System.Collections;
using UnityEngine;

// one keypad button, shows its digit and flashes when pressed
public class ButtonKey : MonoBehaviour
{
    public int digit;
    public Color pressColor = Color.green;

    Renderer buttonRenderer;
    Color normalColor;
    KeypadGrid keypad;

    void Awake()
    {
        buttonRenderer = GetComponent<Renderer>();
        normalColor = buttonRenderer.material.color;
        keypad = FindFirstObjectByType<KeypadGrid>();
    }

    // called by whichever selection technique is active (ray, dwell and so on)
    public void Press()
    {
        keypad.EnterDigit(digit);

        StopAllCoroutines();
        StartCoroutine(FlashColor());
    }

    IEnumerator FlashColor()
    {
        buttonRenderer.material.color = pressColor;
        yield return new WaitForSeconds(0.15f);
        buttonRenderer.material.color = normalColor;
    }
}
