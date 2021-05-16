using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    private Rigidbody2D rigidbody2D;
    private Animator animator;
    private float horizontal;
    private bool grounded;

    public GameObject bulledPrefab;

    private float lastShoot;
    private int health;
    public GameObject [] hearts;

    public float fallMultiplier = 0.5f;

    public float lowJumpMultiplier = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        health = hearts.Length;
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if(horizontal < 0.0f)
        {
            transform.localScale = new Vector3(-5.0f, 5.0f, 5.0f);
        }
        else if(horizontal > 0.0f)
        {
            transform.localScale = new Vector3(5.0f, 5.0f, 5.0f);
        }

        animator.SetBool("running", horizontal != 0.0f);

        // Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);
        // if (Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
        // {
        //     grounded = true;
        // }
        // else
        // {
        //     grounded = false;
        // }
       // Debug.Log("entra " + CheckGround.isGrounded);
        if (Input.GetKeyDown(KeyCode.Space) && CheckGround.isGrounded)
        {
            jump();
        }

        if (Input.GetMouseButtonDown(0) && Time.time > lastShoot + 1f)
        {
            shoot();
            lastShoot = Time.time;
        }

    }

    private void shoot()
    {
        Vector3 diretion;
        if(transform.localScale.x == 5.0f)
        {
            diretion = Vector2.right;
        }
        else
        {
            diretion = Vector2.left;
        }
        GameObject bulled = Instantiate(bulledPrefab, transform.position + diretion* 0.1f, Quaternion.identity);
        bulled.GetComponent<bulletScript>().setDirection(diretion);
    }

    private void jump()
    {
        rigidbody2D.AddForce(Vector2.up * jumpForce);
        // if (rigidbody2D.velocity.y < 0)
        // {
        //     rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
        // }

        // if (rigidbody2D.velocity.y > 0 && !Input.GetKeyDown(KeyCode.Space))
        // {
        //     rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * lowJumpMultiplier * Time.deltaTime;
        // }
    }

    private void FixedUpdate()
    {
        rigidbody2D.velocity = new Vector2(horizontal * speed, rigidbody2D.velocity.y);
    }

    public void Hit()
    {
        if(health > 0)
        {
            Debug.Log("enta a hit");
            health = health - 1;
            Destroy(hearts[health].gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
