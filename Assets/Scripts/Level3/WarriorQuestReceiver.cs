using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class WarriorQuestReceiver : MonoBehaviour {
    public string playerTag = "Player";
    private QuestManager quest;

    void Start() {
        quest = FindFirstObjectByType<QuestManager>();
        if (quest == null) Debug.LogError("[WarriorQuestReceiver] Không tìm thấy QuestManager.");
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag(playerTag) && quest != null)
        {
            quest.TryCompleteAtWarrior();
        }
    }
}
