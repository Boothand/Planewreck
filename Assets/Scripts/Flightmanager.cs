using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(PlayerProperties))]
public class Flightmanager : MonoBehaviour
{
	private	Rigidbody rb;
	private InputManager input;
	private PlayerProperties properties;
	
	private float velocity;

	private float pitchSpeed = 5f;
	private float rollSpeed = 0.45f;
	
	private Transform plane;

	[SerializeField]
	private Transform propeller;
	[SerializeField]
	private float propellerSpeed = 100f;

	[SerializeField]
	private float maxSpeed = 50f;
	[SerializeField]
	private float minSpeed = 20f;

	private Vector3 movementDir;

	public Vector3 Velocity { get { return transform.forward * velocity; } }

	private Vector3 startDirection;

	void Start ()
	{
		properties = GetComponent<PlayerProperties>();

		plane = properties.MeshInstance;
		rb = GetComponent<Rigidbody>();
		input = GetComponent<InputManager>();

		startDirection = transform.forward;
		
		velocity = minSpeed;
	}

	void FightGravity()
	{
		//Compare angle deviation from center
		float angle = Vector3.Angle(Vector3.right, transform.forward);

		if (rb.velocity.x < 0)
		{
			angle = Vector3.Angle(Vector3.left, transform.forward);
		}

		velocity += Time.deltaTime * 5f;

		//Gradually go slower uphill, faster downhill
		if (transform.position.y + transform.forward.y > transform.position.y)
		{
			velocity *= 1 - (angle / 90f) * Time.deltaTime;
		}
		else
		{
			velocity *= 1 + (angle / 90f) * Time.deltaTime;
		}
	}

	void UpdateMesh()
	{
		//Make the plane mesh copy the x and y rotation of this object

		Vector3 planeRot = plane.eulerAngles;
		planeRot.x = transform.eulerAngles.x;
		planeRot.y = Mathf.LerpAngle(planeRot.y, transform.eulerAngles.y, Time.deltaTime * rollSpeed * (velocity * 0.25f));
		plane.eulerAngles = planeRot;

		plane.position = transform.position;

		propeller.transform.Rotate(propeller.transform.up, Time.deltaTime * velocity * propellerSpeed, Space.World);
	}
	
	void Update ()
	{
		FightGravity();

		velocity = Mathf.Clamp(velocity, minSpeed, maxSpeed);

		//Face towards the input direction
		movementDir = new Vector3(input.Horizontal, input.Vertical);
		transform.forward = Vector3.Lerp(transform.forward, movementDir, Time.deltaTime * pitchSpeed);
		if (transform.forward == Vector3.forward)
			transform.forward = startDirection;

		if (plane)
		{
			UpdateMesh();
		}
		else
		{
			plane = properties.MeshInstance;
		}
		
		rb.velocity = transform.forward * velocity;

		//Force z position to 0
		Vector3 pos = transform.position;
		pos.z = 0f;
		transform.position = pos;

	}
}