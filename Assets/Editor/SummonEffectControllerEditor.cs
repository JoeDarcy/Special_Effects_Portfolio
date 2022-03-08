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

		// Script reference
		SummonEffect summonScript = (SummonEffect)target;

		// Play effecy button
		if (GUILayout.Button("Play Effect"))
		{
			summonScript.PlayEffect();
		}

		// Reset to default values button
		if (GUILayout.Button("Reset effect values to default"))
		{
			summonScript.ResetEffectValuesToDefault();
		}
	}
}
