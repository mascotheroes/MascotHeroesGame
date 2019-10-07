using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
