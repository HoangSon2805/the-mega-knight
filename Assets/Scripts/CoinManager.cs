using UnityEngine;

public class CoinManager : MonoBehaviour {
    public static CoinManager I { get; private set; }

    [Header("Unlock target (ô ? xuất hiện khi ăn hết coin)")]
    [SerializeField] private GameObject powerupBlock; // Drag từ Hierarchy vào
    [SerializeField] private bool hidePowerupBlockUntilClear = true;

    [Header("Level 3 - Final coin (bị Disable lúc đầu)")]
    [SerializeField] private GameObject finalQuestCoin; // <-- KÉO final coin vào (chỉ level 3)

    private int total;
    private int remaining;

    void Awake() {
        if (I != null && I != this) { Destroy(gameObject); return; }
        I = this;
    }

    void Start() {
        // Đếm toàn bộ coin thường đang có trong scene (tag = "Coin")
        var coins = GameObject.FindGameObjectsWithTag("Coin");
        total = remaining = coins.Length;

        if (finalQuestCoin != null && !finalQuestCoin.activeInHierarchy)
        {
            total += 1;
            remaining += 1;
        }

        if (hidePowerupBlockUntilClear && powerupBlock)
            powerupBlock.SetActive(false);

        Debug.Log($"[CoinManager] total = {total}, remaining = {remaining} (finalCoin placeholder: {(finalQuestCoin != null && !finalQuestCoin.activeInHierarchy ? 1 : 0)})");
    }

    public void OnCoinCollected() {
        remaining = Mathf.Max(0, remaining - 1);
        // Debug.Log($"[CoinManager] remaining = {remaining}");

        if (remaining == 0) UnlockPowerupBlock();
    }

    private void UnlockPowerupBlock() {
        if (powerupBlock) powerupBlock.SetActive(true);
        Debug.Log("[CoinManager] All coins collected → Power-up block unlocked!");
        // TODO: thêm SFX/animation sau
        var audio = FindAnyObjectByType<AudioManager>();
        if (audio != null) audio.PlayPowerupAppearSound();
    }

    // Optional: cho UI đọc
    public int Total => total;
    public int Remaining => remaining;
}

