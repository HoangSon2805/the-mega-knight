using UnityEngine;

public class GroundManager : MonoBehaviour {
    [SerializeField] GameObject[] groundObjects; // Tilemap/Platform/... kéo vào đây

    public void CollapseAll() {
        foreach (var g in groundObjects)
            if (g) g.SetActive(false);
    }

    // Nếu muốn khôi phục khi restart level:
    public void RestoreAll() {
        foreach (var g in groundObjects)
            if (g) g.SetActive(true);
    }
}
