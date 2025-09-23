using UnityEngine;

public class QuestManager : MonoBehaviour {
    [Header("Refs")]
    [Tooltip("Item kiếm trong scene (bị ẩn khi nhặt)")]
    public GameObject swordItem;
    [Tooltip("NPC nữ warrior (có collider trigger)")]
    public GameObject warriorNPC;
    [Tooltip("Final coin – để Disable sẵn, khi xong quest sẽ Enable lên")]
    public GameObject finalCoin;

    [Header("State (read-only in Inspector)")]
    [SerializeField] private bool hasSword = false;
    [SerializeField] private bool questCompleted = false;

    void Awake() {
        if (finalCoin != null) finalCoin.SetActive(false); // final coin ẩn sẵn
    }

    public bool HasSword => hasSword;
    public bool QuestCompleted => questCompleted;

    public void PickupSword() {
        if (hasSword) return;
        hasSword = true;
        if (swordItem != null) swordItem.SetActive(false);
        Debug.Log("[Quest] Đã nhặt kiếm.");
    }

    public void TryCompleteAtWarrior() {
        if (!hasSword || questCompleted) return;

        questCompleted = true;
        hasSword = false;

        // Bật final coin
        if (finalCoin != null) finalCoin.SetActive(true);

        Debug.Log("[Quest] Trả kiếm thành công! Final coin đã bật.");
        // (Tùy chọn) thưởng điểm/hiệu ứng tại đây
    }
}
