using UnityEngine;

public class NPCCarry : MonoBehaviour
{
    private Collider col;
    private Rigidbody rb;

    void Start()
    {
        col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }

    public void GetPickedUp()
    {
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        if (col != null) col.enabled = false;
    }

    public void GetDropped()
    {
        if (rb != null) rb.isKinematic = false;
        if (col != null) col.enabled = true;
    }
}