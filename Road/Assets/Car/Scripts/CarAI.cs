using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAI : MonoBehaviour
{
    [SerializeField] private float maxAngel;
    [SerializeField] private float acceleration;

    [SerializeField] private WheelCollider firstWheel;
    [SerializeField] private WheelCollider secondWheel;
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
        firstWheel.motorTorque = forse * acceleration;
        secondWheel.motorTorque = forse * acceleration;

        firstWheel.steerAngle = Mathf.Lerp(firstWheel.steerAngle, turn * maxAngel, 0.5f);
        secondWheel.steerAngle = Mathf.Lerp(secondWheel.steerAngle, turn * maxAngel, 0.5f);
    }
}
