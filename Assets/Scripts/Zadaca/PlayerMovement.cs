using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rb;
    
    private Vector3 direction;
    private float regularSpeed;

    private void Start()
    {
        regularSpeed = speed;
    }

    private void Update()
    {
        direction = Vector3.zero;
        if(Input.GetKey(KeyCode.W))
        {
            direction += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector3.back;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector3.right;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = direction.normalized * speed;
    }

    public void ReduceSpeed(float speedReductionFactor)
    {
        speed -= speedReductionFactor * speed;
    }

    public void ResetSpeed()
    {
        speed = regularSpeed;    
    }
}
