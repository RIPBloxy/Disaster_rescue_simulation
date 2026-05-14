using UnityEngine;

public class NPCCarry : MonoBehaviour
{
    private Collider col;
    private Rigidbody rb;
    private Animator anim;

    void Start()
    {
        col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    public void GetPickedUp()
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }
        if (col != null) col.enabled = false;
        if (anim != null) anim.SetTrigger("GoIDLE");
    }

    public void GetDropped()
    {
        if (rb != null) rb.isKinematic = false;
        if (col != null) col.enabled = true;
        if (anim != null) anim.SetTrigger("GoSleep");
    }
}