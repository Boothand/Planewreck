using UnityEngine;

[RequireComponent(typeof(Flightmanager))]
[RequireComponent(typeof(HealthManager))]
public class PlayerProperties : MonoBehaviour
{
	public int wins;
	Flightmanager flight;
	HealthManager health;
	InputManager input;

	[SerializeField]
	Color color = Color.white;
	string playerName = "Player";

	bool isAI;

	[SerializeField]
	GameObject meshPrefab;
	GameObject meshInstance;

	[SerializeField]
	Transform smasher;

	Vector3 startPosition;

	Vector3 smasherStartOffset;

	[SerializeField]
	PlayerControllers.ControllType inputType = PlayerControllers.ControllType.WASD;

	public Flightmanager Flight { get { return flight; } }
	public HealthManager Health { get { return health; } }
	public string PlayerName { get { return playerName; } }
	public Transform MeshInstance { get { return meshInstance.transform; } }

	public bool IsAI
	{
		get { return isAI; }
		set { isAI = value; }
	}


	void Awake()
	{
		meshInstance = transform.root.FindChild("Mesh").gameObject;
	}

	void Start()
	{
		flight = GetComponent<Flightmanager>();
		health = GetComponent<HealthManager>();
		input = GetComponent<InputManager>();

		startPosition = transform.position;
		smasherStartOffset = smasher.transform.position - meshInstance.transform.position;

		SetInputType(inputType);

		SetColor();
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

		MeshInstance.transform.position = transform.position;
		MeshInstance.transform.rotation = transform.rotation;
	}

	public void SetProperties(int index) //used once when object is first Instantiated in GameManager
	{
		playerName = PlayerControllers.names[index]; // set player name

		if (PlayerControllers.inputs[index] != PlayerControllers.ControllType.Noone)
		{
			inputType = PlayerControllers.inputs[index]; // set input type
		}

		if (PlayerControllers.colors[index] != null)
		{
			color = PlayerControllers.colors[index]; //set color
		}
	}

	public void ResetSmasherPosition()
	{
		//smasher.position = smasherStartOffset;
		smasher.position = meshInstance.transform.position + smasherStartOffset;
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

		SetColor();
	}

	void SetColor()
	{
		if (color != Color.white)
		foreach (Transform child in meshInstance.transform)
		{
			child.GetComponent<Renderer>().material.color = color;
		}
	}

	public void EnableInput()
	{
		input.enabled = true;
	}

	public void DisableInput()
	{
		input.enabled = false;
	}

	public void FreezePosition()
	{
		flight.enabled = false;
	}

	public void UnFreezePosition()
	{
		flight.enabled = true;
	}

	public void SetInputType(PlayerControllers.ControllType type)
	{
		inputType = type;

		if (!isAI)
		{
			PlayerController ctr = GetComponent<PlayerController>();
			switch (type)
			{
				case PlayerControllers.ControllType.WASD:
					ctr.HorzString = "Horizontal";
					ctr.VertString = "Vertical";
					break;
				case PlayerControllers.ControllType.Arrows:
					ctr.HorzString = "Horizontal2";
					ctr.VertString = "Vertical2";
					break;
				case PlayerControllers.ControllType.Joystick1:
					ctr.HorzString = "Horizontal3";
					ctr.VertString = "Vertical3";
					break;
				case PlayerControllers.ControllType.Joystick2:
					ctr.HorzString = "Horizontal4";
					ctr.VertString = "Vertical4";
					break;
			}
		}
	}
	
}