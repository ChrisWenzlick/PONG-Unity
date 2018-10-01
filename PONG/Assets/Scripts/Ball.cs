using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	[SerializeField]
	float speed;
	
	float radius;
	Vector2 direction;
	

	// Use this for initialization
	void Start () {
		direction = Vector2.one.normalized; // direction is (1, 1) normalized
		radius = transform.localScale.x / 2;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(direction * speed * Time.deltaTime);
		
		// Top and bottom bounces
		if(direction.y < 0 && transform.position.y < GameplayManager.bottomLeft.y + radius)
			direction.y = -direction.y;
		else if(direction.y > 0 && transform.position.y > GameplayManager.topRight.y - radius)
			direction.y = -direction.y;
		
		
		
		// Game over
		if(direction.x < 0 && transform.position.x < GameplayManager.bottomLeft.x + radius)
		{
			Debug.Log("Right player wins!");
			Time.timeScale = 0;
			enabled = false;
		}
		else if(direction.x > 0 && transform.position.x > GameplayManager.topRight.x - radius)
		{
			Debug.Log("Left player wins!");
			Time.timeScale = 0;
			enabled = false;
		}
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
        Debug.Log("Trigger entered");
		if(other.tag == "Paddle")
		{
			bool isRight = other.GetComponent<Paddle>().isRight;
			
			if(isRight && direction.x > 0)
				direction.x = -direction.x;
			else if(!isRight && direction.x < 0)
				direction.x = -direction.x;
		}
	}
}
