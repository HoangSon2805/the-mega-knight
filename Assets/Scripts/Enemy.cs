using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] private float speed = 2f;   // tốc cơ bản
    [SerializeField] private float distance = 5f;
    private Vector3 startPos;
    private bool movingRight = true;

    // lưu tốc gốc để không bị “tăng dần” sau mỗi lần Frenzy
    private float baseSpeed;

    void Start() {
        startPos = transform.position;
        baseSpeed = speed;
    }

    void Update() {
        // tốc hiện tại = tốc gốc * hệ số Frenzy (1f khi không Frenzy)
        float currentSpeed = baseSpeed * EnemyDirector.SpeedMultiplier;

        float leftBound = startPos.x - distance;
        float rightBound = startPos.x + distance;

        if (movingRight)
        {
            transform.Translate(Vector2.right * currentSpeed * Time.deltaTime);
            if (transform.position.x >= rightBound)
            {
                movingRight = false;
                Flip();
            }
        } else
        {
            transform.Translate(Vector2.left * currentSpeed * Time.deltaTime);
            if (transform.position.x <= leftBound)
            {
                movingRight = true;
                Flip();
            }
        }
    }

    void Flip() {
        var s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
    }
}
