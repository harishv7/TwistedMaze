using UnityEngine;
using System.Collections;

public class CameraFollower : MonoBehaviour {
	
	public Transform character;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 0f) {
			return;
		}
		transform.position = new Vector3(character.position.x, character.position.y, character.position.z - 5.0f);
	}
}
