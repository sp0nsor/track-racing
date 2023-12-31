using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private Transform _transformFL;
    [SerializeField] private Transform _transformFR;
    [SerializeField] private Transform _transformBL;
    [SerializeField] private Transform _transformBR;

    [SerializeField] private WheelCollider _wheelColliderFL;
    [SerializeField] private WheelCollider _wheelColliderFR;
    [SerializeField] private WheelCollider _wheelColliderBL;
    [SerializeField] private WheelCollider _wheelColliderBR;

    [SerializeField] private float _force;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _maxAngel;
    [SerializeField] private float _brakeForce;

    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.centerOfMass = new Vector3 (0f, 0f, 0.5f);
    }

    private void FixedUpdate()
    {
        _wheelColliderFL.motorTorque = Input.GetAxis("Vertical") * _force;
        _wheelColliderFR.motorTorque = Input.GetAxis("Vertical") * _force;

        if (Input.GetKey(KeyCode.Space))
        {
            _wheelColliderFL.brakeTorque = _brakeForce;
            _wheelColliderFR.brakeTorque = _brakeForce;
            _wheelColliderBL.brakeTorque = _brakeForce;
            _wheelColliderBR.brakeTorque = _brakeForce;
        }
        else
        {
            _wheelColliderFL.brakeTorque = 0f;
            _wheelColliderFR.brakeTorque = 0f;
            _wheelColliderBL.brakeTorque = 0f;
            _wheelColliderBR.brakeTorque = 0f;
        }
        _wheelColliderFL.steerAngle = _maxAngel * Input.GetAxis("Horizontal");
        _wheelColliderFR.steerAngle = _maxAngel * Input.GetAxis("Horizontal");

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
