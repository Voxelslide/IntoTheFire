using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OnCameraCameraController : MonoBehaviour
{
  private Vector2 lookInput;
  [SerializeField]
  private Transform orientation;
  [SerializeField]
  private Transform target; // The object that the camera will follow
  [SerializeField]
  private Transform playerMesh;
  [SerializeField]
  private Rigidbody rb;

  [SerializeField]
  private float rotationSpeed;


  public void LookInput(InputAction.CallbackContext ctx)
  {
    lookInput = ctx.ReadValue<Vector2>();
  }




  public float smoothSpeed = 0.125f; // The smoothing speed of the camera movement
  public Vector3 offset; // The offset of the camera from the target
  public float radius = 3f; // The distance of the camera from the target
  public float cameraSensitivity = 4f; // The sensitivity of the camera movement

  private float yaw = 0f;
  private float pitch = 0f;

  public bool lostFocus = true;

  private void LateUpdate()
  {
    //Gabe Camera Movement
    MoveCamera();
    
    //rotate orientation
    Vector3 viewDir = target.position - new Vector3(transform.position.x, target.position.y, transform.position.z);
    orientation.forward = viewDir.normalized;

    //rotate playerMesh
    Vector3 inputDir = orientation.forward * lookInput.y + orientation.right * lookInput.x;

    if(inputDir != Vector3.zero)
		{
      playerMesh.forward = Vector3.Slerp(playerMesh.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
		}

  }


  private void MoveCamera()
	{
    if (target != null)
    {
      Gamepad gamepad = Gamepad.current;
      if (gamepad != null)
      {
        //Vector2 stickL = gamepad.rightStick.ReadValue();

        yaw = lookInput.x;
        pitch = lookInput.y;
      }
      else
      {
        // Get mouse inputs for camera rotation
        //yaw -= Input.GetAxis("Mouse X") * cameraSensitivity;
        //pitch += Input.GetAxis("Mouse Y") * cameraSensitivity;
      }

      lostFocus = false;
      // Limit pitch rotation
      pitch = Mathf.Clamp(pitch, 10f, 80f);

      // Convert spherical coordinates to Cartesian coordinates
      float x = radius * Mathf.Sin(pitch * Mathf.Deg2Rad) * Mathf.Cos(yaw * Mathf.Deg2Rad);
      float y = radius * Mathf.Cos(pitch * Mathf.Deg2Rad);
      float z = radius * Mathf.Sin(pitch * Mathf.Deg2Rad) * Mathf.Sin(yaw * Mathf.Deg2Rad);

      // Calculate the desired position of the camera
      Vector3 desiredPosition = target.position + new Vector3(x, y, z);

      // Smoothly move the camera to the desired position
      transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

      // Make the camera look at the target
      transform.LookAt(target);
    }
    else
    {
      lostFocus = true;
    }
  }



}