using UnityEngine;
using System.Collections;

public class BatteryController : MonoBehaviour {

	public const float MAX_ENERGY = 60.0f;
	public const float MIN_ENERGY = 30.0f;

	public GameObject energyLabel;
	public float energyCarried;
	public float energyDecayRate = 0.02f;

	public GameObject parentSpawner;

	// Use this for initialization
	void Start () {
		energyCarried = Random.value * (MAX_ENERGY - MIN_ENERGY) + MIN_ENERGY;
		energyLabel = (GameObject) Instantiate (energyLabel, new Vector3 (transform.position.x - 0.35f, 
		                                                                  transform.position.y  + 0.20f, -1.0f), 
		                                        transform.rotation);
		energyLabel.GetComponent<TextMesh>().text = energyCarried.ToString ("0") + "%";
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 0f) {
			return;
		}
		energyCarried = energyCarried - energyDecayRate;
		if (energyCarried <= 0.0f) {
			parentSpawner.GetComponent<BatterySpawner>().batteries.Remove (gameObject);
			parentSpawner.GetComponent<BatterySpawner>().numBatteriesToRenew++;
			Destroy (energyLabel);
			Destroy (gameObject);
		} else {
			energyLabel.GetComponent<TextMesh>().text = energyCarried.ToString ("0") + "%";
			energyLabel.GetComponent<Transform>().position = new Vector3 (transform.position.x - 0.35f, 
			                                                              transform.position.y + 0.20f, -1.0f);
			transform.rotation = Quaternion.identity;
			energyLabel.GetComponent<Transform>().rotation = Quaternion.identity;
		}
	}

	void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.name == "Character") {
			Controller controller = col.gameObject.GetComponent<Controller>();
			controller.energy = Mathf.Min (controller.energy + energyCarried, 100.0f);
			parentSpawner.GetComponent<BatterySpawner>().batteries.Remove (gameObject);
			parentSpawner.GetComponent<BatterySpawner>().numBatteriesToRenew++;
			Destroy (energyLabel);
			Destroy (gameObject);
		}
	}
}
