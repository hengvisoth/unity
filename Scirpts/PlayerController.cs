using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region RequireComponent
[RequireComponent(typeof(Animator))]
#endregion
public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    public Rigidbody2D rb2d;
    public float jumpForce = 5f;
    public float moveSpeed = 2f;
    public float theScaleX;
    [Header("Input Manager")]
    private KeyCode rightKey = KeyCode.D;
    private KeyCode leftKey = KeyCode.A;
    private KeyCode jumpKey = KeyCode.W;
    private KeyCode crouchKey = KeyCode.S;
    public int moveDir;

    [Header("GroundManager")]
    public bool isGround;

    public float groundCheckRadius = 0.34f;
    public LayerMask whatIsGround;

    [Header("Action Manager")]
    public bool isFalling;
    void Start()
    {
        anim = GetComponent<Animator>();
        Debug.Log(anim);
        rb2d = GetComponent<Rigidbody2D>();
        Debug.Log(rb2d.name);
    }
    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(transform.position, groundCheckRadius, LayerMask.GetMask("Ground"));
        isFalling = rb2d.velocity.y < 0;
        if (!isGround)
        {
            anim.SetLayerWeight(1, 1);
            if (isFalling)
            {
                anim.SetInteger("jump", 0);
            }
            else
            {
                anim.SetInteger("jump", 1);
            }
        }
        else
        {
            anim.SetLayerWeight(1, 0);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, groundCheckRadius);
    }

    // Update is called once per frame
    private void Update()
    {
        HandlerMovement();
        HandlerJump();
        HandlerCrouch();
    }
    void HandlerMovement()
    {
        theScaleX = transform.localScale.x;
        #region MovementController
        if (Input.GetKey(rightKey))
            moveDir = 1;
        else if (Input.GetKey(leftKey))
            moveDir = -1;
        else
            moveDir = 0;
        #endregion
        // Vector2. right == (1,0)
        // Time.deltaTime == 0.0012341 ... 
        if (moveDir != 0)
        {
            transform.Translate(Vector2.right * moveDir * Time.deltaTime * moveSpeed);
            // Flip character 
            float theScale = Mathf.Abs(theScaleX);
            transform.localScale = new Vector2(theScale * moveDir, theScale);
        }
        // Run animation 
        anim.SetInteger("run", Mathf.Abs(moveDir));
    }
    void HandlerJump()
    {
        if (Input.GetKeyDown(jumpKey))
        {
            if (isGround)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }


        }
    }
    void HandlerCrouch()
    {
        if (Input.GetKey(crouchKey))
        {
            anim.SetBool("isCrouching", true);
        }
        else
        {
            anim.SetBool("isCrouching", false);
        }
    }
}

