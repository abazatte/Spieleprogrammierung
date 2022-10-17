using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCharacterControl : MonoBehaviour
{
    public Rigidbody rb;

    public float speed = 5f;

    public float fallSpeed = 5f;

    private Animator animator;

    private Vector3 startPos;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        startPos = transform.position;
    }

    void FixedUpdate()
    {
        Vector3 input = Camera.main.transform.forward * Input.GetAxis("Vertical") + Camera.main.transform.right * Input.GetAxis("Horizontal");
        input.y = 0;
        input.Normalize();
        rb.MovePosition(transform.position + input * Time.deltaTime * speed);
        if (input.sqrMagnitude > 10e-10)
        {
            rb.MoveRotation(Quaternion.LookRotation(input));
        }

        animator.SetFloat("Speed", rb.velocity.magnitude / speed);

        //hier diese Stelle überarbeiten, damit es richtig kollidiert.
        //
        RaycastHit hit;
        if (!rb.SweepTest(Vector3.down, out hit))
        {

            animator.SetBool("Grounded", false);
            rb.MovePosition(transform.position + Vector3.down * Time.deltaTime * fallSpeed);
        }
        else
        {
            TipToePlatform tipToePlatform = hit.collider.GetComponent<TipToePlatform>();
            if (tipToePlatform != null)
            {
                tipToePlatform.CharacterTouches();
            }
            animator.SetBool("Grounded", true);
        }

        if (transform.position.y < -10)
        {
            ResetPosition();
        }
    }

    private void ResetPosition()
    {
        transform.position = startPos;
    }
}
