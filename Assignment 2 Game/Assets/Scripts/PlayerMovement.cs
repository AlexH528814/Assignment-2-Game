using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jump = 5f;

    private float dirX = 0;

    private enum MovementState { idle, running, jumping}
    private MovementState state = MovementState.idle;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
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

        else if(rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }

        else
        {
            state = MovementState.idle;
        }      

        anim.SetInteger("state", (int)state);
    }
}
