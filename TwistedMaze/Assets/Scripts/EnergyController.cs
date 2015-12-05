using UnityEngine;
using System.Collections;

public class EnergyController : MonoBehaviour {

	public GameObject player;
	private Controller controller;
	public float energyValue;
	public Texture2D energyBarFull;
	public Texture2D energyBarEmpty;
	public float offsetX;
	public float offsetY;
	public Vector2 pos;
	public Vector2 size;

	// Use this for initialization
	void Start () {
		energyValue = 100.0f;
		offsetX = -30.0f;
		offsetY = 20.0f;
		pos = new Vector2 (Screen.width / 2.0f + offsetX, Screen.height / 2.0f + offsetY);
		size = new Vector2 (60.0f, 20.0f);
		controller = player.GetComponent<Controller>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 0f) {
			return;
		}
		energyValue = controller.energy / Controller.MAX_ENERGY;
	}

	void OnGUI () {
		if (Time.timeScale == 0f) {
			return;
		}
		GUIStyle style = GUI.skin.button;
		GUIStyle fontStyle = new GUIStyle ();
		fontStyle.fontSize = 9;
		fontStyle.fontStyle = FontStyle.Bold;
		fontStyle.alignment = TextAnchor.MiddleCenter;
		fontStyle.normal.textColor = Color.black;
		GUI.BeginGroup (new Rect (pos.x, pos.y, size.x, size.y), style);
			GUI.Box (new Rect (0.0f, 0.0f, size.x, size.y), energyBarEmpty, style);
			GUI.BeginGroup (new Rect (0.0f, 0.0f, size.x * energyValue, size.y), style);
				GUI.Box (new Rect (0.0f, 0.0f, size.x, size.y), energyBarFull, style);
			GUI.EndGroup ();
			GUI.Label (new Rect (0.0f, 4.0f, size.x, 12.0f), energyValue.ToString ("0.00%"), fontStyle);
		GUI.EndGroup ();
	}
}
