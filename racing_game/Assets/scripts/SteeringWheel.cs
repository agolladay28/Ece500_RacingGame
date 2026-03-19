using UnityEngine;
using Unity.Mathematics;
using Oculus.Interaction;
public class SteeringWheel : MonoBehaviour
{
    public float angle;
    private bool grabbed = false;
    private Vector3 starting_angle;
    public Grabbable grabbable;
    public PrometeoCarController controller;

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
        if (grabbed)
        {
            controller.steeringAxis = steering_wheel_to_car();
        }
        else
        {
            Vector3 tmp = Vector3.zero;
            tmp.x = starting_angle.x;
            tmp.z = starting_angle.z;
            tmp.y = car_to_steering_wheel();
            transform.localEulerAngles = tmp;
        }
        //  Debug.Log("Relative Y Rotation: " + angle);
    }
    float steering_wheel_to_car()
    {
        float angle = transform.localEulerAngles.y;
        angle = full_to_half_angle(angle);
        Debug.Log("full-clamp " + angle);
        angle /= 180f;
        Debug.Log("pre-clamp " + angle);
        return Mathf.Clamp(angle, -1f, 1f);
    }
    float car_to_steering_wheel()
    {
        float angle = controller.maxSteeringAngle * controller.steeringAxis;
        angle = half_to_full_angle(angle);
        angle = Mathf.Clamp(angle, 0, 360);
        return angle;
    }
    //converts angle of range 0-360 to -180-180
    float full_to_half_angle(float angle)
    {
        if (angle < 180f)
        {
            return angle;
        }
        else
        {
            return -1 * (angle - 180);
        }
    }
    //converts angle of range -180-180 to range of 0-360
    float half_to_full_angle(float angle)
    {
        if (angle < 180)
        {
            return angle;
        }
        else
        {
            return (-1 * angle) + 180;
        }
    }
}
