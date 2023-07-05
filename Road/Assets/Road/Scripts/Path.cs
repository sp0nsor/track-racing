using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Path : MonoBehaviour
{
    public WayPoint[] wayPoints;

    private void OnDrawGizmos()
    {
        if (wayPoints == null || wayPoints.Length < 2)
            return;

        Gizmos.color = Color.white;
        for (int i = 0; i < wayPoints.Length; i++)
        {
            Gizmos.DrawLine(wayPoints[i].transform.position, wayPoints[(i + 1) % wayPoints.Length].transform.position);
        }
    }
}
