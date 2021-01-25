using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	[SerializeField]
	[Min(0.0f), Tooltip("The maximum speed at which the ball can travel")]
	float maxSpeed = 30.0f;

	[SerializeField]
	[Range(0.0f, 1.0f), Tooltip("The starting speed of the ball, expressed as a fraction of the max speed")]
	float startingSpeedScale = 0.1f;

	[SerializeField]
	[Range(0.0f, 1.0f), Tooltip("The amount that will be added to the ball's movement speed upon contacting a paddle, expressed as a fraction of the max speed")]
	float speedInfluence = 0.05f;
	
	float radius;
	float currentSpeed;
	Vector2 currentMoveVector;
	

	// Use this for initialization
	void Start () {
		radius = transform.localScale.x / 2;

		// Start the ball in a random direction to either the left or the right
		if(Random.value > 0.5)
			// Send to right player
			ResetBall(-20.0f, 20.0f);
		else
			// Send to left player
			ResetBall(160.0f, 200.0f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(currentMoveVector * currentSpeed * Time.deltaTime);
		
		// Top and bottom bounces
		if(currentMoveVector.y < 0 && transform.position.y < GameplayManager.bottomLeft.y + radius)
			currentMoveVector.y = -currentMoveVector.y;
		else if(currentMoveVector.y > 0 && transform.position.y > GameplayManager.topRight.y - radius)
			currentMoveVector.y = -currentMoveVector.y;
		
		
		
		// Game over
		if(currentMoveVector.x < 0 && transform.position.x < GameplayManager.bottomLeft.x + radius)
		{
			// Right player scored
			ResetBall(-20.0f, 20.0f);
		}
		else if(currentMoveVector.x > 0 && transform.position.x > GameplayManager.topRight.x - radius)
		{
			// Left player scored
			ResetBall(160.0f, 200.0f);
		}
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("Paddle"))
		{
			Paddle paddle = other.GetComponent<Paddle>();
			bool isRight = paddle.isRight;
			
			// Reverse ball direction
			if(isRight && currentMoveVector.x > 0)
				currentMoveVector.x = -currentMoveVector.x;
			else if(!isRight && currentMoveVector.x < 0)
				currentMoveVector.x = -currentMoveVector.x;

			// Modify the ball's travel based on where it contacted the paddle
			// *NOTE: This will need to be modified to support nonstandard paddle axes, such as horizontal and diagonal
			Vector2 offsetFromPaddle = transform.position - other.transform.position;
			float distanceFromCenter = Mathf.Clamp(offsetFromPaddle.y / (paddle.height / 2), -1.0f, 1.0f);
			float horizontalInfluence = (isRight) ? (1 - Mathf.Abs(distanceFromCenter)) * -1 : (1 - Mathf.Abs(distanceFromCenter));

			// Add more speed vertically when near the paddle edge and horizontally when near the paddle center
			Vector2 influenceVector = new Vector3(horizontalInfluence, distanceFromCenter);
			currentMoveVector += influenceVector;
			currentMoveVector.Normalize();

			// Increase the overall speed
			currentSpeed += speedInfluence * maxSpeed;
			if (currentSpeed > maxSpeed)
				currentSpeed = maxSpeed;
		}
	}

	public void ResetBall(float minAngle = 0.0f, float maxAngle = 0.0f)
    {
		transform.position = Vector3.zero;
		currentSpeed = startingSpeedScale * maxSpeed;
		currentMoveVector = GetRandomLaunchVector(minAngle, maxAngle);
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
		return DegreeToVector2(newAngle);
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
