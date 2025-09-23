using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour {
    [SerializeField] GameObject panel;
    public static bool IsPaused { get; private set; }
    public static System.Action OnPaused;
    public System.Action OnResumed;

    float prevScale = 1f;
    private void Awake() {
        if (panel)
        {
            panel.SetActive(false);
            IsPaused = false;
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) Toggle();
    }
    public void Toggle() { if (IsPaused) Resume(); else Pause(); }

    public void Pause() {
        if (IsPaused) return;
        IsPaused = true;
        prevScale = Time.timeScale;
        Time.timeScale = 0f;
        panel.SetActive(true);
        OnPaused?.Invoke();

    }
    public void Resume() {
        if (!IsPaused) return;
        IsPaused = false;
        Time.timeScale = prevScale <= 0f ? 1f : prevScale;
        panel.SetActive(false);
        OnResumed?.Invoke();
    }

    public void BackToMenu() {
        GameManager.I.GotoMenu();
    }
}
