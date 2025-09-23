using UnityEngine;

public class PlayerCollision : MonoBehaviour {
    private GameManager gameManager;
    private AudioManager audioManager;
    private PlayerStatus status;  // để biết có power-up hay không

    void Awake() {
        gameManager = FindAnyObjectByType<GameManager>();
        audioManager = FindAnyObjectByType<AudioManager>();
        status = GetComponent<PlayerStatus>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            audioManager.PlayCoinSound();
            gameManager.AddScore(1);
            if (CoinManager.I != null)
            {
                CoinManager.I.OnCoinCollected();
            }
        }       
        else if (collision.CompareTag("Trap") || collision.CompareTag("Enemy"))
        {
            if (status != null && status.IsPowered)
            {
                // Khi power-up thì phá trap/enemy
                Destroy(collision.gameObject);
                gameManager.AddScore(5); // ví dụ cộng thêm điểm, tuỳ chỉnh
                audioManager.PlayCoinSound(); // nếu có SFX
            } else
            {
                // Chưa power-up thì game over
                gameManager.GameOver();
            }
        } else if (collision.CompareTag("Key"))
        {
            Destroy(collision.gameObject);
            gameManager.GameWin();
        } else if (collision.CompareTag("MegaCoin"))
        {
            // Không làm gì ở đây. MegaCoin.cs tự xử lý OnTriggerEnter2D.
            return;
        }
    }
}
