#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(World))]
class WorldInspector : Editor
{

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
	}

}

#endif