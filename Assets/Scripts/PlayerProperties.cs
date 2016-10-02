//using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Flightmanager))]
[RequireComponent(typeof(HealthManager))]
public class PlayerProperties : MonoBehaviour
{
	private int wins;
	private Flightmanager flight;
	private HealthManager health;
	private InputManager input;

	private string playerName = "Player";

	private bool isAI;

	[SerializeField]
	private GameObject meshPrefab;
	private GameObject meshInstance;

	[SerializeField]
	private Transform smasher;

	private Vector3 startPosition;

	private Vector3 smasherStartPosition;

	[SerializeField]
	private PlayerControllers.ControllType inputType = PlayerControllers.ControllType.WASD;

	public Flightmanager Flight { get { return flight; } }
	public HealthManager Health { get { return health; } }
	public string PlayerName { get { return playerName; } }
	public Transform MeshInstance { get { return meshInstance.transform; } }

	public bool IsAI
	{
		get { return isAI; }
		set { isAI = value; }
	}

	public int Wins
	{
		get { return wins; }
		set { wins = value; }
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
		smasherStartPosition = smasher.transform.position;

		SetInputType(inputType);
	}

	public void ResetPosition()
	{
		transform.position = startPosition;
		transform.forward = flight.StartDirection;

		meshInstance.transform.position = transform.position;
		meshInstance.transform.rotation = transform.rotation;
	}

	public void ResetSmasherPosition()
	{
		smasher.position = smasherStartPosition;
	}

	public void Revive()
	{
		health.Dead = false;

		Destroy(meshInstance);
		meshInstance = Instantiate(meshPrefab, transform.root) as GameObject;

		meshInstance.transform.rotation = meshPrefab.transform.rotation;
		meshInstance.transform.localPosition = Vector3.zero;
		meshInstance.transform.localScale = meshPrefab.transform.localScale;

		health.EnableComponents();
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
		
	void Update ()
	{
		
	}
}