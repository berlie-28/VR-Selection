using System.Collections.Generic;
using UnityEngine;

// collects digits pressed on the keypad and checks them against the target PIN
public class KeypadGrid : MonoBehaviour
{
    public string targetPin = "1234";

    List<int> enteredDigits = new List<int>();

    public void EnterDigit(int digit)
    {
        enteredDigits.Add(digit);

        if (enteredDigits.Count >= targetPin.Length)
        {
            CheckPin();
        }
    }

    void CheckPin()
    {
        string entered = "";
        foreach (int d in enteredDigits)
            entered += d;

        if (entered == targetPin)
            Debug.Log("Correct PIN!");
        else
            Debug.Log("Wrong PIN, try again.");

        enteredDigits.Clear();
    }
}
