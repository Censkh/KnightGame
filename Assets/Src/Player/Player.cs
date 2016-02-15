using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

	public Section section;

	void Start()
	{
	}


	void Update()
	{
		Rigidbody rigidbody = GetComponent<Rigidbody>();
		if (ShouldMove())
			transform.position+=new Vector3(-9f*Time.deltaTime,0f,0f);
		if (section != null)
		{
			if (section.encounter != null)
			{
				if (!section.encounter.encounterActive && section.encounter.enabled)
				{
					float x = transform.position.x - section.transform.position.x;
					if (x < section.PlayerPoint.transform.localPosition.x)
					{
						section.encounter.Begin();
					}
				}
			}
		}
	}

	public static bool IsPlayer(GameObject gameObject)
	{
		return gameObject.GetComponent<Player>() != null;
	}

	public bool ShouldMove()
	{
		if (section == null || section.encounter == null)
		{
			return true;
		}
		if (!section.encounter.encounterActive) return true;
		float x = transform.position.x - section.transform.position.x;
		if (x > section.PlayerPoint.transform.localPosition.x)
		{
			return true;
		}
		return false;
	}

}
