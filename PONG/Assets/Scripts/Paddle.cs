using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle : MonoBehaviour {

	[SerializeField]
	float speed = 10.0f;
		
	string input;
	public bool isRight;
	public float height;

	private Vector2 currentMovement;

	// Use this for initialization
	void Start () {
		height = transform.localScale.y;
	}
	
	public void Init(bool isRightPaddle)
	{
		isRight = isRightPaddle;
		
		Vector2 pos = Vector2.zero;
		
		if(isRightPaddle)
		{
			// Place paddle to right side
			pos = new Vector2(GameplayManager.topRight.x, 0);
			pos += Vector2.left * transform.localScale.x; // Left offset by width
			
			input = "PaddleRight";
		}
		else
		{
			// Place paddle to left side
			pos = new Vector2(GameplayManager.bottomLeft.x, 0);
			pos += Vector2.right * transform.localScale.x; // Right offset by width
			
			input = "PaddleLeft";
		}
		
		// Update this paddle's location
		transform.position = pos;
		
		// Make the name more readable
		transform.name = input;
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Set the paddle move distance
		float moveDistance = currentMovement.y * speed * Time.deltaTime;

		// Keep the paddle on-screen
		if (moveDistance < 0 && transform.position.y < (GameplayManager.bottomLeft.y + height / 2))
			moveDistance = 0;
		else if (moveDistance > 0 && transform.position.y > (GameplayManager.topRight.y - height / 2))
			moveDistance = 0;

		// Move the paddle
		transform.Translate(moveDistance * Vector2.up);
	}

	public void OnMove(InputValue input)
    {
		// Get the Vector2 representing the current movement input
		currentMovement = input.Get<Vector2>();
	}
}
