using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpForce; // power of jump

    private bool isGrounded;
    private Rigidbody2D rigidbody2d; // power of weight
                                     // work with other object
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public int score;
    public Text scoreText;

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        scoreText.text = score.ToString();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) // we can jump if we push jump
        {
            Jump();
        }

        // Save gamer's coordionats
        Vector3 position = transform.position;

        // add to saved coordionats player's input from bord
        position.x += Input.GetAxis("Horizontal") * speed;
        position.y += Input.GetAxis("Vertical");

        transform.position = position; // new position

        if (Input.GetAxis("Horizontal") != 0)
        {
            if (Input.GetAxis("Horizontal") > 0) // Right
            {
                spriteRenderer.flipX = false;
            }
            else if (Input.GetAxis("Horizontal") < 0) // Left
            {
                spriteRenderer.flipX = true;
            }

            animator.SetInteger("State", 1);
        }
        else
        {
            animator.SetInteger("State", 0);
        }
    }
    private void Jump()
    {
        isGrounded = false; // add impulse to the object
        rigidbody2d.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    public void AddCoin(int count)
    {
        score += count;

        scoreText.text = score.ToString();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true; // we reached the ground
        }
    }

    public void OnCollisionExit2D(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true; // we reached the ground
        }
    }
}
