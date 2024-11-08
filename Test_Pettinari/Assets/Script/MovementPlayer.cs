using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    private Vector3 lastPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        lastPosition = rb.position;
    }

    private void OnMovement (Vector3 movementDirection)
    {
        if (movementDirection != Vector3.zero)
        {
            float lenght = movementDirection.magnitude;
            if (lenght >1)
            {
                movementDirection /= lenght;
            }

            animator.SetFloat("X", movementDirection.x);
            animator.SetFloat("Z", movementDirection.z);

            animator.SetBool("Movement", true);
        }
        else
        {
            animator.SetBool("Movement", false);
        }
    }

    public void FixedUpdate()
    {
        Vector3 currentPosition = rb.position;
        Vector3 movementDirection = currentPosition - lastPosition;

        OnMovement(movementDirection);

        lastPosition = currentPosition;
    }
}
