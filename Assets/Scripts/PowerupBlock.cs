using UnityEngine;

public class PowerupBlock : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player")) return;

        var status = other.GetComponent<PlayerStatus>();
        if (status != null)
        {
            status.PowerUp();
            gameObject.SetActive(false); // ẩn ô ? sau khi ăn
        }
    }
}