using UnityEngine;

public class SimpleArcadeCar : MonoBehaviour
{
    public WheelCollider frontLeft;
    public WheelCollider frontRight;
    public WheelCollider rearLeft;
    public WheelCollider rearRight;

    public float motorForce = 1500f;
    public float maxSteerAngle = 30f;
    public float brakeForce = 3000f;

    float motorInput;
    float steerInput;
    bool braking;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.6f, 0);
    }

    void FixedUpdate()
    {
        motorInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");
        braking = Input.GetKey(KeyCode.Space);

        frontLeft.steerAngle = steerInput * maxSteerAngle;
        frontRight.steerAngle = steerInput * maxSteerAngle;

        rearLeft.motorTorque = motorInput * motorForce;
        rearRight.motorTorque = motorInput * motorForce;

        float brake = braking ? brakeForce : 0f;
        frontLeft.brakeTorque = brake;
        frontRight.brakeTorque = brake;
        rearLeft.brakeTorque = brake;
        rearRight.brakeTorque = brake;
    }
}

