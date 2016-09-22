using UnityEngine;

public class StructureElement : MonoBehaviour
{
	private bool active;
	private Vector3 velocity;

	private Vector3 rotation;

	void Start ()
	{
		
	}

	public void SetVelocity(Vector3 newVelocity)
	{
		velocity = newVelocity;
		rotation = newVelocity;
		active = true;
	}

	void Update ()
	{
		if (!active)
		{
			return;
		}

		velocity.y -= 0.281f * Time.deltaTime;

		transform.position += velocity;
		transform.Rotate(velocity * Time.deltaTime);
	}
}