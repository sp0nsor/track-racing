using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmissionController : MonoBehaviour
{
    public int numberOfGears;
    public float[] gearRatios;
    private int currentGear = 0;

    public float maxSpeed;
    public float minSpeed;

    private Rigidbody rb;
    public WheelCollider[] wheelColliders;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        wheelColliders = GetComponentsInChildren<WheelCollider>();
    }
    private void FixedUpdate()
    {
        float currentSpeed = rb.velocity.magnitude;

        if (currentSpeed > GetMaxSpeedForGear(currentGear) && currentGear < numberOfGears - 1)
        {
            currentGear++;
            ApplyGearRatio();
        }
        else if (currentSpeed < GetMinSpeedForGear(currentGear) && currentGear > 0)
        {
            currentGear--;
            ApplyGearRatio();
        }

    }
    private float GetMaxSpeedForGear(int gear)
    {
        return maxSpeed * gearRatios[gear];
    }
    private float GetMinSpeedForGear(int gear)
    {
        return minSpeed * gearRatios[gear];
    }
    private void ApplyGearRatio()
    {
        foreach(var wheel in wheelColliders)
        {
            wheel.motorTorque = Input.GetAxis("Vertical") * gearRatios[currentGear];
        }
    }
}
