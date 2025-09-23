using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SwordPickup : MonoBehaviour {
    [Tooltip("Tag của Player, mặc định 'Player'")]
    public string playerTag = "Player";

    private QuestManager quest;

    void Start() {
        quest = FindFirstObjectByType<QuestManager>();
        if (quest == null) Debug.LogError("[SwordPickup] Không tìm thấy QuestManager trong scene.");
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag(playerTag) && quest != null)
        {
            quest.PickupSword();
            var audio = FindAnyObjectByType<AudioManager>();
            if (audio != null) audio.PlaySwordCollectSound();
            // tắt chính item này
            gameObject.SetActive(false);
        }
    }
}
