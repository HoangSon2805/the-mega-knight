using UnityEngine;

public class EnemyRainGroup : MonoBehaviour {
    [SerializeField] private GameObject[] rainEnemies; // kéo tất cả enemy/trap đã disable vào đây

    public void ActivateAll() {
        foreach (var e in rainEnemies)
        {
            if (e != null) e.SetActive(true);
        }
        Debug.Log("[EnemyRainGroup] Enabled all rain enemies!");
    }
}
