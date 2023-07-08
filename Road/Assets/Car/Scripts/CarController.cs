using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private Transform transformFL;
    [SerializeField] private Transform transformFR;
    [SerializeField] private Transform transformBL;
    [SerializeField] private Transform transformBR;

    [SerializeField] private WheelCollider wheelColliderFL;
    [SerializeField] private WheelCollider wheelColliderFR;
    [SerializeField] private WheelCollider wheelColliderBL;
    [SerializeField] private WheelCollider wheelColliderBR;

    [SerializeField] public float force;
    [SerializeField] private float maxSpeed;
    [SerializeField] public float maxAngel;
    [SerializeField] private float brakeForce;

    [SerializeField] private GameObject centerMass;

    private float move;
    private Rigidbody rb;
    private Vector3 vectorMass;

    private void Start()
    {
        centerMass.transform.position = vectorMass;
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = vectorMass;
    }

    private void FixedUpdate()
    {
        move = Input.GetAxis("Vertical") * force;

        if (rb.velocity.magnitude > maxSpeed)
        {
            move = 0;
        }
        wheelColliderFL.motorTorque = move;
        wheelColliderFR.motorTorque = move;
        if (Input.GetKey(KeyCode.Space))
        {
            wheelColliderFL.brakeTorque = move;
            wheelColliderFR.brakeTorque = move;
            wheelColliderFL.brakeTorque = move;
            wheelColliderFL.brakeTorque = move;
        }
        else
        {
            wheelColliderFL.brakeTorque = 0f;
            wheelColliderFR.brakeTorque = 0f;
            wheelColliderBL.brakeTorque = 0f;
            wheelColliderBR.brakeTorque = 0f;
        }
        /*wheelColliderFL.steerAngle = Mathf.Lerp(wheelColliderFL.steerAngle, maxAngel * Input.GetAxis("Horizontal"), 0.5f);
        wheelColliderFR.steerAngle = Mathf.Lerp (wheelColliderFR.steerAngle,maxAngel * Input.GetAxis("Horizontal"), 0.5f);*/

        wheelColliderFL.steerAngle = maxAngel * Input.GetAxis("Horizontal");
        wheelColliderFR.steerAngle = maxAngel * Input.GetAxis("Horizontal");

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
