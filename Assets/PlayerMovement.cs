using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D player;

    public GroundChecker groundCheck;

    private bool facingRight = true;

    public float moveSpeed = 10f;
    public float maxSpeed = 50f;
    public float jumpHeight = 30f;
    public float dashSpeed = 30f;
    public float breakRange = 10f;
    public float dashJumpSpeed = 100f;
    
    public Transform dashRay;
    public LayerMask dashBreakLayers;

    public Animator animator;

    float axis;
    Vector3 zero = Vector3.zero;

    private float DEFAULT_DRAG = 40f;
    private float JUMP_DRAG = 1f;
    private float COUNTER_DRAG = 70f;

    private bool usedDash = false;
    private bool inDash = false;
    public float dashStartDuration = 0.1f;
    private float dashTime = 0;

    private float timeSinceDash = 0f;
    private float longJumpTime = 0.1f;

    void Start()
    {
       // audioManager = FindObjectOfType<AudioManager>();

    }

    // Update is called once per frame
    void Update()
    {

        float horz = Input.GetAxisRaw("Horizontal");

        if (horz < 0 && facingRight)
            Flip();
        else if (horz > 0 && !facingRight)
            Flip();

        axis = horz;

        Vector2 direction = facingRight ? Vector2.right : Vector2.left;

        animator.SetFloat("moveSpeed", Mathf.Abs(Input.GetAxis("Horizontal")) + 1);



        if (groundCheck.onGround)
        {
            animator.ResetTrigger("Jump");
            if (usedDash)
            {
                usedDash = false;
                animator.SetTrigger("gotDash");
            } 
        }
        if (timeSinceDash > 0)
        {
            timeSinceDash -= Time.deltaTime;
        }
        if (groundCheck.onGround && Input.GetKeyDown(KeyCode.Space))
        {
            player.drag = JUMP_DRAG;
            if (timeSinceDash > 0)
            {
                player.AddForce(Vector2.right * jumpHeight, ForceMode2D.Impulse);
            }
            player.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);

            

            player.drag = DEFAULT_DRAG;
            animator.SetTrigger("Jump");

        }
        if(!inDash && !usedDash && Input.GetKeyDown(KeyCode.F))
        {

            RaycastHit2D hit = Physics2D.Raycast(dashRay.position,direction,breakRange,dashBreakLayers);

            Collider2D[] colliders = Physics2D.OverlapCircleAll(hit.point, .5f, dashBreakLayers);

            foreach(Collider2D collider in colliders) {
                Destroy(collider.gameObject);
            }

            player.velocity = Vector2.zero;
            inDash = true;
            dashTime = dashStartDuration;
            usedDash = true;
            animator.SetTrigger("Dash");
            animator.ResetTrigger("gotDash");
        }
    }


    private void FixedUpdate()
    {

        Vector2 velocity = new Vector2(axis * moveSpeed, player.velocity.y);
        if (timeSinceDash > 0)
            velocity = new Vector2(player.velocity.x + axis * moveSpeed, player.velocity.y);

        if (inDash && dashTime > 0)
        {
        //    Debug.Log("Dashing");
        //    Debug.Log("Axis: " + axis);

            Vector2 direction = facingRight ? Vector2.right : Vector2.left;


            velocity = direction * dashSpeed;

            Debug.Log("velocity x: " + velocity.x);
            dashTime -= Time.fixedDeltaTime;
        }
        else if (dashTime <= 0 && inDash)
        {
            Debug.Log("Stopped Dashing");
            inDash = false;
            dashTime = dashStartDuration;
            animator.ResetTrigger("Dash");
            timeSinceDash = longJumpTime;
        }

        if (velocity.x > maxSpeed && !inDash) // took me longer than i like to admit to realize i needed toadd here !inDash
        {
           velocity.x = maxSpeed;
           player.drag = COUNTER_DRAG;
        } 
        else 
        {
            player.drag = DEFAULT_DRAG;
        }

        player.velocity = velocity;
    }

    void Flip()
    {
        if (facingRight)
            transform.localRotation = Quaternion.Euler(0, 180f, 0);
        else
            transform.localRotation = Quaternion.Euler(0, 0, 0);

        facingRight = !facingRight;

    }
}
