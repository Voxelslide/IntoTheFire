using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialCamera : MonoBehaviour
{

  private Vector2 lookInput;
  [SerializeField]
  private Transform orientation;
  [SerializeField]
  private Transform player; // The object that the camera will follow
  [SerializeField]
  private Transform playerMesh;
  [SerializeField]
  private Rigidbody rb;

  public float rotationSpeed;

  public void LookInput(InputAction.CallbackContext ctx)
  {
    lookInput = ctx.ReadValue<Vector2>();
  }


  // Update is called once per frame
  void Update()
    {

		//rotate orientation
		/*Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
		orientation.forward = viewDir.normalized;

		//rotate playerMesh
		Vector3 inputDir = orientation.forward * lookInput.y + orientation.right * lookInput.x;

		if (inputDir != Vector3.zero)
		{
      playerMesh.forward = inputDir.normalized;//Vector3.Lerp(playerMesh.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
		}*/
    transform.LookAt(player);

	}
}
