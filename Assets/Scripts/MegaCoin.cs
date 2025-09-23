using UnityEngine;
using System.Collections;

public class MegaCoin : MonoBehaviour
{
    public enum Effect {Frenzy, EnemyRain, CollapseGround, Safe}
    
    [Header("Genaral")]
    [SerializeField] int scoreOnPickup = 100;
    [SerializeField] Effect effect = Effect.Frenzy;
    [SerializeField] bool destroyOnPickup = true;
    [Header("Frenzy (Lv1)")]
    [SerializeField] float frenzySpeedMult = 2f;
    [SerializeField] float frenzySpawnMult = 2f;
    [SerializeField] float frenzyDuration = 10f;


    [Header("Collapse Ground (Lv3)")]
    
    [SerializeField] private GameObject megacoinGround;
    bool used;

    void OnTriggerEnter2D(Collider2D other) {
        if (used || !other.CompareTag("Player")) return;
        used = true;

        var gm = FindAnyObjectByType<GameManager>();
        var audio = FindAnyObjectByType<AudioManager>();

        // 1) Cộng điểm trước
        gm?.AddScore(scoreOnPickup);
        audio?.PlayCoinSound(); // nếu muốn sfx riêng thì tạo PlayMegaCoinSound()

        // 2) Kích hoạt hiệu ứng theo màn
        switch (effect)
        {
            case Effect.Frenzy:        // Lv1 – địch “điên”
                FindAnyObjectByType<EnemyDirector>()?.EnterFrenzy(
                    frenzySpeedMult, frenzySpawnMult, frenzyDuration);
                break;

            case Effect.EnemyRain:     // Lv2 – địch rơi từ trời
                var group = FindAnyObjectByType<EnemyRainGroup>();
                if (group != null) group.ActivateAll();
                break;

            case Effect.CollapseGround: // Lv3 – sập nền rồi thua
                if (megacoinGround) megacoinGround.SetActive(false);
                StartCoroutine(GameOverAfterDelay(gm));
                break;

            case Effect.Safe:          // Lv4 – an toàn, chỉ +100
                // không làm gì thêm
                break;
        }

        if (destroyOnPickup) gameObject.SetActive(false);
    }

    private IEnumerator GameOverAfterDelay(GameManager gm) {
        yield return new WaitForSecondsRealtime(0.3f); // cho player rơi một chút
        gm?.GameOver();
    }
}

