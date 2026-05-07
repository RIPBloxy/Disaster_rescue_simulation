using UnityEngine;

public class PlayerCarry : MonoBehaviour
{
    public Transform carryPosition;
    public float pickupRange = 3f;
    public KeyCode pickupKey = KeyCode.E;

    private NPCCarry carriedNPC;
    private DropZone currentZone;

    void Update()
    {
        if (Input.GetKeyDown(pickupKey))
        {
            if (carriedNPC != null && currentZone != null)
            {
                DropAtZone();
            }
            else if (carriedNPC != null)
            {
                DropNPC();
            }
            else
            {
                TryPickup();
            }
        }

        if (carriedNPC != null)
        {
            carriedNPC.transform.position = carryPosition.position;
            carriedNPC.transform.rotation = carryPosition.rotation;
        }
    }

    void TryPickup()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, pickupRange);
        foreach (Collider hit in hits)
        {
            NPCCarry npc = hit.GetComponent<NPCCarry>();
            if (npc != null)
            {
                carriedNPC = npc;
                npc.GetPickedUp();
                return;
            }
        }
    }

    void DropNPC()
    {
        carriedNPC.GetDropped();
        carriedNPC = null;
    }

    void DropAtZone()
    {
        carriedNPC.transform.position = currentZone.dropPoint.position;
        carriedNPC.GetDropped();
        currentZone.NPCDropped(); // ← adds score
        carriedNPC = null;
        Debug.Log("NPC delivered!");
    }

    public void SetZone(DropZone zone) { currentZone = zone; }
    public void ClearZone() { currentZone = null; }
}