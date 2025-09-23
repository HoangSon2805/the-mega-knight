using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player rơi vào Kill Zone → Game Over");
            gameManager.GameOver();
        }
    }
}
