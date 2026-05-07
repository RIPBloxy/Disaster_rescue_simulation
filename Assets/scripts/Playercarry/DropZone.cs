using UnityEngine;

public class DropZone : MonoBehaviour
{
    public Transform dropPoint;
    public int scoreValue = 10; // points awarded per NPC drop

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered: " + other.name);
        PlayerCarry player = other.GetComponent<PlayerCarry>();
        if (player != null) player.SetZone(this);
    }

    void OnTriggerExit(Collider other)
    {
        PlayerCarry player = other.GetComponent<PlayerCarry>();
        if (player != null) player.ClearZone();
    }

    public void NPCDropped()
    {
        ScoreManager.Instance.AddScore(scoreValue);
    }
    
}