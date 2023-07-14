using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EngineSound : MonoBehaviour
{
    Rigidbody rb;
    public AudioSource engineSourse;

    public int gearShiftQuantity;
    public float pitchBoost;
    public float pitchRange;

    private float averageSpeed;
    private int parsedValue;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void LateUpdate()
    {
        float speed = rb.velocity.magnitude;

        averageSpeed = speed / gearShiftQuantity;
        parsedValue = (int)averageSpeed;

        float difference = averageSpeed - parsedValue;

        engineSourse.pitch = Mathf.Lerp(engineSourse.pitch, (pitchRange * difference) * pitchBoost, 0.1f);
    }
}
