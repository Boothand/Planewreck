using UnityEngine;

public class WreckingBall : PhysicsObject
{
	public bool colliding { get; private set; }

	[SerializeField]
	private float collisionThreshold = 3f;

	new void Update()
	{
		base.Update();

		colliding = false;
	}

	void OnCollisionEnter(Collision col)
	{
		//if (col.relativeVelocity.magnitude > collisionThreshold)
		//{
			colliding = true;
		//}
	}

	void OnCollisionExit(Collision col)
	{
		colliding = false;
	}
}