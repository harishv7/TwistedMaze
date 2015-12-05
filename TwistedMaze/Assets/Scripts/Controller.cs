using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	public const float MAX_ENERGY = 100.0f;

	public float moveSpeed = 3.0f;
	public bool isMoveAllowed;
	public float energy;
	public float energyDecayRate = 0.05f;
	
	// Use this for initialization
	void Start () {
		energy = 100.0f;
		isMoveAllowed = true;
	}
	
	// Update is called once per frame
	void Update () {
		float distFromCenter = Vector2.Distance (Vector2.zero, gameObject.transform.position);
		if (distFromCenter > 37.5) {
			gameObject.transform.position = Vector2.zero;
			TimerController timerController = GameObject.Find ("TimerGUI").GetComponent<TimerController>();
			EndController endController = GameObject.Find ("End").GetComponent<EndController>();
			endController.minute = timerController.currentMinute;
			endController.second = timerController.currentSecond;
			endController.milliSecond = timerController.currentMillisecond;
			endController.ended = true;
			return;
		}
		if (Time.timeScale == 0f) {
			return;
		}
		if (isMoveAllowed) {
			Rigidbody2D rigidBody = gameObject.GetComponent<Rigidbody2D>();
			rigidBody.MovePosition (rigidBody.position + new Vector2 (Input.GetAxis ("Horizontal") * moveSpeed, 
			                                                          Input.GetAxis ("Vertical") * moveSpeed) * Time.fixedDeltaTime);
			float angle = Mathf.Atan2 (Input.GetAxis ("Vertical"), Input.GetAxis ("Horizontal")) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis (angle - 90.0f, Vector3.forward);
		}

		energy = Mathf.Max (energy - energyDecayRate, 0.0f);
		print (energy);
	}
}
