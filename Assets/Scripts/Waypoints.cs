using UnityEngine;

public class Waypoints : MonoBehaviour
{
    // Finding the agents on the waypoints
    public Vector3 Position => transform.position;
    public bool isThreat = false;

    // Red wire sphere gizmo
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.1f);
    }

}
