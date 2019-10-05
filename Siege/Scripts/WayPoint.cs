using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum WaypointType{ Simple, Control  };

public class WayPoint : MonoBehaviour
{
    public List<WayPoint> waypoints;
    public float radius;
    public WaypointType type;
    public ControlPoint controlPoint;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public Vector3 GetPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        return transform.position + randomDirection;
    }

    public WayPoint nextPoint()
    {
        return waypoints[Random.Range(0, waypoints.Count)];
    }
}
