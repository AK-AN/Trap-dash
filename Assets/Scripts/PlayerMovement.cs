using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rib;
    private BoxCollider2D coll;
    private Animator animref;
    private SpriteRenderer sprite;
    
    [SerializeField] private LayerMask jumpableGround;
    
    private float dirnX = 0f;
    // Adjust these variables to fine-tune the movement
   [SerializeField] private float moveSpeed = 7f;
   [SerializeField] private float jumpForce = 14f;

    private enum MovementState {idle,running,jumping,falling}
    [SerializeField] private AudioSource JumpSoundEffect;

    private void Start()
    {
        rib = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        animref = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        dirnX = Input.GetAxisRaw("Horizontal");
        rib.velocity = new Vector2(dirnX * moveSpeed, rib.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            JumpSoundEffect.Play();
            rib.velocity = new Vector2(rib.velocity.x, jumpForce);
        }
        
        Animationupdate();
    }

    private void Animationupdate()
    {
        MovementState state;
         
        if(dirnX > 0f){
           state = MovementState.running;
            sprite.flipX = false;
        }
        else if(dirnX < 0f){
              state = MovementState.running;
             sprite.flipX = true;
        }
        else{
              state = MovementState.idle;
        }
        if(rib.velocity.y>.1f){
            state = MovementState.jumping;
        }
        else if (rib.velocity.y<-.1f){
            state = MovementState.falling;
        }
         animref.SetInteger("state",(int)state);
    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center,coll.bounds.size,0f,Vector2.down,.1f,jumpableGround);
    }
}
