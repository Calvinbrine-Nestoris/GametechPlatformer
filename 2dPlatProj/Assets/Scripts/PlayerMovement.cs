using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public PlayerControllerSettings settings;
    Rigidbody2D rb;
    public Animator runAnimation;
    public SpriteRenderer facing;
    float speedX = 0f;
    float direction = 1f;
    bool isGrounded;
    float coyoteTime = 0.3f;
    bool doubleJump = true;
    public static int score = 0;
    float platMovement;
    string scene;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        runAnimation = GetComponent<Animator>();
        facing = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, settings.groundCheckDistance, settings.layerMask);
        Debug.DrawLine(transform.position, transform.position + Vector3.down * settings.groundCheckDistance);
        if (hit.collider != null)
        {
            isGrounded = true;
            coyoteTime = settings.maxCoyoteTime;
            doubleJump = true;
        }
        else
        {
            isGrounded = false;
        }
    }
    void Update()
    {
        direction = Input.GetAxis("Horizontal");
        if (direction > 0f)
        {
            facing.flipX = false;
        }
        if (direction < 0f)
        {
            facing.flipX = true;
        }
        rb.velocity = new Vector2(speedX + platMovement, rb.velocity.y);
        if (!isGrounded)
        {
            coyoteTime -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) == true || Input.GetKeyDown(KeyCode.A) == true)
        {
            speedX -= settings.speedXValue;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) == true || Input.GetKeyDown(KeyCode.D) == true)
        {
            speedX += settings.speedXValue;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) == true || Input.GetKeyUp(KeyCode.A) == true)
        {
            speedX += settings.speedXValue;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) == true || Input.GetKeyUp(KeyCode.D) == true)
        {
            speedX -= settings.speedXValue;
        }
        if (Input.GetKeyDown(KeyCode.Space) == true || Input.GetKeyDown(KeyCode.W) == true || Input.GetKeyDown(KeyCode.UpArrow) == true)
        {
            if (isGrounded == true || coyoteTime > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, settings.speedY);
            }
            else if (doubleJump == true)
            {
                rb.velocity = new Vector2(rb.velocity.x, settings.speedY);
                doubleJump = false;
            }
        }
        runAnimation.SetFloat("speedX", Mathf.Abs(speedX));
        runAnimation.SetBool("onGround", isGrounded);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeathBox"))
        {
            score = 0;
            scene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(scene);
        }
        if (collision.CompareTag("Coin"))
        {
            score += 1;
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Flagpole"))
        {
            if (SceneManager.GetActiveScene().name == "Level1")
            {
                SceneManager.LoadScene("Level2");
            }
            else if (SceneManager.GetActiveScene().name == "Level2")
            {
                SceneManager.LoadScene("Level3");
            }
            else if (SceneManager.GetActiveScene().name == "Level3")
            {
                SceneManager.LoadScene("EndScreen");
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            platMovement = MovingPlatform.platSpeed;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            platMovement = 0;
        }
    }
}
