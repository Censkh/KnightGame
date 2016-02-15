using UnityEngine;
using System.Collections.Generic;
using System;

class World : MonoBehaviour
{

	public SectionManager sectionManager;

	void Reset()
	{
		if (sectionManager == null)
		{
			sectionManager = new SectionManager(this);
		}
	}

	void Start()
	{
		Generate();
	}

	void Generate()
	{
		for (int x = 0; x < 4; x++)
		{
			Type encounterType = RandomEncounterType();
			int index = UnityEngine.Random.Range(0, sectionManager.SectionPrefabs.Count);
			Transform sectionPrefab = sectionManager.SectionPrefabs[index];
			if (sectionPrefab != null)
			{
				GameObject sectionObject = ((Transform)Instantiate(sectionPrefab, new Vector3(Section.Size.x * -x, 0, 0), Quaternion.identity)).gameObject;
				sectionObject.transform.parent = transform;
				Section section = sectionObject.GetComponent<Section>();
				Encounter encounter = (Encounter)sectionObject.AddComponent(encounterType);
				section.encounter = encounter;
			}
			else Debug.LogError("Selection prefab is null at index " + index + ".");
		}
	}

	Type RandomEncounterType()
	{
		return Encounter.Types[UnityEngine.Random.Range(0, Encounter.Types.Length)];
	}

}
