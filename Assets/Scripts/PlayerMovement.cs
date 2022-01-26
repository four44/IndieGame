using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private BoxCollider2D boxCollider;
    private Rigidbody2D rigidbody;
    private SpriteRenderer sprite;
    private Animator animator;

    [SerializeField] private LayerMask JumpingGround;
    [SerializeField] private AudioSource JumpingSound;

    private enum ChangeMovement {idle, running,jumping,falling }
    

    private float direction = 0f;
    [SerializeField] private float WalkingSpeed = 7f;
    [SerializeField] private float JumpingHeight = 13F;

    // Start is called before the first frame update
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    private void Update()
    {
        direction = Input.GetAxisRaw("Horizontal");
        rigidbody.velocity = new Vector2(direction * WalkingSpeed, rigidbody.velocity.y);

        //Zýplama y ekseninde olduðu için y deðeri alýr x deðeri mevcut konuma göre deðiþken.
        if (Input.GetButtonDown("Jump") && JumpControl())
        {
            JumpingSound.Play();
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, JumpingHeight);
        }
        
        ChangeAnimation();
        
    }

    private void ChangeAnimation()
    {
        ChangeMovement movement;


        // direction horizontal olduðu için pozitif deðerde saða negatif deðerde sola yön alýr. 
        if (direction > 0f)
        {
            movement = ChangeMovement.running;
            // Sað yönde ilerlemek için sola flip false.
            sprite.flipX = false;
        }
        else if (direction < 0f)
        {
            movement = ChangeMovement.running;
            //sol yönde ilerlerken sola dönmek için (flip to left)
            sprite.flipX = true;
        }
        else
        {
            movement = ChangeMovement.idle;
        }
        if (rigidbody.velocity.y > .1f)
        {
            movement = ChangeMovement.jumping;
        }
        else if (rigidbody.velocity.y < -.1f)
        {
            movement = ChangeMovement.falling;
        }

        animator.SetInteger("movement", (int)movement);
    }

    private bool JumpControl()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, JumpingGround);
    }
}
