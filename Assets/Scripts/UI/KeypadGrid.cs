using System.Collections.Generic;
using TMPro;
using UnityEngine;

// collects digits pressed on the keypad and checks them against the target PIN
public class KeypadGrid : MonoBehaviour
{
    public string targetPin = "1234";
    public TextMeshPro display;

    List<int> enteredDigits = new List<int>();

    public void EnterDigit(int digit)
    {
        enteredDigits.Add(digit);
        UpdateDisplay();

        if (enteredDigits.Count >= targetPin.Length)
        {
            CheckPin();
        }
    }

    void UpdateDisplay()
    {
        if (display == null)
            return;

        string entered = "";
        foreach (int d in enteredDigits)
            entered += d;

        display.text = entered;
    }

    void CheckPin()
    {
        string entered = "";
        foreach (int d in enteredDigits)
            entered += d;

        bool correct = entered == targetPin;
        Debug.Log(correct ? "Correct PIN!" : "Wrong PIN, try again.");

        if (display != null)
            display.text = correct ? "Correct!" : "Wrong";

        enteredDigits.Clear();
    }
}
