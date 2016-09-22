using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DestructableStatic : MonoBehaviour
{
	StructureElement[] elements;

	[SerializeField]
	private float yThreshold = 1f;

	[SerializeField]
	private float velocityDamper = 0.25f;

	[SerializeField]
	private Transform top;

	[SerializeField]
	private float explosionRange = 0.2f;

	private BoxCollider boxCol;

	void Start ()
	{
		elements = GetComponentsInChildren<StructureElement>();
		boxCol = GetComponent<BoxCollider>();
	}

	void OnCollisionEnter(Collision col)
	{
		Vector3 collisionPoint = col.contacts[0].point;

		float dropAmount = Mathf.Abs(top.position.y - collisionPoint.y) / transform.localScale.y;

		Vector3 boxCenter = boxCol.center;
		boxCenter.y -= dropAmount;
		boxCol.center = boxCenter;

		Vector3 topPos = top.position;
		topPos.y -= dropAmount * transform.localScale.y;
		top.position = topPos;

		Vector3 velocity = col.relativeVelocity;

		if (col.transform.GetComponent<RopeVictim>())
		{
			velocity = col.transform.GetComponent<RopeVictim>().Velocity;
			velocity.x = -velocity.x;
		}
		else if (col.transform.GetComponent<Flightmanager>())
		{
			velocity = col.transform.GetComponent<Flightmanager>().Velocity / 60f;
		}

		foreach (StructureElement element in elements)
		{
			if (element.transform.position.y + yThreshold >= collisionPoint.y)
			{
				velocity = new Vector3(Random.Range(velocity.x - explosionRange, velocity.x + explosionRange),
										Random.Range(velocity.y - explosionRange, velocity.y + explosionRange));

				velocity *= velocityDamper;

				element.SetVelocity(velocity);
			}
		}
	}
	
	void Update ()
	{
		
	}
}