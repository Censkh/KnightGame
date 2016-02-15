#if UNITY_EDITOR

using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(SectionBuilder))]
public class SectionBuilderInspector : Editor
{

	public class SectionBuilderData
	{
		public Transform selectionObject;
		public Transform prefab;
	}

	SectionBuilderData data = new SectionBuilderData();
	public override void OnInspectorGUI()
	{
		Show((SectionBuilder)target, data);
	}

	private static int selectedObject;
	private static List<Transform> childObjects = new List<Transform>();

	public static void Show(SectionBuilder builder, SectionBuilderData data)
	{
		UpdateSectionObjects(builder);
		GUILayout.Label("Section Builder",EditorStyles.boldLabel);
		GUILayout.BeginHorizontal();
		GUI.enabled = builder.PlayerPointObject!=null;
		EditorGUILayout.ObjectField("Player Point",builder.PlayerPointObject==null ? null : builder.PlayerPointObject.transform,typeof(Transform),true);
		if (GUILayout.Button("Select",EditorStyles.miniButton,GUILayout.Width(50)))
		{
			Selection.activeTransform = builder.PlayerPointObject.transform;
		}
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Save", EditorStyles.miniButton, new GUILayoutOption[] { GUILayout.Width(50) }))
		{
			builder.BuildPrefab();
		}
		GUI.enabled = builder.IsValid();
		builder.prefabPath = EditorGUILayout.TextField(builder.prefabPath);
		GUI.enabled = true;
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Load", EditorStyles.miniButton, new GUILayoutOption[] { GUILayout.Width(50) }))
		{
			builder.LoadSection(data.prefab.gameObject);
		}
		data.prefab = (Transform)EditorGUILayout.ObjectField(data.prefab, typeof(Transform), true, new GUILayoutOption[] { });
		GUILayout.EndHorizontal();
		GUILayout.Label("Section Object", EditorStyles.boldLabel);
		if (builder.transform.childCount != 0)
		{
			selectedObject = ShowObjectSelect(builder, selectedObject);
			if (selectedObject >= builder.transform.childCount)
			{
				selectedObject = 0;
			}
			Transform transform = builder.transform.GetChild(selectedObject);
			ShowSectionObject(builder, transform);
		}
	}

	static void ShowSectionObject(SectionBuilder builder, Transform obj)
	{
		GUILayout.BeginHorizontal();
		GUI.enabled = Selection.activeTransform != null && !Selection.activeTransform.Equals(obj);
		if (GUILayout.Button("Select Section Object"))
		{
			Selection.activeTransform = obj;
		}
		GUI.enabled = true;
		GUILayout.EndHorizontal();
	}

	static void UpdateSectionObjects(SectionBuilder builder)
	{
		for(int i = 0; i < builder.transform.childCount;i++) {
			Transform child = builder.transform.GetChild(i);
			if (childObjects.Contains(child))
			{

			}
			else
			{
				builder.AddSectionObject(child);
			}
		}
	}

	static int ShowObjectSelect(SectionBuilder builder, int select)
	{
		string[] names = new string[builder.transform.childCount];
		for (int i = 0; i < names.Length; i++)
		{
			names[i] = builder.transform.GetChild(i).name;
		}
		return EditorGUILayout.Popup("Section Object Select", select, names);
	}
	
}

#endif