using UnityEngine;
using System.Collections;

public class TimerController : MonoBehaviour {

	public float currentTime;
	public int currentMinute;
	public int currentSecond;
	public int currentMillisecond;

	// Use this for initialization
	void Start () {
		currentTime = 0.0f;
		currentMinute = 0;
		currentSecond = 0;
		currentMillisecond = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 0f) {
			return;
		}
		currentTime += Time.deltaTime;
		float remaining = currentTime;
		currentMinute = (int) Mathf.Floor (remaining / 60.0f);
		remaining = remaining - currentMinute * 60.0f;
		currentSecond = (int) Mathf.Floor (remaining);
		remaining = remaining - currentSecond;
		currentMillisecond = (int) Mathf.Floor (remaining * 100.0f);
	}

	void OnGUI () {
		if (Time.timeScale == 0f) {
			return;
		}
		GUIStyle fontStyle = new GUIStyle ();
		fontStyle.fontSize = 18;
		fontStyle.fontStyle = FontStyle.Bold;
		fontStyle.alignment = TextAnchor.MiddleCenter;
		fontStyle.normal.textColor = Color.white;
		GUI.Label (new Rect (Screen.width / 2.0f - 50.0f, 20.0f, 100.0f, 50.0f), 
		           currentMinute.ToString ("00") + ":" + currentSecond.ToString ("00") + "." + 
		           currentMillisecond.ToString ("00"), fontStyle);
	}
}
