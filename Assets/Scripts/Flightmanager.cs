using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(AirplaneManager))]
public class Flightmanager : MonoBehaviour
{
	private InputManager input;
	private AirplaneManager properties;
	
	private float speed;

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
	private Vector3 startDirection;

	public Vector3 velocity { get { return transform.forward * speed; } }
	public Vector3 StartDirection { get { return startDirection; } }


	void Start ()
	{
		properties = GetComponent<AirplaneManager>();

		plane = properties.meshInstance.transform;
		input = GetComponent<InputManager>();

		startDirection = transform.forward;
		
		speed = minSpeed;
	}

	void FightGravity()
	{
		//Compare angle deviation from center
		float angle = Vector3.Angle(Vector3.right, transform.forward);

		if (movementDir.x < 0)
		{
			angle = Vector3.Angle(Vector3.left, transform.forward);
		}

		speed += Time.deltaTime * 5f;

		//Gradually go slower uphill, faster downhill
		if (transform.position.y + transform.forward.y > transform.position.y)
		{
			speed *= 1 - (angle / 90f) * Time.deltaTime;
		}
		else
		{
			speed *= 1 + (angle / 90f) * Time.deltaTime;
		}
	}

	void UpdateMesh()
	{
		//Make the plane mesh copy the x and y rotation of this object

		Vector3 planeRot = plane.eulerAngles;
		planeRot.x = transform.eulerAngles.x;
		planeRot.y = Mathf.LerpAngle(planeRot.y, transform.eulerAngles.y, Time.deltaTime * rollSpeed * (speed * 0.25f));
		plane.eulerAngles = planeRot;

		plane.position = transform.position;

		propeller.transform.Rotate(propeller.transform.up, Time.deltaTime * speed * propellerSpeed, Space.World);
	}

	void FixedUpdate()
	{
		transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
	}
	
	void Update ()
	{
		FightGravity();

		speed = Mathf.Clamp(speed, minSpeed, maxSpeed);

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
			plane = properties.meshInstance.transform;
		}		

		//Force z position to 0
		Vector3 pos = transform.position;
		pos.z = 0f;
		transform.position = pos;
	}
}