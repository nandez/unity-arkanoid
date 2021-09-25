using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 150.0f;

    void FixedUpdate()
    {
        // Get Horizontal Input
        float horizontal = Input.GetAxisRaw("Horizontal");

        // Set Velocity (movement direction * speed)
        GetComponent<Rigidbody2D>().velocity = Vector2.right * horizontal * speed;
    }
}
