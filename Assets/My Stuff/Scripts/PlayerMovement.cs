using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

  [Header("Movement")]
  [SerializeField]
  private float moveSpeed;
  public Transform orientation;
  private Vector2 moveInput;
  private Rigidbody rb;
  private Vector3 moveDirection;

  [Header("Ground Checks")]
  [SerializeField]
  private float playerHeight;
  [SerializeField]
  private float groundDrag;
  private bool grounded;

  [Header("Jumping")]
  //[SerializeField]
  //private float gravity = -9.8f;
  private bool jumpInput;
  [SerializeField]
  private float jumpForce;
  [SerializeField]
  private float airMultiplier;
  bool canJump;


  private void Awake()
	{
    rb = GetComponent<Rigidbody>();
    canJump = false;
	}

	
	void FixedUpdate()
  {

    //ApplyGravity();

    GroundedCheck();

    Move();

    SpeedLimiter();

    ApplyDrag();
  }

  public void MoveInput(InputAction.CallbackContext ctx)
  {
    moveInput = ctx.ReadValue<Vector2>();
  }
  public void JumpInput(InputAction.CallbackContext ctx)
  {
    if (ctx.started) Jump();
  }

	private void Move()
	{
    moveDirection = orientation.forward * moveInput.y + orientation.right * moveInput.x;

    if (grounded)
    {
      rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    else if (!grounded)
		{
      rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }
	}

  private void Jump()
	{ 
    if (canJump)
    { 
      rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
      rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
      canJump = false;
    }
  }

  private void GroundedCheck()
	{
    grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.1f);
    if (grounded) canJump = true;
	}

  private void ApplyDrag()
	{
    if (grounded)
      rb.drag = groundDrag;
    else
      rb.drag = 0;
	}

  private void SpeedLimiter()
	{
    Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

    //limit velocity if needed
    if(flatVel.magnitude > moveSpeed)
		{
      Vector3 limitedVel = flatVel.normalized * moveSpeed;
      rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
		}
	}

  private void ApplyGravity()
	{
    //rb.AddForce(Vector3.up * gravity, ForceMode.Acceleration);
	}

  /*private void ResetJump()
	{
    canJump = true;
	}*/


}
