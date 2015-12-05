using UnityEngine;
using System.Collections;

public class HSController : MonoBehaviour {

	public bool isViewing;
	public int minute;
	public int second;
	public int milliSecond;

	// Use this for initialization
	void Start () {
		isViewing = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isViewing) {
			if (minute != 0 || second != 0 || milliSecond != 0) {
				float score = minute * 6000.0f + second * 100.0f + milliSecond;
				print ("score: " + score);
				int i;
				for (i = 0; i < 5; i++) {
					int curMinute = PlayerPrefs.GetInt ("minute" + i);
					int curSecond = PlayerPrefs.GetInt ("second" + i);
					int curMilliSecond = PlayerPrefs.GetInt ("milliSecond" + i);
					if (curMinute == 0 && curSecond == 0 && curMilliSecond == 0) {
						break;
					}
					float currentScore = curMinute * 6000.0f + curSecond * 100.0f + curMilliSecond;
					if (currentScore > score) {
						break;
					}
				}
				if (i < 5) {
					for (int j = 4; j >= i + 1; j--) {
						int prefMinute = PlayerPrefs.GetInt ("minute" + (j - 1));
						int prefSecond = PlayerPrefs.GetInt ("second" + (j - 1));
						int prefMilliSecond = PlayerPrefs.GetInt ("milliSecond" + (j - 1));
						if (prefMinute == 0 && prefSecond == 0 && prefMilliSecond == 0) {
							continue;
						}
						PlayerPrefs.SetInt ("minute" + j, prefMinute);
						PlayerPrefs.SetInt ("second" + j, prefSecond);
						PlayerPrefs.SetInt ("milliSecond" + j, prefMilliSecond);
					}
					PlayerPrefs.SetInt ("minute" + i, minute);
					PlayerPrefs.SetInt ("second" + i, second);
					PlayerPrefs.SetInt ("milliSecond" + i, milliSecond);
					print ("i = " + i);
				}
				minute = 0;
				second = 0;
				milliSecond = 0;
			}
			if (Input.GetKeyDown (KeyCode.R)) {
				Application.LoadLevel (0);
			}
		}
	}

	void OnGUI () {
		if (isViewing) {
			float centerX = (float)Screen.width / 2.0f;
			float centerY = (float)Screen.height / 2.0f;
			GUILayout.BeginArea (new Rect (centerX - 100, centerY - 200, 200, 400));
			GUILayout.FlexibleSpace();
			GUI.skin.label.alignment = TextAnchor.MiddleCenter;
			GUILayout.Label ("Highscores:", GUILayout.Width (200));
			for (int i = 0; i < 5; i++) {
				int curMinute = PlayerPrefs.GetInt ("minute" + i);
				int curSecond = PlayerPrefs.GetInt ("second" + i);
				int curMilliSecond = PlayerPrefs.GetInt ("milliSecond" + i);
				if (curMinute == 0 && curSecond == 0 && curMilliSecond == 0) {
					break;
				}
				GUILayout.Label (i + 1 + ". " + curMinute + " minutes " + curSecond + "." + curMilliSecond + " second", 
				                 GUILayout.Width (200));
			}
			if (GUILayout.Button ("\nClick here or press 'R' to return\n")) {
				Application.LoadLevel (0);
			}
			GUILayout.FlexibleSpace();
			GUILayout.EndArea ();
		}
	}
}
