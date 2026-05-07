using UnityEngine;

public class NavigationOrb : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.Instance.AddScore(2);
            Destroy(gameObject);
        }
    }
}