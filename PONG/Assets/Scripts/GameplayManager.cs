using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour {

	public Ball ball;
	public Paddle paddle;
	
	public static Vector2 bottomLeft;
	public static Vector2 topRight;

	// Use this for initialization
	void Start () {
		
		// Convert screen pixel coordinates into game's coordinates
		bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
		topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
		
		// Create the ball
		Instantiate(ball);
		
		// Create the paddles
		Paddle paddleLeft = Instantiate(paddle) as Paddle;
		Paddle paddleRight = Instantiate(paddle) as Paddle;
		paddleLeft.Init(false); // left paddle
		paddleRight.Init(true); // right paddle
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
