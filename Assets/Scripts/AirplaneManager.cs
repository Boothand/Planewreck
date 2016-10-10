using UnityEngine;

[RequireComponent(typeof(Flightmanager))]
[RequireComponent(typeof(HealthManager))]
public class AirplaneManager : MonoBehaviour
{
	public int wins;
	public Flightmanager flight { get; private set; }
	public HealthManager health { get; private set; }
	InputManager input;
	
	public Color color = Color.white;

	public string playerName { get; private set; }

	public bool isAI;

	[SerializeField]
	GameObject meshPrefab;

	public GameObject meshInstance { get; private set; }

	[SerializeField]
	WreckingBall smasher;

	Vector3 startPosition;

	Vector3 smasherStartOffset;

	[SerializeField]
	GlobalVariables.ControlType inputType = GlobalVariables.ControlType.WASD;


	void Awake()
	{
		meshInstance = transform.root.FindChild("Mesh").gameObject;
	}

	void Start()
	{
		flight = GetComponent<Flightmanager>();
		health = GetComponent<HealthManager>();
		input = GetComponent<InputManager>();

		if (playerName == null)
		{
			playerName = "Noname";
		}

		startPosition = transform.position;
		smasherStartOffset = smasher.transform.position - meshInstance.transform.position;

		SetInputType(inputType);

		SetColor(color);
	}

	public WreckingBall GetSmasher()
	{
		return smasher;
	}

	public void ResetPosition()
	{
		transform.position = startPosition;
		transform.forward = flight.StartDirection;

		meshInstance.transform.position = transform.position;
		meshInstance.transform.rotation = transform.rotation;

		SetPosition(transform.position, true);
	}

	public void SetPosition(Vector3 pos, bool facingCenter = false) //used when respawing to set all the planes position.
	{
		transform.position = pos;

		Vector3 facing = Vector3.right;
		if (pos.x < 0f)
			facing *= -1f;
		if (facingCenter)
			facing *= -1f;

		transform.LookAt(transform.position + facing, Vector3.up);

		meshInstance.transform.position = transform.position;
		meshInstance.transform.rotation = transform.rotation;
	}

	public void SetProperties(int index) //used once when object is first Instantiated in GameManager
	{
		playerName = GlobalVariables.names[index];

		if (GlobalVariables.inputs[index] != GlobalVariables.ControlType.Noone)
		{
			inputType = GlobalVariables.inputs[index];
		}

		if (GlobalVariables.colors[index] != null)
		{
			color = GlobalVariables.colors[index];
		}
	}

	public void ResetSmasherPosition()
	{
		//smasher.position = smasherStartOffset;
		smasher.transform.position = meshInstance.transform.position + smasherStartOffset;
	}

	public void Revive()
	{
		health.Dead = false;

		Destroy(meshInstance);
		meshInstance = Instantiate(meshPrefab, transform.root);

		meshInstance.transform.rotation = meshPrefab.transform.rotation;
		meshInstance.transform.localPosition = Vector3.zero;
		meshInstance.transform.localScale = meshPrefab.transform.localScale;

		health.EnableComponents();

		SetColor(color);
	}

	public void SetColor(Color newColor)
	{
		foreach (Transform child in meshInstance.transform)
		{
			child.GetComponent<Renderer>().material.color = newColor;
		}
	}

	public void EnableInput(bool truth = true)
	{
		input.enabled = truth;
	}

	public void EnableFlight(bool truth = true)
	{
		flight.enabled = truth;
	}

	public void SetInputType(GlobalVariables.ControlType type)
	{
		inputType = type;

		if (!isAI)
		{
			PlayerController ctr = GetComponent<PlayerController>();
			switch (type)
			{
				case GlobalVariables.ControlType.WASD:
					ctr.HorzString = "Horizontal";
					ctr.VertString = "Vertical";
					break;
				case GlobalVariables.ControlType.Arrows:
					ctr.HorzString = "Horizontal2";
					ctr.VertString = "Vertical2";
					break;
				case GlobalVariables.ControlType.Joystick1:
					ctr.HorzString = "Horizontal3";
					ctr.VertString = "Vertical3";
					break;
				case GlobalVariables.ControlType.Joystick2:
					ctr.HorzString = "Horizontal4";
					ctr.VertString = "Vertical4";
					break;
			}
		}
	}

}