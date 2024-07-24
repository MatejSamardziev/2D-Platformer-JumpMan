using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator anim;
    private float dirX=0f;
    private SpriteRenderer sprite;
    private int JumpCounter = 0;
    private bool isFirstJump = true;
   [SerializeField] private AudioSource jumpSoundEffect;
   [SerializeField] private float moveSpeed = 7f;
   [SerializeField] private float jumpSpeed = 14f;
   [SerializeField] private LayerMask jumpableGround; 
    private enum MovementState { idle,running,jumping,falling,secondJump};
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite= GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

       dirX = Input.GetAxisRaw("Horizontal");//Horizontal e imeto na buttonot
        //ako pises getaxis samo postepeno zavrsuva odkako ke pustis kopceto so getaxisraw odma zavrsuva so dvizenje
        //dirX ide od -1 do 1 ako si na joystick moze i decimala da bide

        rb.velocity = new Vector2(dirX*moveSpeed,rb.velocity.y); //ne sakame y da e 0 poso mozda predhodniot frame sme ripnale sledniot frame na 0 ke go vrati

        if (IsGrounded() && rb.velocity.y <= 0.1f)
        {
            JumpCounter = 0;
        }


        if ((Input.GetButtonDown("Jump")))
        {
            Debug.Log(JumpCounter);
            handleJump();
            
        }

        UpdateAnimationState();

       
    }

    private void handleJump()
    {
        if (JumpCounter < 2)
        {
            if (JumpCounter == 0)
            {
                isFirstJump = true;
            }
            if(JumpCounter == 1)
            {
                isFirstJump = false;
            }
            JumpCounter++;
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);//x y
                                                                //isto i za x ne sakame 0 da bide
            jumpSoundEffect.Play();

        }

        
    }


    private void UpdateAnimationState()
    {

        MovementState state;

        if (dirX > 0)
        {

            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0)
        {

            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }
        //nikad ne bilo 0 velocity zatoa vaka
        if (rb.velocity.y > .1f)
        {
            if (isFirstJump)
            {
                state = MovementState.jumping;
                
            }
            else if (!isFirstJump)
            {
                state = MovementState.secondJump;
                
            }
           
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);

    }

    private bool IsGrounded()
    {
        bool grounded = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
        return grounded; 
    }

    
}
