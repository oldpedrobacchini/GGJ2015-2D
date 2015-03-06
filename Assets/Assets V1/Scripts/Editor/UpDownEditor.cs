using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(UpDown))]
public class UpDownEditor : Editor 
{
	public override void OnInspectorGUI()
	{
		UpDown myTarget = (UpDown)target;
		myTarget.isRandom = EditorGUILayout.Toggle ("Random behavior", myTarget.isRandom);

		if (!myTarget.isRandom) 
		{
			myTarget.range = EditorGUILayout.FloatField("Range",myTarget.range);
			myTarget.speed = EditorGUILayout.FloatField("Speed",myTarget.speed);
		}
	}
}
