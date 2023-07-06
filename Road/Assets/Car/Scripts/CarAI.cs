using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAI : MonoBehaviour
{
    [SerializeField] private float maxAngel;
    [SerializeField] private float acceleration;

    [SerializeField] private WheelCollider _wheelColliderFL;
    [SerializeField] private WheelCollider _wheelColliderFR;
    [SerializeField] private WheelCollider _wheelColliderBL;
    [SerializeField] private WheelCollider _wheelColliderBR;

    [SerializeField] private Transform _transformFL;
    [SerializeField] private Transform _transformFR;
    [SerializeField] private Transform _transformBL;
    [SerializeField] private Transform _transformBR;

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
        _wheelColliderFL.motorTorque = forse * acceleration;
        _wheelColliderFR.motorTorque = forse * acceleration;

        _wheelColliderFL.steerAngle = Mathf.Lerp(_wheelColliderFL.steerAngle, turn * maxAngel, 0.5f);
        _wheelColliderFR.steerAngle = Mathf.Lerp(_wheelColliderFR.steerAngle, turn * maxAngel, 0.5f);

        RotateWheel(_wheelColliderFL, _transformFL);
        RotateWheel(_wheelColliderFR, _transformFR);
        RotateWheel(_wheelColliderBL, _transformBL);
        RotateWheel(_wheelColliderBR, _transformBR);

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
