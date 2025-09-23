using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayGame() {
        SceneManager.LoadScene("Level_1");
    }

    public void QuitGame() {
        #if UNITY_EDITOR
                // Dừng Play Mode trong Editor
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                // Thoát ứng dụng khi đã build
                Application.Quit();
        #endif
    }
}
