using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaySystem : MonoBehaviour {

	private const int bufferFrames = 100;
	private MyKeyFrame[] keyFrames = new MyKeyFrame[bufferFrames];

	private Rigidbody rigidbody;
	private GameManager gameManager;
	private bool firstRecordMade = false;

	private int lastFrameRecorded=0;
	private int lastFramePlayedBack =0;
	private int totalFramesRecorded=1;

	//private 

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody>();
		gameManager = GameObject.FindObjectOfType<GameManager>();

	}
	
	// Update is called once per frame
	void Update () {
		if (gameManager.recording){
			Record ();
		}else if (!gameManager.recording && firstRecordMade) {
			PlayBack();
			Debug.Log("Playbacking");
		}
	}

	void Record () {
		rigidbody.isKinematic = false;
		firstRecordMade = true;
		int frame = Time.frameCount % bufferFrames;
		float time = Time.time;
		lastFrameRecorded = Time.frameCount;
		totalFramesRecorded = lastFrameRecorded-lastFramePlayedBack;
		//print ("Writing frame: " + frame);
		//print ("Frame: " + Time.frameCount);
		keyFrames [frame] = new MyKeyFrame (time, transform.position, transform.rotation);
	}

	void PlayBack () {
		rigidbody.isKinematic = true;
		int frame;

		if (totalFramesRecorded > bufferFrames){
			frame = Time.frameCount % bufferFrames;
		}else{
			frame = Time.frameCount % totalFramesRecorded;
		}
		//print ("Reading frame: " + frame);
		transform.position = keyFrames [frame].position;
		transform.rotation = keyFrames [frame].rotation;
		lastFramePlayedBack = Time.frameCount;
	}

	/// <summary>
	/// A structure for storing time, rotation and position.
	/// </summary>
	public struct MyKeyFrame {
		public float frameTime;
		public Vector3 position;
		public Quaternion rotation;

		public MyKeyFrame ( float aTime, Vector3 aPosition, Quaternion aRotation){

			frameTime = aTime;
			position= aPosition;
			rotation = aRotation;

		}
		


	}
}