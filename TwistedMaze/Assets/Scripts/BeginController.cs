using UnityEngine;
using System.Collections;

public class BeginController : MonoBehaviour {

	public bool started;

	// Use this for initialization
	void Start () {
		started = false;
		Time.timeScale = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (!started && Input.GetKeyDown (KeyCode.S)) {
			Time.timeScale = 1f;
			started = true;
		}
	}

	void OnGUI () {
		if (!started) {
			Time.timeScale = 0f;
			float centerX = (float)Screen.width / 2.0f;
			float centerY = (float)Screen.height / 2.0f;
			GUILayout.BeginArea (new Rect (centerX - 200, centerY - 200, 400, 400));
			GUILayout.FlexibleSpace();
			GUI.skin.label.alignment = TextAnchor.MiddleCenter;
			GUIStyle fontStyle = new GUIStyle ();
			fontStyle.fontSize = 32;
			fontStyle.fontStyle = FontStyle.Bold;
			fontStyle.alignment = TextAnchor.MiddleCenter;
			fontStyle.normal.textColor = Color.white;
			GUILayout.Label ("Twisted Maze\n", fontStyle);
			GUIStyle descStyle = new GUIStyle ();
			descStyle.fontSize = 16;
			descStyle.fontStyle = FontStyle.Bold;
			descStyle.alignment = TextAnchor.MiddleCenter;
			descStyle.normal.textColor = Color.white;
			GUILayout.Label ("Move: Use Arrow Keys\n\n" + 
			                 "Pause: Press 'P'\n\n" + 
			                 "Energy indicates light intensity\n\n" + 
			                 "Hit Zombie: -10 Energy\n\n" + 
			                 "Pick Battery: +0 ~ +60 Energy\n"
			                 , descStyle);
			if (GUILayout.Button ("\nClick here or press 'S' to start\n")) {
				Time.timeScale = 1f;
				started = true;
			}
			GUILayout.FlexibleSpace();
			GUILayout.EndArea ();
		}
	}
}
