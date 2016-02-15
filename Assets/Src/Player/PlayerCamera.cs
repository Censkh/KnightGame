using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour
{
	
	public GameObject playerObject;

	void Start()
	{

	}

	void Update()
	{
		transform.position = Vector3.Slerp(transform.position,playerObject.transform.position + new Vector3(-3,2,5),9f*Time.deltaTime);
	}
}
