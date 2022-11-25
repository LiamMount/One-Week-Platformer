using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Necessities")]
    //Base Stuff
    Rigidbody2D rb;
    CapsuleCollider2D capCol;
    [SerializeField] private LayerMask groundLayerMask;

    //Camera thingy
    public GameObject camProp;

    //Animation
    public Animator anim;

    public GameObject deathParticles;

    [Header("Move Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 15f;

    public float accelAmount;
    public float decelAmount;
    public float gravityScale = 4f;
    public float fallGravityMultiplier = 1.5f;

    //Horizontal move values
    public const string LEFT = "left";
    public const string RIGHT = "right";

    string horizontalButtonPressed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        capCol = GetComponent<CapsuleCollider2D>();

        PlayerVariables.isWalking = false;
        PlayerVariables.isJumping = false;
    }

    void Update()
    {
        GetHorizontalMovement();
    }

    private void FixedUpdate()
    {
        if (!PlayerVariables.levelComplete)
        {
            DoHorizontalMovement();
            DoJump();
        }
    }

    // MOVEMENT

    public void GetHorizontalMovement()
    {
        //Right Controller
        if (Input.GetAxisRaw("HorizontalStick") > 0.1)
        {
            horizontalButtonPressed = RIGHT;
        }
        //Left Controller
        else if (Input.GetAxisRaw("HorizontalStick") < -0.1)
        {
            horizontalButtonPressed = LEFT;
        }

        //Right
        else if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.A))
        {
            horizontalButtonPressed = RIGHT;
        }
        //Left
        else if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.D))
        {
            horizontalButtonPressed = LEFT;
        }
        //Stop
        else
        {
            horizontalButtonPressed = null;
        }
    }

    public void DoHorizontalMovement()
    {
        //Right
        if (horizontalButtonPressed == RIGHT)
        {
            PlayerVariables.isWalking = true;
            anim.SetBool("walking", true);

            if (transform.rotation.y != 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            float targetSpeed = moveSpeed;
            float speedDif = targetSpeed - rb.velocity.x;
            float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? accelAmount : decelAmount;
            float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, 0.9f) * Mathf.Sign(speedDif);

            rb.AddForce(movement * Vector2.right);

            //rb.velocity = new Vector2(moveSpeed, rb.velocity.y); //Old
        }
        //Left
        else if (horizontalButtonPressed == LEFT)
        {
            PlayerVariables.isWalking = true;
            anim.SetBool("walking", true);

            if (transform.rotation.y != 180)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            float targetSpeed = -moveSpeed;
            float speedDif = targetSpeed - rb.velocity.x;
            float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? accelAmount : decelAmount;
            float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, 0.9f) * Mathf.Sign(speedDif);

            rb.AddForce(movement * Vector2.right);

            //rb.velocity = new Vector2(-moveSpeed, rb.velocity.y); //Old
        }
        //Stop
        else
        {
            PlayerVariables.isWalking = false;
            anim.SetBool("walking", false);

            if (IsGrounded())
            {
                //Type 1
                float targetSpeed = 0 * moveSpeed;
                float speedDif = targetSpeed - rb.velocity.x;
                float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? accelAmount : decelAmount;
                float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, 0.9f) * Mathf.Sign(speedDif);

                rb.AddForce(movement * Vector2.right);

                //Type 2
                float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(0.2f));
                amount *= Mathf.Sign(rb.velocity.x);
                rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
            }

            //PlayerVariables.isWalking = false; //Old
            //rb.velocity = new Vector2(0, rb.velocity.y); //Old
        }
    }

    public void DoJump()
    {
        //Jumps
        if (IsGrounded() && Input.GetButton("Jump"))
        {
            //Start jump animation
            //rb.velocity = new Vector2(rb.velocity.x, jumpForce); //Old
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            PlayerVariables.isJumping = true;
        }

        else if (IsGrounded() && !Input.GetButton("Jump"))
        {
            PlayerVariables.isJumping = false;
        }

        //Short Jumps/Jump Cuts
        if (PlayerVariables.isJumping && Input.GetButtonUp("Jump") && (rb.velocity.y > 0))
        {
            //PlayerVariables.isJumping = false;

            rb.AddForce(Vector2.down * rb.velocity.y * (1f - 0.5f), ForceMode2D.Impulse);
        }

        //Fall Gravity
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = gravityScale * fallGravityMultiplier;
        }
        else
        {
            rb.gravityScale = gravityScale;
        }
    }

    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(capCol.bounds.center, capCol.bounds.size * 0.9f, 0f, Vector2.down, 0.1f, groundLayerMask);
        if (hit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Bullet")
        {
            GameObject newDeath;
            newDeath = Instantiate(deathParticles, transform.position, transform.rotation);
            //Sounds?

            FindObjectOfType<RespawnManager>().Respawn();

            GameObject newCamProp;
            newCamProp = Instantiate(camProp, this.transform.position, this.transform.rotation);

            Destroy(this.gameObject);
        }
    }
}
