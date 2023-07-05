using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public Path path;
    public CarAI car { get; private set; }
    public float distanceThresHold;
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

        car.forse = forward;
        car.turn = steer;
    }
}
