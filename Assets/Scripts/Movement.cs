using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour {

	public KeyCode moveLeftKey = KeyCode.A;
	public KeyCode moveRightKey = KeyCode.D;
	public KeyCode jump = KeyCode.W;
	public float speed;
	private Transform tf;
	private bool isGrounded = true;	
	public int jumpHeight;
	public float jumpSpeed;
	private bool movingRight;
	private bool movingLeft;
	private Touch touchOne;
	private Touch touchTwo;
	private float lastTouchOne;
	private float lastTouchTwo;
	public Text text;
	public float jumpSwipeRequirement = 100;

	// Use this for initialization
	void Start () {
		tf = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (movingRight) {
			MoveRight ();
		} else if(movingLeft) {
			MoveLeft ();
		}
		if (Input.GetKeyDown (jump)) {
			if (isGrounded) {
				StartCoroutine ("Jump");
			}
		}
		touchOne = Input.GetTouch (0);
		if (lastTouchOne == 0) {
			lastTouchOne = touchOne.position.y;
		}
		if (touchOne.position.y >= lastTouchOne + jumpSwipeRequirement) {
			StartJump ();
		}
		lastTouchOne = touchOne.position.y;
		touchTwo = Input.GetTouch (1);
		if (lastTouchTwo == 0) {
			lastTouchTwo = touchTwo.position.y;
		}
		if (touchTwo.position.y >= lastTouchTwo + jumpSwipeRequirement) {
			StartJump ();
		}
		lastTouchTwo = touchTwo.position.y;
		//text.text = ("Current: " + touchOne.position.y +" Last: " + lastTouchOne);
	}

	private IEnumerator Jump(){
		isGrounded = false;
		int i = 0;
		while(i < jumpHeight){
			tf.position += tf.up * jumpSpeed * Time.deltaTime;
			i++;
			yield return null;
		}
		StopCoroutine ("Jump");
	}

	public void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Ground") {
			isGrounded = true;
		}
	}

	private void MoveLeft(){
		tf.position -= tf.right * speed * Time.deltaTime;
	}

	private void MoveRight(){
		tf.position += tf.right * speed * Time.deltaTime;
	}

	public void SetMoveRight(){
		movingRight = true;
	}
	public void SetMoveLeft(){
		movingLeft = true;
	}
	public void StopLeftMovement(){
		movingLeft = false;
	}
	public void StopRightMovement(){
		movingRight = false;
	}
	public void StartJump(){
		if (isGrounded) {
			StartCoroutine ("Jump");
		}
	}
}
