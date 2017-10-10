using UnityEngine;
using System.Collections;

public class PlayerStartPoint : MonoBehaviour {

	//Player start points for each team? Snap to team point? Even CPU opponents?

	private PlayerController thePlayer;
	private CameraController theCamera;

	public Vector2 startDirection;

	// Use this for initialization
	void Start () {


		thePlayer = FindObjectOfType<PlayerController> ();
		thePlayer.transform.position = transform.position;

		theCamera = FindObjectOfType<CameraController> ();
		theCamera.transform.position = new Vector3 (transform.position.x, transform.position.y, theCamera.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
