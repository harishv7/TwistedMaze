using UnityEngine;
using System.Collections;

public class EndController : MonoBehaviour {

	public bool ended;
	public int minute;
	public int second;
	public int milliSecond;

	// Use this for initialization
	void Start () {
		ended = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (ended) {
			Time.timeScale = 0f;
			if (Input.GetKeyDown (KeyCode.S)) {
				ended = false;
				HSController hsController = GameObject.Find ("Highscores").GetComponent<HSController>();
				hsController.minute = minute;
				hsController.second = second;
				hsController.milliSecond = milliSecond;
				hsController.isViewing = true;
				return;
			}
			if (Input.GetKeyDown (KeyCode.R)) {
				Application.LoadLevel (0);
			}
		}
	}

	void OnGUI () {
		if (ended) {
			float centerX = (float)Screen.width / 2.0f;
			float centerY = (float)Screen.height / 2.0f;
			GUILayout.BeginArea (new Rect (centerX - 150, centerY - 200, 300, 400));
			GUILayout.FlexibleSpace();
			GUI.skin.label.alignment = TextAnchor.MiddleCenter;
			GUIStyle fontStyle = new GUIStyle ();
			fontStyle.fontSize = 16;
			fontStyle.fontStyle = FontStyle.Bold;
			fontStyle.alignment = TextAnchor.MiddleCenter;
			fontStyle.normal.textColor = Color.white;
			GUILayout.Label ("Congratulations,\n" +
			                 "You finished the twisted maze within:\n" + minute + " minutes and " 
			                 + second + "." + milliSecond + " seconds!", fontStyle);
			if (GUILayout.Button ("\nClick here or press 'S' to view highscores\n")) {
				ended = false;
				HSController hsController = GameObject.Find ("Highscores").GetComponent<HSController>();
				hsController.minute = minute;
				hsController.second = second;
				hsController.milliSecond = milliSecond;
				hsController.isViewing = true;
				return;
			}
			if (GUILayout.Button ("\nClick here or press 'R' to return\n")) {
				Application.LoadLevel (0);
			}
			GUILayout.FlexibleSpace();
			GUILayout.EndArea ();
		}
	}
}
