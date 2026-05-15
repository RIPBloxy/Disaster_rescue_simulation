using UnityEngine;

public class DropZone : MonoBehaviour
{
    public Transform dropPoint;
    public int scoreValue = 10;

    private bool isOccupied = false;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered: " + other.name);
        PlayerCarry player = other.GetComponent<PlayerCarry>();
        if (player != null && !isOccupied) player.SetZone(this);
    }

    void OnTriggerExit(Collider other)
    {
        PlayerCarry player = other.GetComponent<PlayerCarry>();
        if (player != null) player.ClearZone();
    }

    public void NPCDropped(GameObject npc)
    {
        npc.transform.position = dropPoint.position;
        npc.transform.rotation = dropPoint.rotation;

        NPCCarry npcCarry = npc.GetComponent<NPCCarry>();
        if (npcCarry != null) npcCarry.SetPlaced();

        isOccupied = true;
        ScoreManager.Instance.AddScore(scoreValue);
    }

    public bool IsOccupied() => isOccupied;
}