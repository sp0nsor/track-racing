using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAI : MonoBehaviour
{
    [SerializeField] private float maxAngel;
    [SerializeField] private float acceleration;

    [SerializeField] private WheelCollider wheelColliderFL;
    [SerializeField] private WheelCollider wheelColliderFR;
    [SerializeField] private WheelCollider wheelColliderBL;
    [SerializeField] private WheelCollider wheelColliderBR;

    [SerializeField] private Transform transformFL;
    [SerializeField] private Transform transformFR;
    [SerializeField] private Transform transformBL;
    [SerializeField] private Transform transformBR;

    [SerializeField] private GameObject centerOfMass;
    private Rigidbody rb;
    Vector3 vectorMass;
    [SerializeField] private float maxSpeed;
    private void Awake()
    {
        centerOfMass.transform.position = vectorMass;
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = vectorMass;
    }

    [Range(-1, 1)]
    public float forse;
    [Range(-1, 1)]
    public float turn;
    private void FixedUpdate()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            forse = 0;
        }
        wheelColliderFL.motorTorque = forse * acceleration;
        wheelColliderFR.motorTorque = forse * acceleration;

        wheelColliderFL.steerAngle = Mathf.Lerp(wheelColliderFL.steerAngle, turn * maxAngel, 0.5f);
        wheelColliderFR.steerAngle = Mathf.Lerp(wheelColliderFR.steerAngle, turn * maxAngel, 0.5f);

        RotateWheel(wheelColliderFL, transformFL);
        RotateWheel(wheelColliderFR, transformFR);
        RotateWheel(wheelColliderBL, transformBL);
        RotateWheel(wheelColliderBR, transformBR);

    }
    private void RotateWheel(WheelCollider wheelCollider, Transform transform)
    {
        Vector3 position;
        Quaternion rotation;

        wheelCollider.GetWorldPose(out position, out rotation);

        transform.position = position;
        transform.rotation = rotation;
    }
}
