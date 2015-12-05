using UnityEngine;
using System.Collections;

public class PauseController : MonoBehaviour {
	
	public bool paused = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.P)) {
			paused = TogglePause();
		}
		if (Input.GetKeyDown (KeyCode.R)) {
			Application.LoadLevel (0);
		}
	}
	
	void OnGUI () {
		if (paused) {
			float centerX = (float)Screen.width / 2.0f;
			float centerY = (float)Screen.height / 2.0f;
			GUILayout.BeginArea (new Rect (centerX - 100, centerY - 100, 200, 200));
			GUILayout.FlexibleSpace();
			GUI.skin.label.alignment = TextAnchor.MiddleCenter;
			GUILayout.Label ("Game is paused", GUILayout.Width (200));
			if (GUILayout.Button ("\nClick here or press 'P' to unpause\n")) {
				paused = TogglePause ();
			}
			if (GUILayout.Button ("\nClick here or press 'R' to restart\n")) {
				Application.LoadLevel (0);
			}
			GUILayout.FlexibleSpace();
			GUILayout.EndArea ();
		}
	}
	
	bool TogglePause() {
		if(Time.timeScale == 0f)
		{
			Time.timeScale = 1f;
			return(false);
		}
		else
		{
			Time.timeScale = 0f;
			return(true);    
		}
	}
}
