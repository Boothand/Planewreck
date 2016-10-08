using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RopeManager : MonoBehaviour
{
	[SerializeField]
	protected int ropeSegments = 2;

	[SerializeField]
	protected PhysicsObject ropeObject;
	protected bool isConnected = true;

	protected float distanceToTarget;

	[SerializeField]
	[Range(0f, 2f)]
	protected float ropeWidth = 0.4f;

	[SerializeField]
	protected float ropeLength = 1f;

	[SerializeField]
	protected float elasticity = 1f;

	protected LineRenderer lineRend;

	protected void Start()
	{
		lineRend = GetComponent<LineRenderer>();

		lineRend.SetVertexCount(ropeSegments);
	}

	void SetRopeWidth()
	{
		float width = (9 - distanceToTarget) / 10f;
		width = Mathf.Clamp(width, ropeWidth, ropeWidth + distanceToTarget);
		lineRend.SetWidth(width, width);
	}

	public virtual void Break()
	{
		isConnected = false;
		lineRend.SetVertexCount(0);
	}

	public virtual void Connect()
	{
		isConnected = true;
		lineRend.SetVertexCount(ropeSegments);
	}

	void UpdateRope()
	{
		lineRend.SetPosition(0, transform.position);
		lineRend.SetPosition(1, ropeObject.transform.position);
	}

	void UpdateRopeObject()
	{
		float someForce = 0.008f;

		Vector3 dirToTarget = (transform.position - ropeObject.transform.position).normalized;

		if (distanceToTarget > ropeLength)
		{
			ropeObject.AddVelocity(dirToTarget * (distanceToTarget - ropeLength) * someForce);
		}

		Vector3 clampedSpeed = Vector3.ClampMagnitude(ropeObject.getVelocity, elasticity);
		ropeObject.SetVelocity(clampedSpeed);
	}

	protected void Update()
	{
		if (isConnected)
		{
			UpdateRope();

			distanceToTarget = Vector3.Distance(transform.position, ropeObject.transform.position);

			SetRopeWidth();

			UpdateRopeObject();
		}
	}
}