using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private CircleCollider2D coll;
    private SpriteRenderer sprite;
    [SerializeField] private AudioSource jumpSoundEffect;


    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    [SerializeField] private LayerMask jumpableground;


    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jump = 5f;

    private int extraJumps;
    public int extraJumpsValue = 2;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;


    private float dirX = 0;

    private enum MovementState { idle, running, jumping }
    private MovementState state = MovementState.idle;

    // Start is called before the first frame update
    void Start()
    {
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<CircleCollider2D>();
    }
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position,checkRadius,jumpableground);
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        UpdateAnimState();
    }


    private void Update()
    {
        if (isGrounded == true)
        {
            extraJumps = extraJumpsValue;
        }

        if (Input.GetButtonDown("Jump") && extraJumps > 0)
        {
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, jump);
            extraJumps--;
            jumpTimeCounter = jumpTime;
            jumpSoundEffect.Play();
        }

        else if (Input.GetButtonDown("Jump") && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
            jumpSoundEffect.Play();
        }

        if (Input.GetButton("Jump") && extraJumps > 0 && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jump);
                jumpTimeCounter -= Time.deltaTime;
                
            }
            else { isJumping = false; }
        }
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
        UpdateAnimState();
    }

    private void UpdateAnimState()
    {
        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }

        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }

        else if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;

        }

        else
        {
            state = MovementState.idle;
        }

        anim.SetInteger("state", (int)state);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EndingDoor"))
        {
            if (isGrounded == false)
            {
                Invoke("StaticRigidbody", 0.1f);
            }

            else
            {
                StaticRigidbody();
            }
        }
    }

    private void StaticRigidbody()
    {
        rb.bodyType = RigidbodyType2D.Static;
    }

}
