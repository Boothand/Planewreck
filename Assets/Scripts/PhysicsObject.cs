using System.Collections;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
	protected Vector3 velocity;

	[SerializeField]
	protected bool ignoreGravity;

	[SerializeField]
	protected bool ignoreCollisions;
	
	public Vector3 gravity = new Vector3(0f, -9.81f, 0f);

	[SerializeField]
	float maxVelocity = 1f;

	public Vector3 getVelocity { get { return velocity; } }


	protected virtual void Start()
	{

	}

	public void SetVelocity(Vector3 newVelocity)
	{
		velocity = newVelocity;
	}

	public void AddVelocity(Vector3 force)
	{
		velocity += force;
	}

	public void MultiplyVelocity(float amount)
	{
		velocity *= amount;
	}

	protected virtual void FixedUpdate()
	{
		if (!ignoreGravity)
		{
			velocity += gravity / 100f * Time.deltaTime;

			velocity = Vector3.ClampMagnitude(velocity, maxVelocity);
		}

		transform.position += velocity;
	}
	
	protected virtual void Update()
	{

	}
}