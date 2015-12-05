using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MazeRotator : MonoBehaviour {
	
	public int DEFAULT_COUNTER = 300;
	public int DEFAULT_ROTATE_COUNTER = 45;
	public int BLINK_RESET_COUNTER = 10;
	public int UNBLINK_RESET_COUNTER = 15;
	
	public bool isRotate;
	public bool isDisplayWarning;
	public bool isBlinking;
	public int blinkCounter;
	public int counter;
	public int rotateCounter;
	public float innerRadius;
	public float outerRadius;
	public int rotateDirection;

	public GameObject character;
	public GameObject zombieSpawner;
	public GameObject batterySpawner;
	public AudioClip warningClip;

	public Texture2D warningTexture;

	private AudioSource audio;

	// Use this for initialization
	void Start () {
		isRotate = false;
		isDisplayWarning = false;
		isBlinking = false;
		blinkCounter = 0;
		counter = Random.Range (1, 5) * DEFAULT_COUNTER;
		rotateCounter = 0;
		audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 0f) {
			return;
		}
		List<GameObject> zombies = zombieSpawner.GetComponent<ZombieSpawner>().zombies;
		List<GameObject> batteries = batterySpawner.GetComponent<BatterySpawner>().batteries;

		if (counter == 0 && !isRotate) {
			isRotate = true;
			rotateCounter = DEFAULT_ROTATE_COUNTER;
			rotateDirection = Random.Range (0, 2) * 2 - 1;
		}
		
		if (rotateCounter == 0 && isRotate) {
			isRotate = false;
			counter = Random.Range (1, 5) * DEFAULT_COUNTER;
		}
		
		if (isRotate) {
			float distFromCenter = Vector2.Distance (new Vector2 (0.0f, 0.0f), character.transform.position);
			if (distFromCenter >= innerRadius && distFromCenter < outerRadius) {
				isDisplayWarning = true;
				if (!audio.isPlaying) {
					audio.Play ();
				}
				character.GetComponent<Controller>().isMoveAllowed = false;
				character.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				character.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
				character.transform.RotateAround (Vector3.zero, Vector3.forward, rotateDirection * 1.0f);
			}

			for (int i = 0; i < zombies.Count; i++) {
				if (zombies[i] == null) {
					break;
				}
				distFromCenter = Vector2.Distance (new Vector2 (0.0f, 0.0f), zombies[i].transform.position);
				if (distFromCenter >= innerRadius && distFromCenter < outerRadius) {
					zombies[i].GetComponent<ZombieAI>().isMoveAllowed = false;
					zombies[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
					zombies[i].transform.RotateAround (Vector3.zero, Vector3.forward, rotateDirection * 1.0f);
				}
			}

			for (int i = 0; i < batteries.Count; i++) {
				if (batteries[i] == null) {
					break;
				}
				distFromCenter = Vector2.Distance (new Vector2 (0.0f, 0.0f), batteries[i].transform.position);
				if (distFromCenter >= innerRadius && distFromCenter < outerRadius) {
					batteries[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
					batteries[i].transform.RotateAround (Vector3.zero, Vector3.forward, rotateDirection * 1.0f);
					batteries[i].GetComponent<BatteryController>().energyLabel.transform.RotateAround (Vector3.zero, 
					                                                                                   Vector3.forward, 
					                                                                                   rotateDirection * 1.0f);
				}
			}

			transform.Rotate (new Vector3 (0.0f, 0.0f, rotateDirection * 1.0f));
			rotateCounter--;
		} else {
			float distFromCenter = Vector2.Distance (new Vector2 (0.0f, 0.0f), character.transform.position);
			if (distFromCenter >= innerRadius && distFromCenter < outerRadius) {
				isDisplayWarning = false;
				character.GetComponent<Controller>().isMoveAllowed = true;
				character.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
			}
			for (int i = 0; i < zombies.Count; i++) {
				if (zombies[i] == null) {
					break;
				}
				distFromCenter = Vector2.Distance (new Vector2 (0.0f, 0.0f), zombies[i].transform.position);
				if (distFromCenter >= innerRadius && distFromCenter < outerRadius) {
					zombies[i].GetComponent<ZombieAI>().isMoveAllowed = true;
				}
			}
			counter--;
		}
	}

	void FixedUpdate() {
		if (isBlinking && blinkCounter == BLINK_RESET_COUNTER) {
			isBlinking = false;
			blinkCounter = 0;
		}
		if (!isBlinking && blinkCounter == UNBLINK_RESET_COUNTER) {
			isBlinking = true;
			blinkCounter = 0;
		}

		blinkCounter++;
	}

	void OnGUI () {
		if (Time.timeScale == 0f) {
			return;
		}
		if (isDisplayWarning && !isBlinking) {
			GUI.depth = -10;
			GUI.DrawTexture (new Rect (Screen.width / 2.0f - 100.0f, Screen.height / 2.0f - 50.0f, 200.0f, 100.0f), 
			                 warningTexture, ScaleMode.ScaleToFit);
		}
	}
}
