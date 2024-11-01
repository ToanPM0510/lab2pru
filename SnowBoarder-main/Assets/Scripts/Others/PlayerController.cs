using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] float torqueAmount = 1f;
    [SerializeField] float normalSpeed;
    [SerializeField] float boostSpeed;
    private float jumpingPower = 10f;
    SurfaceEffector2D surfaceEffector2D;
    bool canMove = true;
    private bool isJumping;
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;
    Rigidbody2D rb2d;
    CapsuleCollider2D boardCollider;
    private float rotationTime;
    public float RotationTime => rotationTime;



    private AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        boardCollider = GetComponent<CapsuleCollider2D>();
        surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();

    }
    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            RotatePlayer();
            RespondToBoost();
            RespondToJump();
        }
        speedText.text = "Speed: " + rb2d.velocity.magnitude.ToString("F2");
    }
    public void DisableControls()
    {
        canMove = false;
    }
    void RespondToBoost()
    {
        if (Input.GetKey(KeyCode.W))
        {
            surfaceEffector2D.speed = boostSpeed;
        }
        else
        {
            surfaceEffector2D.speed = normalSpeed;
        }
       
    }
    private void RespondToJump()
    {
        if (IsTouchingGroundLayer())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpingPower);
            jumpBufferCounter = 0f;
            StartCoroutine(JumpCooldown());
        }
        if (Input.GetKey(KeyCode.Space) && rb2d.velocity.y > 0f)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * 1f);
            coyoteTimeCounter = 0f;
        }
    }
    void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb2d.AddTorque(torqueAmount);
            rotationTime += Time.deltaTime; // Increment the rotation time
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb2d.AddTorque(-torqueAmount);
            rotationTime += Time.deltaTime; // Increment the rotation time
        }
    }
    bool IsTouchingGroundLayer()
    {
        return (boardCollider.IsTouchingLayers(LayerMask.GetMask("Ground")));
    }

    public void IncreaseCoinCount(int amount)
    {
        GameManager.Instance.AddCoins(amount);
    }

    public void UpdateCoinDisplay(int coinCount)
    {
        scoreText.text = " : " + coinCount.ToString();
    }

    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.4f);
        isJumping = false;
    }
}
