using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RopeManager : MonoBehaviour
{
	[SerializeField]
	protected int ropeSegments = 2;

	[SerializeField]
	protected Transform ropeObject;
	protected bool isConnected = true;

	protected Vector3 ropeObjectVelocity;

	protected float distanceToTarget;

	[SerializeField]
	[Range(0f, 2f)]
	protected float ropeWidth = 0.4f;

	[SerializeField]
	protected float ropeLength = 1f;

	[SerializeField]
	protected float elasticity = 1f;

	protected LineRenderer lineRend;

	protected void Start ()
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

	public void Break()
	{
		isConnected = false;
		lineRend.SetVertexCount(0);

		if (ropeObject.GetComponent<CameraTarget>())
		{
			ropeObject.GetComponent<CameraTarget>().Trackable = false;
		}
	}
	
	protected void Update ()
	{
		if (isConnected)
		{
			lineRend.SetPosition(0, transform.position);

			lineRend.SetPosition(1, ropeObject.position);

			distanceToTarget = Vector3.Distance(transform.position, ropeObject.position);

			SetRopeWidth();

			float someForce = 0.008f;

			Vector3 dirToTarget = (transform.position - ropeObject.position).normalized;

			if (distanceToTarget > ropeLength)
			{
				ropeObjectVelocity += dirToTarget * (distanceToTarget-ropeLength) * someForce;
			}
		}
		
			ropeObjectVelocity = Vector3.ClampMagnitude(ropeObjectVelocity, elasticity);
			ropeObjectVelocity.y -= 0.981f * Time.deltaTime;

			ropeObject.position += ropeObjectVelocity;

		
	}
}