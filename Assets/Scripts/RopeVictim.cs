using UnityEngine;

public class RopeVictim : MonoBehaviour
{
	[SerializeField]
	private Transform ropeMaster;

	[SerializeField]
	private float maxDistance = 5f;

	private Vector3 velocity;

	public Vector3 Velocity { get { return velocity; } }

	void Start ()
	{
		
	}

	void OnCollisionEnter(Collision col)
	{		
		velocity.x = -velocity.x;
	}
	
	void FixedUpdate ()
	{
		float someForce = 0.1f;

		Vector3 dirToMaster = (ropeMaster.transform.position - transform.position).normalized;

		

		float distanceToMaster = Vector3.Distance(ropeMaster.transform.position, transform.position);

		if (distanceToMaster > maxDistance)
		{
			velocity += dirToMaster * someForce;
		}

		velocity.y -= 0.981f * Time.deltaTime;
		
		velocity = Vector3.ClampMagnitude(velocity, 1f);

		transform.position += velocity;
	}
}