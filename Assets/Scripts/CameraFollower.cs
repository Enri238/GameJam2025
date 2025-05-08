using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
	public Transform target;

	void Update()
	{
		if (target != null)
			transform.position = new Vector3(target.position.x, 
											 target.position.y, 
											 target.position.z - 1);
	}

}