
using UnityEngine;

public class DetachOnGrab : MonoBehaviour
{
    Transform originalParent;
    private Quaternion lastParentRotation;

    void Start()
    {
        if (transform.parent != null)
            lastParentRotation = transform.parent.rotation;
    }
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
