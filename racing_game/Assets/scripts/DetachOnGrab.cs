
using UnityEngine;

public class DetachOnGrab : MonoBehaviour
{
    Transform originalParent;

    public void OnGrab()
    {
        originalParent = transform.parent;
        transform.parent = null;
    }

    public void OnRelease()
    {
        transform.parent = originalParent;
    }
}
