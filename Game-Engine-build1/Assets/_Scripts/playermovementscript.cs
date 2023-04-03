using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovementscript : MonoBehaviour
{
    // Declare a public reference to the CharacterController component
    public CharacterController controller;

    // Declare public variables for movement speed, gravity, and jump height
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jump = 1f;

    // Declare public references to the ground check transform, ground distance, and ground mask
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    // Declare private variables for velocity and grounded status
    Vector3 velocity;
    bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        // Check if the player is on the ground using a sphere cast
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // If the player is on the ground and falling, set their y velocity to a small negative value to ensure they are on the ground
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Get the player's input for horizontal and vertical movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Calculate the movement vector based on the player's input and the direction they are facing
        Vector3 move = transform.right * x + transform.forward * z;

        // Move the player using the CharacterController component, taking into account movement speed and deltaTime
        controller.Move(move * speed * Time.deltaTime);

        // If the player is on the ground and presses the jump button, set their y velocity to a value that will make them jump
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jump * -2f * gravity);
        }

        // Apply gravity to the player's velocity over time
        velocity.y += gravity * Time.deltaTime;

        // Move the player using the CharacterController component, taking into account the player's velocity and deltaTime
        controller.Move(velocity * Time.deltaTime);
    }
}