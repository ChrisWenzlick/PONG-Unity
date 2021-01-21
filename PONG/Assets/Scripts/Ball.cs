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
		radius = transform.localScale.x / 2;
		ResetBall(); // need to fix so that unreachable serves don't happen (straight up or down)
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
			ResetBall(225.0f, 315.0f);
			/*
			// End game with right player victory
			Debug.Log("Right player wins!");
			Time.timeScale = 0;
			enabled = false;
			*/
		}
		else if(direction.x > 0 && transform.position.x > GameplayManager.topRight.x - radius)
		{
			ResetBall(45.0f, 135.0f);
			/*
			// End game with left player victory
			Debug.Log("Left player wins!");
			Time.timeScale = 0;
			enabled = false;
			*/
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

	// NOTE: this method currently doesn't return the correct vectors
	public void ResetBall(float minAngle = 0.0f, float maxAngle = 0.0f)
    {
		transform.position = Vector3.zero;
		direction = GetRandomLaunchVector(minAngle, maxAngle);
		Debug.Log("(" + minAngle + ", " + maxAngle + ")  ->  " + direction);
    }

	/// <summary>
	/// Gets a unit vector with a random direction between two angles, inclusive. If both
	/// angles are equal, the vector will be chosen from a full 360-degree circle.
	/// </summary>
	/// <param name="minAngle">The lower bound of the angle range, between 0.0f and 360.0f</param>
	/// <param name="maxAngle">The upper bound of the angle range, between 0.0f and 360.0f</param>
	/// <returns></returns>
	public Vector2 GetRandomLaunchVector(float minAngle = 0.0f, float maxAngle = 0.0f)
    {
		// Remove any excess full rotations from the angle values
		minAngle %= 360.0f;
		maxAngle %= 360.0f;

		// If the angles are the same, use the full circle
		if (minAngle == maxAngle)
			return Random.insideUnitCircle.normalized;

		// Return a random unit vector between the two angles, inclusive
		float newAngle = Random.Range(minAngle, maxAngle);
		newAngle = ConvertTo360Angle(newAngle);
		return RadianToVector2(newAngle * Mathf.Deg2Rad);
	}

	private float ConvertTo360Angle(float angle)
    {
		if (angle == 0.0f)
			return 0.0f;

		angle %= 360.0f;
		if (angle < 0.0f)
			angle += 360.0f;

		return angle;
    }

	public static Vector2 RadianToVector2(float radian)
	{
		return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
	}

	public static Vector2 DegreeToVector2(float degree)
	{
		return RadianToVector2(degree * Mathf.Deg2Rad);
	}
}
