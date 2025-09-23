using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {

    [Header("Move")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] float groundRadius = 0.18f;

    [Header("Air Jumps")]
    [SerializeField] int baseAirJumps = 0;     // khi chưa power-up (0 = chỉ nhảy từ đất)
    [SerializeField] int poweredAirJumps = 2;  // khi power-up (2 = triple jump)

    private Animator animator;
    private bool isGrounded;
    int airJumpsUsed;

    private Rigidbody2D rb;
    private GameManager gameManager;
    private AudioManager audioManager;
    private PlayerStatus status;
    private void Awake() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindAnyObjectByType<GameManager>();
        audioManager = FindAnyObjectByType<AudioManager>();
        status = GetComponent<PlayerStatus>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (gameManager.IsGameOver()||gameManager.IsGameWin()) return;

        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
        if (isGrounded) airJumpsUsed = 0;

        HandleMovement();
        HandleJump();
        UpdateAnimation();
    }
    private void HandleMovement() {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        if (moveInput > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0) transform.localScale = new Vector3(-1, 1, 1);
    }

    private void HandleJump() {
        if (!Input.GetButtonDown("Jump")) return;

        // Số lần air-jump cho phép tuỳ trạng thái
        bool powered = status != null && status.IsPowered;
        int maxAirJumpsAllowed = powered ? poweredAirJumps : baseAirJumps;

        // Được nhảy nếu đang đứng đất, hoặc còn quota air-jump
        bool canAirJump = airJumpsUsed < maxAirJumpsAllowed;

        if (isGrounded || canAirJump)
        {
            // reset Y cho cú nhảy “đầm”
            rb.linearVelocity= new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            if (!isGrounded)
            {
                // chỉ tăng khi thực hiện air-jump
                airJumpsUsed++;
            }

            if (audioManager) audioManager.PlayJumpSound();
        }
        //if (!Input.GetButtonDown("Jump")) return;

        //if (Input.GetButtonDown("Jump") && isGrounded)
        //{
        //   linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        //    audioManager.PlayJumpSound();
        //}
        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    private void UpdateAnimation() {
        bool isRunning = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
        bool isJumping = !isGrounded;
        animator.SetBool("isRunning",isRunning);
        animator.SetBool("isJumping", isJumping);

    }
}
