using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public Path path;
    public CarAI car { get; private set; }
    public float sensorFrontOffset;
    public float sensorSideOffset;
    public float sensorEndShift;
    public float sensorLength;
    public float distanceThresHold;
    public float heightOffset;
    public int nextWayPoint;

    private void Awake()
    {
        car = GetComponent<CarAI>();
        FindClosestWayPoint();
    }

    private void FindClosestWayPoint()
    {
        float closestDistance = float.MaxValue;
        int closestWayPoint = -1;

        Vector3 myPosition = transform.position;

        for (int i = 0; i < path.wayPoints.Length; i++)
        {
            float distance = Vector3.Distance(path.wayPoints[i].transform.position, myPosition);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestWayPoint = i;
            }
        }

        nextWayPoint = closestWayPoint;
    }
    Vector3 GetSensorStart(float dir, float heightOffset)
    {
        Vector3 position = transform.position;
        position.y += heightOffset;
        return position + transform.forward * sensorFrontOffset + transform.right * sensorSideOffset * dir;
    }

    Vector3 GetSensorDir(float shift, float heightOffset)
    {
        Vector3 direction = transform.forward * sensorLength + transform.right * shift * sensorEndShift;
        direction.y += heightOffset;
        return direction;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawRay(GetSensorStart(0.0f, heightOffset), GetSensorDir(0.0f, heightOffset));
        Gizmos.DrawRay(GetSensorStart(-1.0f, heightOffset), GetSensorDir(-1.0f, heightOffset));
        Gizmos.DrawRay(GetSensorStart(-1.0f, heightOffset), GetSensorDir(0.0f, heightOffset));
        Gizmos.DrawRay(GetSensorStart(1.0f, heightOffset), GetSensorDir(1.0f, heightOffset));
        Gizmos.DrawRay(GetSensorStart(1.0f, heightOffset), GetSensorDir(0.0f, heightOffset));
    }

    private void Update()
    {
        Vector3 targetWayPoint = path.wayPoints[nextWayPoint].transform.position;
        Vector3 target = new Vector3(targetWayPoint.x, transform.position.y, targetWayPoint.z);

        Vector3 vectorToTarget = transform.InverseTransformPoint(target);
        float distanceToTarget = vectorToTarget.magnitude;

        if (distanceToTarget < distanceThresHold)
        {
            nextWayPoint = (nextWayPoint + 1) % path.wayPoints.Length;
        }

        float forward = 1f;
        float steer = vectorToTarget.x / distanceToTarget;

        if (Physics.Raycast(GetSensorStart(0.0f, heightOffset), GetSensorDir(0.0f, heightOffset), sensorLength))
            forward = -0.2f;
        if (Physics.Raycast(GetSensorStart(-1.0f, heightOffset), GetSensorDir(-1.0f, heightOffset), sensorLength))
            steer = 1.0f;
        if (Physics.Raycast(GetSensorStart(-1.0f, heightOffset), GetSensorDir(0.0f, heightOffset), sensorLength))
            steer = 1.0f;
        if (Physics.Raycast(GetSensorStart(1.0f, heightOffset), GetSensorDir(1.0f, heightOffset), sensorLength))
            steer = -1.0f;
        if (Physics.Raycast(GetSensorStart(1.0f, heightOffset), GetSensorDir(0.0f, heightOffset), sensorLength))
            steer = -1.0f;

        car.forse = forward;
        car.turn = steer;
    }
}
