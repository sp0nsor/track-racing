using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _car;

    private Vector3 ofset = new Vector3 (0, 2f, -4f);
    private float speed = 10f;

    private void FixedUpdate()
    {
        var targetPosition = _car.TransformPoint (ofset);
        transform.position = Vector3.Lerp (transform.position, targetPosition, speed * Time.deltaTime);

        var direction = _car.position - transform.position;
        var rotation = Quaternion.LookRotation (direction, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);
    }
}
