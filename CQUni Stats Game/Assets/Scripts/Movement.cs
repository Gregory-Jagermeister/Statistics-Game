using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D player;
    private Vector3 velocity;
    public float speed = 10f;

    private void Start()
    {
        player = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //set velocity to equal a new Vector3 based on input on the Horizontal and Vertical axises as the x and y values and a z vaule of 0. 
        //Normalize this vector then multiply the x and y values by the speed variable to define movemnet speed. 
        //Finally use this vector to translate the transform of this gameObject

        velocity = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        //velocity.Normalize();
        //velocity.x = velocity.x * speed;
        //velocity.y = velocity.y * speed;
        //transform.Translate( velocity * Time.deltaTime);

        //Daniels Edit
        player.MovePosition(transform.position + velocity * Time.fixedDeltaTime * speed);

    }
}
