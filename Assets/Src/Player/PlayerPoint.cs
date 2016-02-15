using UnityEngine;
using System.Collections;

public class PlayerPoint : MonoBehaviour
{

	void Reset()
	{
		float scale = 0.25f;
		transform.localScale = new Vector3(scale, scale, scale);
		if (GetComponent<Collider>() != null) GetComponent<Collider>().isTrigger = true;
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, transform.localScale.x / 2);
	}
}
