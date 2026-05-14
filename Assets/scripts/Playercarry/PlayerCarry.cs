using UnityEngine;
using TMPro;

public class PlayerCarry : MonoBehaviour
{
    public Transform carryPosition;
    public float pickupRange = 3f;
    public KeyCode pickupKey = KeyCode.E;

    [Header("Carry Position Offset")]
    public float offsetRight = 0.5f;
    public float offsetUp = 1.0f;
    public float offsetForward = -0.3f;

    [Header("Carry Rotation")]
    public float rotationX = 90f;

    private TMP_Text pickupPrompt;
    private TMP_Text dropPrompt;
    private NPCCarry carriedNPC;
    private DropZone currentZone;

    void OnEnable() => FindPrompts();

    void FindPrompts()
    {
        GameObject p1 = GameObject.FindWithTag("PickupPrompt");
        GameObject p2 = GameObject.FindWithTag("DropPrompt");
        if (p1 != null) pickupPrompt = p1.GetComponent<TMP_Text>();
        if (p2 != null) dropPrompt = p2.GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (pickupPrompt == null || dropPrompt == null) FindPrompts();

        if (Input.GetKeyDown(pickupKey))
        {
            if (carriedNPC != null && currentZone != null)
                DropAtZone();
            else
                TryPickup();
        }

        if (carriedNPC != null)
        {
            Vector3 shoulderOffset = transform.right * offsetRight
                                   + Vector3.up * offsetUp
                                   + transform.forward * offsetForward;

            carriedNPC.transform.position = transform.position + shoulderOffset;
            carriedNPC.transform.rotation = Quaternion.Euler(
                rotationX,
                transform.eulerAngles.y,
                0f
            );
        }

        UpdatePrompts();
    }

    void UpdatePrompts()
    {
        SetPromptVisible(pickupPrompt, carriedNPC == null && IsNPCNearby());
        SetPromptVisible(dropPrompt, carriedNPC != null && currentZone != null);
    }

    void SetPromptVisible(TMP_Text prompt, bool visible)
    {
        if (prompt == null) return;
        prompt.gameObject.SetActive(true);
        Color c = prompt.color;
        c.a = visible ? 1f : 0f;
        prompt.color = c;
    }

    bool IsNPCNearby()
    {
        foreach (Collider hit in Physics.OverlapSphere(transform.position, pickupRange))
            if (hit.GetComponent<NPCCarry>() != null) return true;
        return false;
    }

    void TryPickup()
    {
        foreach (Collider hit in Physics.OverlapSphere(transform.position, pickupRange))
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

    void DropAtZone()
    {
        carriedNPC.GetDropped();
        currentZone.NPCDropped(carriedNPC.gameObject);
        carriedNPC = null;
    }

    public void SetZone(DropZone zone) => currentZone = zone;
    public void ClearZone() => currentZone = null;
}