using UnityEngine;
using System.Collections;

public class FogController : MonoBehaviour {

	public float maxScale = 80.0f;
	public float minScale = 30.0f;

	public GameObject player;
	private Controller controller;

	// Use this for initialization
	void Start () {
		controller = player.GetComponent<Controller>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 0f) {
			return;
		}
		float fogRatio = controller.energy / Controller.MAX_ENERGY;
		float scale = (maxScale - minScale) * fogRatio + minScale;
		transform.localScale = new Vector3(scale, scale, 0.0f);
	}
}
