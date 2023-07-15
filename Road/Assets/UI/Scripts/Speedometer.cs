using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minSpeedArrowAngle;
    [SerializeField] private float maxSpeedArrowAngle;

    public Rigidbody rb;

    [Header("UI")]
    [SerializeField] private RectTransform arrow;

    private void LateUpdate()
    {
        if (rb == null) return;

        float speed = rb.velocity.magnitude * 3.6f;

        if (arrow != null)
        {
            arrow.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(minSpeedArrowAngle, maxSpeedArrowAngle, speed / maxSpeed));
        }
    }

    public void SetTargetRb(Rigidbody newRb)
    {
        rb = newRb;
    }
}
