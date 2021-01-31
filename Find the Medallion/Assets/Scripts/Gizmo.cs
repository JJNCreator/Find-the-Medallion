using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gizmo : MonoBehaviour
{
    public enum GizmoShape
    {
        WireSphere,
        Sphere,
        WireBox,
        Box
    }
    public float gizmoRadius = 2f;
    public GizmoShape gizmoShape = GizmoShape.WireSphere;
    public Color gizmoColor;
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(gizmoColor.r, gizmoColor.g, gizmoColor.b, 1f);
        switch (gizmoShape)
        {
            case GizmoShape.WireSphere:
                Gizmos.DrawWireSphere(transform.position, gizmoRadius);
                break;
            case GizmoShape.WireBox:
                Gizmos.DrawWireCube(transform.position, new Vector3(gizmoRadius, gizmoRadius, gizmoRadius));
                break;
            case GizmoShape.Sphere:
                Gizmos.DrawSphere(transform.position, gizmoRadius);
                break;
            case GizmoShape.Box:
                Gizmos.DrawCube(transform.position, new Vector3(gizmoRadius, gizmoRadius, gizmoRadius));
                break;
        }
    }
}
