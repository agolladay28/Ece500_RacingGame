
using UnityEngine;
using UnityEngine.XR;

public class XRInput : MonoBehaviour
{
    InputDevice rightHand;
    InputDevice leftHand;
    public bool forward, reverse;

    void Start()
    {
        rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
    }

    void Update()
    {
        // Re-acquire device if it becomes invalid (important)
        if (!rightHand.isValid)
        {
            rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        }
        if (!leftHand.isValid)
        {
            leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        }

        bool forL = false, forR = false, revL = false, revR = false;
        forward = false;
        reverse = false;

        leftHand.TryGetFeatureValue(CommonUsages.secondaryButton, out forL);   // A
        leftHand.TryGetFeatureValue(CommonUsages.primaryButton, out revL); // B
        rightHand.TryGetFeatureValue(CommonUsages.secondaryButton, out forR);   // A
        rightHand.TryGetFeatureValue(CommonUsages.primaryButton, out revR); // B
        forward = forL || forR;
        reverse = revL || revR;
    }
}
