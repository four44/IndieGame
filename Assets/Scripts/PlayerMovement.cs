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

        //Z�plama y ekseninde oldu�u i�in y de�eri al�r x de�eri mevcut konuma g�re de�i�ken.
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


        // direction horizontal oldu�u i�in pozitif de�erde sa�a negatif de�erde sola y�n al�r. 
        if (direction > 0f)
        {
            movement = ChangeMovement.running;
            // Sa� y�nde ilerlemek i�in sola flip false.
            sprite.flipX = false;
        }
        else if (direction < 0f)
        {
            movement = ChangeMovement.running;
            //sol y�nde ilerlerken sola d�nmek i�in (flip to left)
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
