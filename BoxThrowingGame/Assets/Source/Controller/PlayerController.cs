using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    public float speed; // multiply with vec2 XZ-directions
    public float jumpValue; // multiply with Y-axis
    public float kickpower; // how fast player kicks
    // following variables will be updated in the input 
    // controller according to user 
    public Vector3 movement;
    public bool jumped;
    public bool kicked;
    public Box kickedBox;
    public Vector3 InitialPosition;
    public static PlayerController Manager;
    public void Initialize()
    {
        if (Manager == null)
        {
            Manager = this;
            // DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        InitialPosition = gameObject.transform.position;
    }

    public void ControllerUpdate()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        if (!jumped)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                jump();
            }
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + movement * speed * Time.deltaTime);
    }

    public void UpdateJumpValue(float value)
    {
        jumpValue = value;
    }

    public void UpdatePlayerSpeed(float value)
    {
        speed = value;
    }

    public void UpdatePower(float value)
    {
        kickpower = value;
    }

    private void jump()
    {
        // it will change player's position in the
        // y direction with scale of jump_coefficient
        jumped = true;
        jumpValue = 150;
        rb.AddForce(Vector3.up * jumpValue);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            jumped = false;
        }
    }

    public void ResetPhysic()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.CompareTag("Box"))
        {
            // add force?
            hit.rigidbody.AddForce(hit.moveDirection * kickpower);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Box"))
        {
            kickedBox = other.GetComponent<Box>();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("BoundingPlane"))
        {
            // resposition player
            gameObject.transform.position = InitialPosition;
        }
        ResetPhysic();
    }

    private void OnCollisionExit(Collider other)
    {
        ResetPhysic();
    }
}
