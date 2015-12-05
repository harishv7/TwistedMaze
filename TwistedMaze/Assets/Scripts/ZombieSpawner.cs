using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZombieSpawner : MonoBehaviour {

	public int numZombies = 35;
	public GameObject zombie;
	public List<GameObject> zombies;
	public float minSpawnRadius = 5.0f;
	public float maxSpawnRadius = 35.0f;
	public float mapRadius = 37.5f;
	public int numZombiesToRenew = 0;

	// Use this for initialization
	void Start () {
		GameObject last;
		for (int i = 0; i < numZombies; i++) {
			int signX = Random.Range (0, 2) * 2 - 1;
			int signY = Random.Range (0, 2) * 2 - 1;
			float randomX = signX * (Random.value * (maxSpawnRadius - minSpawnRadius) + minSpawnRadius);
			float randomY = signY * (Random.value * (maxSpawnRadius - minSpawnRadius) + minSpawnRadius);
			last = (GameObject) Instantiate (zombie, new Vector3 (randomX, randomY, 0.0f), zombie.transform.rotation);
			last.GetComponent<ZombieAI>().parentSpawner = gameObject;
			zombies.Add (last);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 0f) {
			return;
		}
		for (int i = zombies.Count - 1; i >= 0; i--) {
			float dist = Vector2.Distance (zombies[i].transform.position, Vector2.zero);
			if (dist > mapRadius) {
				Destroy (zombies[i]);
				zombies.RemoveAt (i);
				numZombiesToRenew++;
			}
		}

		renewZombies();
	}

	void renewZombies () {
		GameObject last;
		for (int i = 0; i < numZombiesToRenew; i++) {
			int signX = Random.Range (0, 2) * 2 - 1;
			int signY = Random.Range (0, 2) * 2 - 1;
			float randomX = signX * (Random.value * (maxSpawnRadius - minSpawnRadius) + minSpawnRadius);
			float randomY = signY * (Random.value * (maxSpawnRadius - minSpawnRadius) + minSpawnRadius);
			last = (GameObject) Instantiate (zombie, new Vector3 (randomX, randomY, 0.0f), zombie.transform.rotation);
			last.GetComponent<ZombieAI>().parentSpawner = gameObject;
			zombies.Add (last);
		}
		numZombiesToRenew = 0;
	}
}
