using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfieStick : MonoBehaviour {

	public float panSpeed = 10f;

	private GameObject player;
	private Vector3 armRotation;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		armRotation = transform.rotation.eulerAngles;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = player.transform.position;
		armRotation.y += Input.GetAxis("RHoriz") * panSpeed;
		armRotation.z += Input.GetAxis("RVert") * panSpeed;
		transform.rotation = Quaternion.Euler(armRotation);
	}
}
