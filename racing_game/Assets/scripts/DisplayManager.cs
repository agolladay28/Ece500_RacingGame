
using UnityEngine;

public class DisplayManager : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Displays connected: " + Display.displays.Length);

        // Display.displays[0] is always the primary monitor (where your VR rig/Main Game usually is)
        // Display.displays[1] is the second monitor (where your UI Toolkit HUD should be)

        if (Display.displays.Length > 1)
        {
            Debug.Log("Starting display 2");
            Display.displays[1].Activate();
        }
    }
}