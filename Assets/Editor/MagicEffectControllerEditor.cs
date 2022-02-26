using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MagicEffectController))]
public class MagicEffectControllerEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		MagicEffectController magicControllerScript = (MagicEffectController)target;

		if (GUILayout.Button("Play Summon Effect"))
		{
			magicControllerScript.PlaySummonEffect();		// Summon
		}

		if (GUILayout.Button("Play Attack Effect"))
		{
			magicControllerScript.PlayAttackEffect();       // Attack
		}

		if (GUILayout.Button("Play Heal Effect"))
		{
			magicControllerScript.PlayHealEffect();         // Heal
		}

		if (GUILayout.Button("Play Dark Effect"))
		{
			magicControllerScript.PlayDarkEffect();         // Dark
		}
	}
}
