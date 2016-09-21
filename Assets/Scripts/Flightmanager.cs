using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(InputManager))]
public class Flightmanager : MonoBehaviour
{
	private	Rigidbody rb;
	private InputManager input;

	private float maxIdleSpeed = 1000f;

	private Vector3 direction;
	private float velocity;

	private float motor;
	private float accelerationSpeed = 1f;
	private float decelerationSpeed = 1f;

	private float speedModifier = 700f;

	private float tilt;
	private float tiltSpeed = 120f;

	private enum Orientation { Right, Left }
	private Orientation orientation = Orientation.Right;

	private float pitchSpeed = 5f;
	private float rollSpeed = 2.5f;

	[SerializeField]
	private Transform plane;

	[SerializeField]
	private float maxSpeed = 50f;
	[SerializeField]
	private float minSpeed = 20f;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		input = GetComponent<InputManager>();
	}

	
	void Update ()
	{
		//ControlMotor();

		float angle = Vector3.Angle(Vector3.right, transform.forward);
		if (rb.velocity.x < 0)
		{
			angle = Vector3.Angle(Vector3.left, transform.forward);
		}

		velocity += Time.deltaTime * 5f;


		print(angle);
		//Facing up
		if (transform.position.y + transform.forward.y > transform.position.y)
		{
			velocity *= 1 - (angle / 90f) * Time.deltaTime;
		}
		else
		{
			velocity *= 1 + (angle / 90f) * Time.deltaTime;
		}

		velocity = Mathf.Clamp(velocity, minSpeed, maxSpeed);

		Vector3 newDir = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f);

		transform.forward = Vector3.Lerp(transform.forward, newDir, Time.deltaTime * pitchSpeed);

		Vector3 planeRot = plane.eulerAngles;
		planeRot.x = transform.eulerAngles.x;
		planeRot.y = Mathf.LerpAngle(planeRot.y, transform.eulerAngles.y, Time.deltaTime * (velocity * 0.25f));
		plane.eulerAngles = planeRot;

		plane.position = transform.position;

		rb.velocity = transform.forward * velocity;
	}
}