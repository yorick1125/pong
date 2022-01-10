using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Ball : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public Vector3 startPosition;
    public GameObject game;
    private float currentVelocityX;
    private float currentVelocityY;
    private Vector2 lastVel;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        lastVel = rb.velocity;
    }

    // Update is called once per frame
    void Update()
    {
        // if velocity changes
        if(lastVel != rb.velocity)
        {
            lastVel = rb.velocity;
            print(rb.velocity);
        }
    }

    public void Reset()
    {
        rb.velocity = Vector2.zero;
        transform.position = startPosition;
    }

    public void Launch(int servingPlayer = 1)
    {
        float x = servingPlayer == 2 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;
        speed = Random.Range(5, 10);
        rb.velocity = new Vector2(speed * x, speed * y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (didCollide(collision, "CpuGoal"))
        {
            Debug.Log("Player 1 Scored...");
            game.GetComponent<Game>().Player1Scored();
        }
        else if (didCollide(collision, "PlayerGoal"))
        {
            Debug.Log("Player 2 Scored...");
            game.GetComponent<Game>().Player2Scored();
        }
        else if(didCollide(collision, "TopWall") || didCollide(collision, "BottomWall"))
        {
            currentVelocityY = -currentVelocityY;
            // i don't know why but if i dont add a - before currentVelocityX the x velocity keeps changing
            // also the reason i created currentVelocityX
            rb.velocity.Set(rb.velocity.x, -rb.velocity.y);
            print("hey");
            //rb.velocity = new Vector2(-currentVelocityX, currentVelocityY);
        }
        else if(didCollide(collision, "PlayerPaddle") || didCollide(collision, "CpuPaddle")){
            rb.velocity.Set(rb.velocity.x * 1.03f, rb.velocity.y);
            if(rb.velocity.y > 0)
            {
                RandomizeYVelocity();
            }
            //if(currentVelocityY < 0)
            //{
            //    currentVelocityY = Random.Range(4, 5);
            //}
            //else
            //{
            //    currentVelocityY = -Random.Range(4, 5);
            //}
            //rb.velocity = new Vector2(currentVelocityX, currentVelocityY);
        }

    }

    private bool didCollide(Collision2D collision, string name)
    {
        return collision.collider.name == name;
    }

    private Vector2 RandomizeYVelocity()
    {
        float randomY = rb.velocity.y > 0 ? Random.Range(5, 10) : Random.Range(-10, -5);
        rb.velocity.Set(rb.velocity.x, randomY);
        return rb.velocity;
    }




}
