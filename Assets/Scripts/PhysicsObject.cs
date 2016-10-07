using System.Collections;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
	protected Vector3 velocity;

	[SerializeField]
	protected bool ignoreGravity;

	[SerializeField]
	protected bool ignoreCollisions;

	[SerializeField]
	float gravity = -9.81f;



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
			velocity.y += gravity / 100f * Time.deltaTime;
		}

		transform.position += velocity;
	}
	
	protected virtual void Update()
	{

	}
}