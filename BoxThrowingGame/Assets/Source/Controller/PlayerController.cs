using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    public Animator animator = new Animator();
    public float speed; // multiply with vec2 XZ-directions
    public float jumpValue; // multiply with Y-axis
    public float kickpower; // how fast player kicks
    public float MaxKickPower;
    public float StackedKickPower;
    public Vector3 movement;
    public bool IsJumped;
    public bool IsKicked;
    public Box kickedBox;
    public bool Moving;
    public bool IsFalling;
    public Vector3 InitialPosition;
    Vector3 direction;
    public static PlayerController Manager;
    [SerializeField] Joystick joystick;
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
        MaxKickPower = 2000.0f;
        kickedBox = null;
        InitialPosition = gameObject.transform.position;
    }

    public void ControllerUpdate()
    {
#if UNITY_EDITOR
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), movement.y, Input.GetAxis("Vertical"));
        movement = new Vector3(joystick.Horizontal, movement.y, joystick.Vertical);
        if (dir != Vector3.zero)
        {
            movement = dir;
        }
#else
        movement = new Vector3(joystick.Horizontal, 0.0f, joystick.Vertical);
#endif
        if (movement != Vector3.zero)
        {
            Moving = true;
            direction = new Vector3 (movement.x, 0.0f, movement.z);
            animator.SetBool("IsMoving", true);
        }
        else
        {
            Moving = false;
            animator.SetBool("IsMoving", false);
        }

        if (!IsJumped)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                IsJumped = true;
                animator.Play("Jump", 0, 0.0f);
                animator.SetBool("IsJumped", true);
                
            }
        }
    }

    public void KickAnimationOn()
    {
        IsKicked = true;
        animator.SetBool("IsKicked", true);
    }

    public void JumpAnimationOn()
    {
        IsJumped = true;
        animator.SetBool("IsJumped", true);
    }

    public void KickAnimationOff()
    {
        IsKicked = false;
        animator.SetBool("IsKicked", false);
        animator.Play("Idle", 0, 0);
        StackedKickPower = kickpower;
    }

    public void OnKicked()
    {
        if (kickedBox)
        {
            kickedBox.KickBox(transform.position, StackedKickPower);
            kickedBox = null;
        }
        IsKicked = false;
    }

    public void Landed()
    {
        IsJumped = false;
        IsFalling = false;
        movement.y = 0.0f;
        animator.SetBool("IsJumped", false);
    }

    private void FixedUpdate()
    {
        transform.SetPositionAndRotation(transform.position + movement * speed * Time.deltaTime, Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.15F));
        if (IsJumped && !IsFalling)
        {
            movement.y = Mathf.MoveTowards(movement.y, jumpValue/10.0f, 0.1f);
        }
        else if (IsFalling)
        {
            movement.y = Mathf.MoveTowards(movement.y, 0.0f, 0.1f);
        }
        //rb.MovePosition(transform.position + movement * speed * Time.deltaTime);
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
        StackedKickPower = kickpower;
    }

    public void Jump()
    {
        // it will change player's position in the
        // y direction with scale of jump_coefficient
        // ?
        IsJumped = true;
    }

    public void Fall()
    {
        // ? 
        IsFalling = true;
        //animator.Play("Jump", 0, 0.45f);
        //animator.SetBool("IsJumped", true);
    }

    public void ResetPhysic()
    {
        IsKicked = false;
        Moving = false;
        animator.SetBool("IsMoving", false);
        animator.SetBool("IsJumped", false);
        animator.SetBool("IsKicked", false);
        movement = new Vector3 (0.0f, movement.y, 0.0f);
    }

    public void Reposition()
    {
        transform.SetPositionAndRotation(InitialPosition, Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.15F));
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Box"))
        {
            kickedBox = other.GetComponent<Box>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Box"))
        {
            if (kickedBox == other.GetComponent<Box>())
            {
                kickedBox = null;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("BoundingPlane"))
        {
            gameObject.transform.position = InitialPosition;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        ResetPhysic();
    }
}
