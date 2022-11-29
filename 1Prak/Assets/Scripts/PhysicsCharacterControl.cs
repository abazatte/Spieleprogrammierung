using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCharacterControl : MonoBehaviour
{

    private Animator animator;
    public GameObject camera;
    public float speed;
    public Vector3 respawnPosition;
    private Rigidbody body;
    private bool grounded;
    private float collTime;

    void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
    }

    private void OnCollisionExit(Collision collision)
    {
        animator.SetBool("Grounded", false);
        grounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent("TipToePlatform") != null)
        {
            TipToePlatform platform = (TipToePlatform)collision.gameObject.GetComponent("TipToePlatform");
            platform.CharacterTouches();
        }
        if (collision.gameObject.GetComponent<Rigidbody>() != null)
        {
            //body.constraints = RigidbodyConstraints.None;
            collTime = Time.time;
            body.AddForce(collision.gameObject.transform.right * -1 * 30, ForceMode.Impulse);
        }
        animator.SetBool("Grounded", true);
        grounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookVec = camera.transform.forward;
        Vector3 left = Vector3.Cross(lookVec, Vector3.up).normalized;
        Vector3 forward = new Vector3(camera.transform.forward.x, 0, camera.transform.forward.z);

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 move;
        animator.SetFloat("Speed", 0);
        if (Time.time - collTime > 1)
        {
            //body.constraints = RigidbodyConstraints.None;
            if (Input.GetButtonDown("Jump") && grounded)
            {
                body.AddForce(Vector3.up * 7, ForceMode.Impulse);
                animator.SetBool("Grounded", false);
                grounded = false;
            }
            if (x != 0 || z != 0)
            {
                animator.SetFloat("Speed", Mathf.Abs(x) + Mathf.Abs(z));
                move = ((-left * x) + (forward * z)) * speed;
                //body.AddForce(move, ForceMode.VelocityChange);
                body.MovePosition(body.position + move * Time.deltaTime);
                //Debug.Log(body.velocity);
                //transform.Translate(move * Time.deltaTime, Space.World);
                transform.LookAt(move * 100000);
                //lastMove = move;
            }
        }
        else
        {
            //body.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
        }
        if (transform.position.y < -5)
        {
            resetPosition();
        }

        //body.AddForce(new Vector3(0, 1, 0), ForceMode.Impulse);
    }

    private void resetPosition()
    {
        transform.position = respawnPosition;
    }
}