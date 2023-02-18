using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InClassCamera : MonoBehaviour
{
  public Transform target;
  public float damping = 2f;
	public float rotationDamping = 1f;


	private void Update()
	{
		//update camera to move towards point in space
		transform.position = Vector3.Lerp(transform.position, target.position, damping * Time.deltaTime);
		//transform.rotation = target.transform.rotation;
		//transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, rotationDamping * Time.deltaTime);
		transform.LookAt(target);



	}


}
