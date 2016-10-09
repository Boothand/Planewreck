using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RopeManager : MonoBehaviour
{
	protected LineRenderer lineRend;

	[SerializeField]
	protected int ropeSegments = 2;

	[SerializeField]
	protected PhysicsObject ropeObject;

	[SerializeField]
	[Range(0f, 2f)]
	protected float ropeWidth = 0.4f;

	[SerializeField]
	protected float ropeLength = 1f;

	[SerializeField]
	protected float elasticity = 1f;

	[SerializeField]
	float inheritedVelocity = 0f;

	protected float distanceToTarget;
	protected bool isConnected = true;

	Vector3 velocity;
	Vector3 lastPos;

	[SerializeField]
	bool debug = false;

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
		Vector3 dirToTarget = (transform.position - ropeObject.transform.position).normalized;

		velocity = transform.position - lastPos;

		lastPos = transform.position;

		if (debug)
		{
			Debug.DrawRay(ropeObject.transform.position, ropeObject.getVelocity * 3f, Color.red);
		}

		if (distanceToTarget > ropeLength)
		{
			Vector3 dirToRope = (transform.position - ropeObject.transform.position).normalized;
			if (distanceToTarget > ropeLength + elasticity)
			{
				Vector3 outerCirclePoint = transform.position - dirToTarget * (ropeLength + elasticity);
				ropeObject.transform.position = outerCirclePoint;

				float distanceFromMiddleX = transform.position.x - ropeObject.transform.position.x;
				Vector3 bounceSurface = Quaternion.Euler(0f, 0f, 90f) * dirToTarget * Mathf.Sign(distanceFromMiddleX);
				Vector3 bounceVelocity = Vector3.Reflect(dirToTarget, bounceSurface).normalized;

				ropeObject.AddVelocity(bounceVelocity * Mathf.Abs(distanceFromMiddleX) * Time.deltaTime);

				ropeObject.SetVelocity(velocity * inheritedVelocity + ropeObject.getVelocity * (1f - Time.deltaTime));

			}
			
			ropeObject.AddVelocity(dirToRope * Time.deltaTime);
		}

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