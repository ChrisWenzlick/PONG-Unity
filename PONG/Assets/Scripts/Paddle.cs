using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

	[SerializeField]
	float speed = 10.0f;
		
	string input;
	public bool isRight;
	public float height;

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
	void Update () {
		// Set the paddle move distance
		float move = Input.GetAxis(input) * speed * Time.deltaTime;
		
		if(move < 0 && transform.position.y < (GameplayManager.bottomLeft.y + height / 2))
			move = 0;
		else if(move > 0 && transform.position.y > (GameplayManager.topRight.y - height / 2))
			move = 0;
		
		// Move the paddle
		transform.Translate(move * Vector2.up);
	}
}
