using UnityEngine;

public class RopeVictim : MonoBehaviour
{
	private bool colliding;

	public Vector3 Velocity { get; set; }
	public bool Colliding { get { return colliding; } }

	[SerializeField]
	private float collisionThreshold = 3f;

	void Start ()
	{
		
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.relativeVelocity.magnitude > collisionThreshold)
		{
			colliding = true;
		}
	}
}