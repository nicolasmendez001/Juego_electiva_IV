using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{

    public GameObject bulletPrefab;
    public GameObject player;

    public float lastShoot;

    public float speed;
    public Rigidbody2D rigidbody2d;

    public int health = 2;

    public bool normal = true;

    private float walk = -1f;
    
    void Update()
    {
        if (player == null)
        {
            return ;
        }
        Vector3 direction = transform.position - player.transform.position;
        float distance = Mathf.Abs(player.transform.position.x - transform.position.x);
        Vector2 newPosition;
        if (distance < 5f && normal != true)
        {
            if (direction.x >= 0.0f)
            {
                transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
            } else
            {
                transform.localScale = new Vector3(-3.0f, 3.0f, 3.0f);
            }
            newPosition = Vector2.MoveTowards(rigidbody2d.position, new Vector2(player.transform.position.x, rigidbody2d.position.y), speed * Time.deltaTime);
        } else {
            newPosition = Vector2.MoveTowards(rigidbody2d.position, new Vector2(transform.position.x + walk, rigidbody2d.position.y), speed * Time.deltaTime);
        }
        rigidbody2d.MovePosition(newPosition);
    }

    private void Shoot()
    {
        Vector3 direction;
        if(transform.localScale.x == 3.0f)
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.left;
        }
        GameObject bullet = Instantiate(bulletPrefab, transform.position + direction* 0.1f, Quaternion.identity);
        bullet.GetComponent<bulletScript>().setDirection(direction);
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerMove player = collision.collider.GetComponent<playerMove>();
        if (player != null)
        {
            player.Hit();
        } else {
            Vector3 collPosition = collision.transform.position;
            if (collPosition.x > transform.position.x)
            {
                walk = 1f;
                transform.localScale = new Vector3(-3.0f, 3.0f, 3.0f);
            } 
            else 
            {
                walk = -1f;
                transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
            }
        }
    }

    public void Hit()
    {
        Debug.Log("vida: " + health);
        if(health > 0)
        {
            Debug.Log("entra a hit");
            health = health - 1;
            if (health == 0) {

                player.GetComponent<playerMove>().setText();
                Destroy(gameObject);
            }
        }
    }
}
