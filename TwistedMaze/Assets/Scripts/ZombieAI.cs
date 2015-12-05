using UnityEngine;
using System.Collections;

public class ZombieAI : MonoBehaviour {

	public float moveSpeed = 1.5f;
	public bool isPlayerDetected;
	public int direction;
	public int moveCounter;
	public Vector2 targetPosition;
	public float detectionRadius = 5.0f;
	public bool isMoveAllowed;

	public GameObject parentSpawner;

	// Use this for initialization
	void Start () {
		isPlayerDetected = false;
		moveCounter = 0;
		isMoveAllowed = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 0f) {
			return;
		}
		Rigidbody2D rigidBody = gameObject.GetComponent<Rigidbody2D>();
		Transform transform = gameObject.GetComponent<Transform>();

		isPlayerDetected = false;
		Collider2D [] collision = Physics2D.OverlapCircleAll (rigidBody.position, detectionRadius);
		for (int i = 0; i < collision.Length; i++) {
			if (collision[i].name.Equals("Character")) {
				Vector2 direction = new Vector2 (collision[i].transform.position.x - rigidBody.position.x, 
				                                 collision[i].transform.position.y - rigidBody.position.y);
				RaycastHit2D [] rayCollision = Physics2D.RaycastAll (rigidBody.position, direction);
				Debug.DrawLine (rigidBody.position, collision[i].transform.position);
				if (rayCollision.Length > 1 && rayCollision[1].collider.name.Equals("Character")) {
					isPlayerDetected = true;
					moveCounter = 0;
					targetPosition = rayCollision[1].transform.position;
				}
				break;
			}
		}

		if (isMoveAllowed) {
			if (!isPlayerDetected) {
				// randomized movement if no player is detected
				if (moveCounter == 0) {
					direction = Random.Range (0, 4);
					moveCounter = (int)(Random.value * 100);
				} else {
					if (direction == 0) {
						rigidBody.MovePosition (rigidBody.position + new Vector2 (moveSpeed, 0.0f) * Time.fixedDeltaTime);
						//float angle = Vector2.Angle (Vector2.zero, new Vector2 (moveSpeed, 0.0f));
						transform.rotation = Quaternion.AngleAxis (-90.0f, Vector3.forward);
					} else if (direction == 1) {
						rigidBody.MovePosition (rigidBody.position + new Vector2 (0.0f, -moveSpeed) * Time.fixedDeltaTime);
						//float angle = Vector2.Angle (Vector2.zero, new Vector2 (0.0f, -moveSpeed));
						transform.rotation = Quaternion.AngleAxis (180.0f, Vector3.forward);
					} else if (direction == 2) {
						rigidBody.MovePosition (rigidBody.position + new Vector2 (-moveSpeed, 0.0f) * Time.fixedDeltaTime);
						//float angle = Vector2.Angle (Vector2.zero, new Vector2 (-moveSpeed, 0.0f));
						transform.rotation = Quaternion.AngleAxis (90.0f, Vector3.forward);
					} else {
						rigidBody.MovePosition (rigidBody.position + new Vector2 (0.0f, moveSpeed) * Time.fixedDeltaTime);
						//float angle = Vector2.Angle (Vector2.zero, new Vector2 (0.0f, moveSpeed));
						transform.rotation = Quaternion.AngleAxis (0.0f, Vector3.forward);
					}
					moveCounter--;
				}
			} else {
				// chase movement if player is detected
				Vector2 diffPosition = targetPosition - rigidBody.position;
				rigidBody.MovePosition (rigidBody.position + new Vector2(diffPosition.x / Mathf.Abs (diffPosition.x), 
				                                                         diffPosition.y / Mathf.Abs (diffPosition.y)) * moveSpeed * Time.fixedDeltaTime);
				float angle = Mathf.Atan2 (diffPosition.y, diffPosition.x) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.AngleAxis (angle - 90.0f, Vector3.forward);
			}
		}
	}

	void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.name == "Character") {
			Controller controller = col.gameObject.GetComponent<Controller>();
			controller.energy = Mathf.Max (controller.energy - 10.0f, 0.0f);
			parentSpawner.GetComponent<ZombieSpawner>().zombies.Remove (gameObject);
			parentSpawner.GetComponent<ZombieSpawner>().numZombiesToRenew++;
			Destroy (gameObject);
		}
	}
}
