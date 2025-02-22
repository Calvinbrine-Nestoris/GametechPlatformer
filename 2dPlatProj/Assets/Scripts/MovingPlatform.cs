using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    static public float platSpeed;
    static public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        platSpeed = -2;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(platSpeed, 0);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            platSpeed *= -1;
        }
    }
}
