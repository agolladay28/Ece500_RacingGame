using UnityEngine;

public class SteeringWheelRender : MonoBehaviour
{
    private Vector3 start_angle;
    public SteeringWheel sw;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        start_angle = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tmp;
        tmp.x = transform.eulerAngles.x;
        tmp.z = transform.eulerAngles.z;
        tmp.y = sw.angle;
        transform.eulerAngles = tmp;
    }
}
