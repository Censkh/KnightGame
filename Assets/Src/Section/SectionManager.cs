using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[Serializable]
class SectionManager
{

	[SerializeField]
	private List<Transform> sectionPrefabs = new List<Transform>();
	public List<Transform> SectionPrefabs
	{
		get
		{
			return sectionPrefabs;
		}
	}

	[HideInInspector]
	public Transform worldObject;
	private World world;
	public World World
	{
		get
		{
			if (world == null)
			{
				return world = worldObject.GetComponent<World>();
			}
			return world;
		}
	}

	public SectionManager(World world)
	{
		worldObject = world.transform;
	}

}