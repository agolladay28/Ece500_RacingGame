using UnityEngine;
using Oculus.Interaction;
public class SteeringWheel : MonoBehaviour
{
    public float angle;
    private bool grabbed = false;
    private Vector3 starting_angle;
    public Grabbable grabbable;

    void OnEnable()
    {
        grabbable.WhenPointerEventRaised += HandleEvent;
    }

    void OnDisable()
    {
        grabbable.WhenPointerEventRaised -= HandleEvent;
    }
    void Start()
    {
        starting_angle = transform.localEulerAngles;
    }
    private void HandleEvent(PointerEvent evt)
    {
        if (evt.Type == PointerEventType.Select)
        {
            Debug.Log("Grabbed");
            grabbed = true;
        }
        else if (evt.Type == PointerEventType.Unselect)
        {
            Debug.Log("Released");
            grabbed = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        angle = transform.localEulerAngles.y;
        if (!grabbed)
        {
            Vector3 tmp;
            tmp.x = starting_angle.x;
            tmp.z = starting_angle.z;

            transform.localEulerAngles = tmp;
        }
        //  Debug.Log("Relative Y Rotation: " + angle);
    }

}
