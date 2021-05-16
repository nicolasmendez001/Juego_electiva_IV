using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    public float speed;
    private Vector2 direction;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        rigidbody2D.velocity = direction * speed;
    }

    public void setDirection(Vector2 direction)
    {
        this.direction = direction;
    }

    public void destroyBullet()
    {
        Destroy(gameObject);
    }

}
