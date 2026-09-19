using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;

// selects a button just by aiming at it long enough, no trigger needed
public class DwellSelector : MonoBehaviour
{
    public float dwellTime = 1.2f;
    public float graceTime = 0.2f;

    ButtonKey buttonKey;
    XRDeviceSimulator simulator;
    bool hovering;
    float progress;
    float timeSinceLostHover;
    InteractorHandedness hoveringHand;

    void Awake()
    {
        buttonKey = GetComponent<ButtonKey>();
        simulator = FindFirstObjectByType<XRDeviceSimulator>();
    }

    // hooked up to the same Hover Entered and Hover Exited events as ButtonKey
    // takes the event args (not the plain no-argument version) so we know which hand it is
    public void HoverStart(HoverEnterEventArgs args)
    {
        hovering = true;
        timeSinceLostHover = 0;
        hoveringHand = args.interactorObject.handedness;
    }

    public void HoverEnd()
    {
        hovering = false;
    }

    // in the simulator, only count time while the hovering hand is actually being held (T or Y)
    // outside the simulator (real headset) there's no such thing, so it's just always true
    bool ControllerActive
    {
        get
        {
            if (simulator == null)
                return true;

            if (hoveringHand == InteractorHandedness.Left)
                return simulator.manipulatingLeftController;
            if (hoveringHand == InteractorHandedness.Right)
                return simulator.manipulatingRightController;

            return true;
        }
    }

    void Update()
    {
        if (hovering && ControllerActive)
        {
            progress += Time.deltaTime;

            if (progress >= dwellTime)
            {
                buttonKey.Press();
                progress = 0;
                hovering = false;
            }
        }
        else if (progress > 0)
        {
            // small tremor shouldn't reset progress right away, only a longer gap does
            timeSinceLostHover += Time.deltaTime;
            if (timeSinceLostHover >= graceTime)
                progress = 0;
        }

        buttonKey.SetDwellProgress(progress / dwellTime);
    }
}
