using UnityEngine;
using UnityEngine.InputSystem;

// press M to bring the keypad panel in front of you, press again to hide it
// lives on an always-active object, since a disabled object's own Update never runs
public class PanelSummon : MonoBehaviour
{
    public GameObject panel;
    public float distance = 0.6f;
    public float heightOffset = -0.15f;

    void Update()
    {
        if (Keyboard.current.mKey.wasPressedThisFrame)
        {
            panel.SetActive(!panel.activeSelf);

            if (panel.activeSelf)
                PlaceInFrontOfPlayer();
        }
    }

    void PlaceInFrontOfPlayer()
    {
        Transform cam = Camera.main.transform;

        // ignore how much the player is looking up or down, keep the panel level
        Vector3 forward = cam.forward;
        forward.y = 0;
        forward.Normalize();

        panel.transform.position = cam.position + forward * distance + Vector3.up * heightOffset;
        panel.transform.rotation = Quaternion.LookRotation(forward);
    }
}
