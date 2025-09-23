using UnityEngine;
using System.Collections;

public class EnemyDirector : MonoBehaviour {
    [Header("Enemy Rain Spawner")]
    [SerializeField] GameObject enemyPrefab;   // prefab enemy rơi xuống
    [SerializeField] Transform leftBound;      // Empty ở biên trái màn
    [SerializeField] Transform rightBound;     // Empty ở biên phải màn
    [SerializeField] Transform yReference;     // Player
    [SerializeField] float heightOffset = 6f;  // rơi từ trên ref này + offset

    public static float SpeedMultiplier { get; private set; } = 1f;
    public static float SpawnRateMultiplier { get; private set; } = 1f;

    Coroutine frenzyCR, rainCR;
    void Awake() {
        ResetFrenzy();
    }
    // Gọi từ MegaCoin (Lv1)
    public void EnterFrenzy(float speedMult, float spawnMult, float duration) {
        if (frenzyCR != null) StopCoroutine(frenzyCR);
        frenzyCR = StartCoroutine(FrenzyCo(speedMult, spawnMult, duration));
    }

    IEnumerator FrenzyCo(float s, float r, float dur) {
        SpeedMultiplier = s;
        SpawnRateMultiplier = r;
        yield return new WaitForSeconds(dur);
        SpeedMultiplier = 1f;
        SpawnRateMultiplier = 1f;
    }

    // Gọi từ MegaCoin (Lv2)
    public void StartEnemyRain(int count, float height, float interval) {
        if (enemyPrefab == null || leftBound == null || rightBound == null)
        {
            Debug.LogWarning("[EnemyDirector] Missing prefab/bounds for EnemyRain");
            return;
        }
        if (rainCR != null) StopCoroutine(rainCR);
        rainCR = StartCoroutine(EnemyRainCo(count, height > 0 ? height : heightOffset, interval));
    }

    IEnumerator EnemyRainCo(int count, float h, float interval) {
        float refY = 0f;
        if (yReference != null) refY = yReference.position.y;
        else if (Camera.main != null) refY = Camera.main.transform.position.y;

        float spawnY = refY + h;

        for (int i = 0; i < count; i++)
        {
            float x = Random.Range(leftBound.position.x, rightBound.position.x);
            Vector3 pos = new Vector3(x, spawnY, 0f);
            Instantiate(enemyPrefab, pos, Quaternion.identity);
            yield return new WaitForSeconds(interval);
        }
    }
    public static void ResetFrenzy() {
        SpeedMultiplier = 1f;
        SpawnRateMultiplier = 1f;
    }
}
