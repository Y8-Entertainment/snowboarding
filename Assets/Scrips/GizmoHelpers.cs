#if UNITY_EDITOR
using UnityEngine;

public class GizmoHelpers : MonoBehaviour
{
    public float radius = 0.5f;
    void OnDrawGizmos() => Gizmos.DrawWireSphere(transform.position, radius);
}
#endif

