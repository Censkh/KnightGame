using UnityEngine;

class TestEncounter : Encounter
{

	float ticks = 0;

	protected override void BeginEncounter()
	{
	}

	protected override void EndEncounter()
	{
	}

	protected override void UpdateEncounter()
	{
		ticks += Time.deltaTime;
	}

	protected override bool ShouldEnd()
	{
		return ticks > 2f;
	}

}
