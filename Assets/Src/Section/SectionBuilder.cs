using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class SectionBuilder : MonoBehaviour
{

	public PlayerPoint PlayerPointObject
	{
		get { return GetComponentInChildren<PlayerPoint>(); }
	}
	public string prefabPath;

	void Start()
	{
		DestroyObject(gameObject);
	}

	void Reset()
	{
		BoxCollider box = GetComponent<BoxCollider>();
		if (GetComponent<Collider>() == null || !(GetComponent<Collider>() is BoxCollider))
		{
			if (GetComponent<Collider>() != null) DestroyImmediate(GetComponent<Collider>());
			box = gameObject.AddComponent<BoxCollider>();
		}
		box.center = Section.ColliderCenter;
		box.size = Section.Size;
		box.isTrigger = true;
		for (int i = 0; i < transform.childCount; i++)
		{
			DestroyImmediate(transform.GetChild(i).gameObject);
		}
	}

	#if UNITY_EDITOR
	void OnDrawGizmos()
	{
		BoxCollider box = GetComponent<BoxCollider>();
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(box.center + transform.position, box.size);
	}

	public void BuildPrefab()
	{
		GameObject obj = new GameObject();
		try
		{
			obj.name = "Section";
			Section section = obj.AddComponent<Section>();
			for (int i = 0; i < this.transform.childCount; i++)
			{
				Transform t = this.transform.GetChild(i);
				Transform transform = (Transform)Instantiate(t);
				transform.name = "Section_" + transform.name;
				transform.gameObject.AddComponent<SectionObject>();
				transform.parent = obj.transform;
			}
			string path = Application.dataPath + "/" + prefabPath;
			string directoryPath = path.Substring(0, path.LastIndexOf("/")) + "/";
			if (!Directory.Exists(directoryPath))
			{
				Directory.CreateDirectory(directoryPath);
			}
			path = path.Substring(path.IndexOf("Assets/"));
			if (!path.EndsWith(".prefab"))
			{
				path += ".prefab";
			}
			section.prefabPath = path.Substring("Assets/".Length);
			PrefabUtility.CreatePrefab(path, obj);
		}
		catch (Exception e)
		{
			Debug.LogException(e);
		}
		DestroyImmediate(obj);
	}

	public void LoadSection(GameObject obj)
	{
		Clear();
		obj = (GameObject)Instantiate(obj);
		Section section = obj.GetComponent<Section>();
		int childCount = obj.transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			Transform transform = obj.transform.GetChild(0);
			DestroyImmediate(transform.GetComponent<SectionObject>());
			transform.name = transform.name.Replace("Section_", "");
			AddSectionObject(transform);
		}
		prefabPath = section.prefabPath;
		DestroyImmediate(obj);
	}

	public void Clear()
	{
		int childCount = this.transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			Transform transform = this.transform.GetChild(0);
			if (transform != null)
			{
				DestroyImmediate(transform.gameObject);
			}
		}
	}
	#endif

	void UpdatePlayerPoint(Transform obj)
	{
		obj.name = "PlayerPoint";
		UpdateObject(obj);
	}

	void UpdateObject(Transform obj)
	{
		if (obj != null)
		{
			string name = obj.name;
			if (name.StartsWith("Section_"))
			{
				name = name.Replace("Section_","");
			}
			name = name.Replace("(Clone)", "");
			obj.name = name;
			obj.parent = gameObject.transform;
		}
	}

	public void AddSectionObject(Transform obj)
	{
		obj.parent = transform;
		UpdateObject(obj);
	}

	public void RemoveSectionObject(Transform obj)
	{
		obj.parent = null;
	}

	public bool IsValid()
	{
		return PlayerPointObject != null;
	}

}
