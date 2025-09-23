using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed = 2f;

    private Transform currentTarget;
    private Transform player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTarget = pointA;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);
        if ((transform.position - currentTarget.position).sqrMagnitude < 0.01f)
        {
            currentTarget = (currentTarget == pointA) ? pointB : pointA;
            // Debug.Log($"Target = {(currentTarget == pointA ? "pointA" : "pointB")}");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.CompareTag("Player")){
            collision.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
