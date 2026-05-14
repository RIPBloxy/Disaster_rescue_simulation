using UnityEngine;

public class NPCCarry : MonoBehaviour
{
    private Collider col;
    private Rigidbody rb;
    private Animator anim;
    private NPCSOUND npcSound;
    private bool hasBeenPickedUp = false;

    void Start()
    {
        col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        npcSound = GetComponent<NPCSOUND>();
    }

    public void GetPickedUp()
    {
        if (!hasBeenPickedUp)
        {
            hasBeenPickedUp = true;
            if (npcSound != null) npcSound.StopPermanently();
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }

        if (col != null) col.enabled = false;

        if (anim != null)
        {
            anim.SetBool("IsLaying", false);
            anim.ResetTrigger("GoSleep");
            anim.SetTrigger("GoIDLE");
        }

        // Keep renderers ON so legs/butt are visible over shoulder
        // But put NPC on a layer the camera near-clip won't block
        SetLayerRecursively(gameObject, LayerMask.NameToLayer("Default"));
    }

    public void GetDropped()
    {
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.isKinematic = false;
        }

        if (col != null) col.enabled = true;

        if (anim != null)
        {
            anim.ResetTrigger("GoIDLE");
            anim.SetTrigger("GoSleep");
            anim.SetBool("IsLaying", true);
        }

        SetLayerRecursively(gameObject, LayerMask.NameToLayer("Default"));
    }

    void SetLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;
        foreach (Transform child in obj.transform)
            SetLayerRecursively(child.gameObject, layer);
    }
}