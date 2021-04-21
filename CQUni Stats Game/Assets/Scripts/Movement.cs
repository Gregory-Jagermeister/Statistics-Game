using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D player;
    private Vector3 velocity;
    private Animator animator;
    public float speed = 10f;
    private SpriteRenderer sprite;

    private void Start()
    {
        player = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        sprite = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //set velocity to equal a new Vector3 based on input on the Horizontal and Vertical axises as the x and y values and a z vaule of 0. 
        //Normalize this vector then multiply the x and y values by the speed variable to define movemnet speed. 
        //Finally use this vector to translate the transform of this gameObject

        velocity = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        //Daniels Edit

        //If the player is moving then set the player sprite to animate.
        if (velocity.x != 0 || velocity.y != 0)
        {
            animator.SetFloat("Speed", 1);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
        player.MovePosition(transform.position + velocity * Time.fixedDeltaTime * speed);
        sprite.sortingOrder = (int)(transform.position.y * -100);

        if (velocity.x > 0)
        {
            transform.localScale = new Vector2(1, transform.localScale.y);
        }
        else if (velocity.x < 0)
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
        }



    }


}
