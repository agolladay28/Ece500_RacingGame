using UnityEngine;

public class SteeringWheelRender : MonoBehaviour
{
    public SteeringWheel sw;

    // Update is called once per frame
    void Update()
    {
        Vector3 tmp = transform.localEulerAngles;
        tmp.y = sw.angle;
        transform.localEulerAngles = tmp;
    }
}
