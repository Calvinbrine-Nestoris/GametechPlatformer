using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    float speedX = 0f;
    float speedY = 15f;
    float speedXValue = 10f;
    float direction = 1f;
    bool isGrounded;
    public LayerMask layerMask;
    float groundCheckDistance = 1.5f;
    float coyoteTime = 0.2f;
    bool doubleJump = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, layerMask);
        Debug.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
        if (hit.collider != null)
        {
            isGrounded = true;
            coyoteTime = 0.2f;
            doubleJump = true;
        }
        else
        {
            isGrounded = false;
        }   
    }
    void Update()
    {
        rb.velocity = new Vector2(speedX, rb.velocity.y);
        if (!isGrounded)
        {
            coyoteTime -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) == true || Input.GetKeyDown(KeyCode.A) == true)
        {
            speedX -= speedXValue;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) == true || Input.GetKeyDown(KeyCode.D) == true)
        {
            speedX += speedXValue;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) == true || Input.GetKeyUp(KeyCode.A) == true)
        {
            speedX += speedXValue;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) == true || Input.GetKeyUp(KeyCode.D) == true)
        {
            speedX -= speedXValue;
        }
        if (Input.GetKeyDown(KeyCode.Space) == true || Input.GetKeyDown(KeyCode.W) == true || Input.GetKeyDown(KeyCode.UpArrow) == true)
        {
            if (isGrounded == true || coyoteTime > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, speedY);   
            }
            if (doubleJump == true)
            {
                rb.velocity = new Vector2(rb.velocity.x, speedY);
                doubleJump = false;
            }

        }
        
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeathBox"))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
