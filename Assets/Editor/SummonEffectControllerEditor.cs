using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SummonEffect))]
public class SummonEffectControllerEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		SummonEffect summonScript = (SummonEffect)target;

		if (GUILayout.Button("Play Effect"))
		{
			summonScript.PlayEffect();
		}
	}
}
