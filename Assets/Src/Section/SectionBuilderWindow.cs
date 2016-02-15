#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

class SectionBuilderWindow : EditorWindow
{

	public SectionBuilder SectionBuilder
	{
		get
		{
			GameObject obj = GameObject.FindGameObjectWithTag("SectionBuilder");
			if (obj == null)
			{
				return null;
			}
			return obj.GetComponent<SectionBuilder>();
		}
	}

	[MenuItem("Window/Section Builder")]
	static void ShowWindow()
	{
	SectionBuilderWindow window = EditorWindow.GetWindow<SectionBuilderWindow>();
		window.title = "Section Builder";
		window.autoRepaintOnSceneChange = true;
	}

	SectionBuilderInspector.SectionBuilderData data = new SectionBuilderInspector.SectionBuilderData();

	void OnGUI()
	{
		if (SectionBuilder != null)
			SectionBuilderInspector.Show(SectionBuilder, data);
	}

}

#endif