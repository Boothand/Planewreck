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

	[SerializeField]
	private GameObject meshPrefab;
	private GameObject meshInstance;

	[SerializeField]
	private Transform smasher;

	private Vector3 startPosition;

	private Vector3 smasherStartPosition;

	public Flightmanager Flight { get { return flight; } }
	public HealthManager Health { get { return health; } }
	public string PlayerName { get { return playerName; } }
	public Transform MeshInstance { get { return meshInstance.transform; } }

	public int Wins
	{
		get { return wins; }
		set { wins = value; }
	}

	void Start()
	{
		flight = GetComponent<Flightmanager>();
		health = GetComponent<HealthManager>();
		input = GetComponent<InputManager>();

		meshInstance = transform.root.FindChild("Mesh").gameObject;

		startPosition = transform.position;
		smasherStartPosition = smasher.transform.position;
	}

	public void ResetPosition()
	{
		transform.position = startPosition;
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

		MeshInstance.transform.localPosition = Vector3.zero;
		meshInstance.transform.localScale = meshPrefab.transform.localScale;
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
		
	void Update ()
	{
		
	}
}