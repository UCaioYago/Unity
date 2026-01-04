using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator playerAnim;
    private Rigidbody2D rbPlayer;
    public float speed;
    private SpriteRenderer sr;
    public float jumpForce;
    public bool infloor = true;
    public bool doubleJump;
    public bool triploJump;

    private GameController gcPlayer;
    private void Start()
    {
        gcPlayer = GameController.gc;
        gcPlayer.coins = 0;
        playerAnim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rbPlayer = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void Update()
    {
        Jump();
    }
    private void MovePlayer()
    {
        float horizontalMoviment = Input.GetAxisRaw("Horizontal");
        //Debug.Log(horizontalMoviment);
        //transform.position += new Vector3(horizontalMoviment * Time.deltaTime * speed,0,0);
        rbPlayer.linearVelocity = new Vector2(horizontalMoviment * speed, rbPlayer.linearVelocity.y);

        if (horizontalMoviment > 0)
        {
            playerAnim.SetBool("Walk", true);
            sr.flipX = false;
        }
        else if (horizontalMoviment < 0)
        {
            playerAnim.SetBool("Walk", true);
            sr.flipX = true;
        }
        else
        {
            playerAnim.SetBool("Walk", false);
        }
    }
    void Jump()
    {   
        if (Input.GetButtonDown("Jump"))
        {
            if (infloor)
            {
                rbPlayer.linearVelocity = Vector2.zero;
                playerAnim.SetBool("Jump", true);
                rbPlayer.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                infloor = false;
                doubleJump = true;
            }
            else if (!infloor && doubleJump)
            {
                rbPlayer.linearVelocity = Vector2.zero;
                playerAnim.SetBool("Jump", true);
                rbPlayer.AddForce(new Vector2(0, jumpForce * 2), ForceMode2D.Impulse);
                infloor = false;
                doubleJump = false;
                triploJump = true;
            }
            else if (!infloor && triploJump)
            {
                rbPlayer.linearVelocity = Vector2.zero;
                playerAnim.SetBool("Jump", true);
                rbPlayer.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                infloor = false;
                doubleJump = false;
                triploJump = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            playerAnim.SetBool("Jump", false);
            infloor = true;
            doubleJump = false;
            triploJump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coins")
        {
            Destroy(collision.gameObject);
            gcPlayer.coins++;
            GameController.gc.RefreshScreen();
        }
        if (collision.gameObject.tag == "Enemy")
        {
            rbPlayer.linearVelocity = Vector2.zero;
            rbPlayer.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            collision.gameObject.GetComponent<SpriteRenderer>().flipY = true;
            collision.gameObject.GetComponent<Enemy>().enabled = false;
            collision.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            Destroy(collision.gameObject, 1f);
        }
    }
}