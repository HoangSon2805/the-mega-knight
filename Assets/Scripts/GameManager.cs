using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager I { get; private set; }
    private int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverUi;
    [SerializeField] private GameObject gameWinUi;
    private bool isGameWin = false;
    private bool isGameOver = false;

    void Awake() {
        // Đảm bảo chỉ có 1 GameManager
        if (I != null && I != this)
        {
            Destroy(gameObject);
            return;
        }
        I = this;
        // Giữ lại khi đổi scene
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateScore();
        gameOverUi.SetActive(false);
        gameWinUi.SetActive(false);
    }

  
    public void AddScore(int points) {
        if (!isGameOver && !isGameWin) {
            score += points;
            UpdateScore();
        }
        
    }

    public void UpdateScore() {
        scoreText.text = score.ToString();
    }

    public void GameOver() {
        isGameOver = true;
        score = 0;
        Time.timeScale = 0;
        gameOverUi.SetActive(true);
    }
    public void GameWin() {
        isGameWin = true;
        Time.timeScale = 0;
        gameWinUi.SetActive(true);
        StartCoroutine(LoadNextLevelAfterDelay(2f));
    }
    private System.Collections.IEnumerator LoadNextLevelAfterDelay(float seconds) {
        float t = 0f;
        while (t < seconds)
        {
            t += Time.unscaledDeltaTime;  // không bị ảnh hưởng bởi timeScale
            yield return null;
        }
        LoadNextLevel();
    }
    private void LoadNextLevel() {
        Time.timeScale = 1f;
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;

        // Nếu còn màn → sang màn tiếp theo
        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextIndex);
        } else
        {
            // Hết level → quay về menu
            SceneManager.LoadScene("MainMenu");
        }
    }
    public void RestarGame() {
        isGameOver = false;
        score = 0;
        UpdateScore();
        Time.timeScale = 1;
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }

    public void GotoMenu() {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
    public bool IsGameOver() {
        return isGameOver;
    }
    public bool IsGameWin() {
        return isGameWin;
    }
}
