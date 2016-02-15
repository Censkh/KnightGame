using UnityEngine;
using System.Collections.Generic;


public class Section : MonoBehaviour
{

	public static readonly Vector3 Size = new Vector3(35, 8, 12);
	public static readonly Vector3 ColliderCenter = new Vector3(0, 4, -2);

	public PlayerPoint PlayerPoint
	{
		get { return GetComponentInChildren<PlayerPoint>();}
	}
	public Encounter encounter;

	public string prefabPath;

	void Start()
	{
		if (GetComponent<Collider>() == null || !(GetComponent<Collider>() is BoxCollider))
		{
			if (GetComponent<Collider>() != null) DestroyImmediate(GetComponent<Collider>());
			gameObject.AddComponent<BoxCollider>();
		}
		BoxCollider box = (BoxCollider)GetComponent<Collider>();
		box.center = ColliderCenter;
		box.size = Size;
		box.isTrigger = true;
		Destroy(PlayerPoint.GetComponent<Renderer>());
	}

	void OnTriggerEnter(Collider other)
	{
		Player player = other.GetComponent<Player>();
		if (player != null)
		{
			player.section = this;
		}
	}

	void Update()
	{

	}
}
