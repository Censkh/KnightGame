using System;
using UnityEngine;

public abstract class Encounter : MonoBehaviour
{

	public static Type[] Types = new Type[] { typeof(TestEncounter) };

	public bool encounterActive = false;

	void Update()
	{
		if (encounterActive)
		{
			UpdateEncounter();
			if (ShouldEnd())
			{
				End();
			}
		}
	}

	public void Begin()
	{
		enabled = true;
		encounterActive = true;
		BeginEncounter();
	}

	public void End()
	{
		encounterActive = false;
		enabled = false;
		EndEncounter();
	}

	protected abstract void EndEncounter();

	protected abstract void BeginEncounter();

	protected abstract void UpdateEncounter();

	protected abstract bool ShouldEnd();

}
