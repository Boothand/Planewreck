using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
	private GameObject mesh;

	private PlayerProperties properties;

	private int meshCount;

	Transform[] childs;
	private bool dead = false;

	public bool Dead { get { return dead; } set { dead = value; } }

	void Start()
	{
		properties = GetComponent<PlayerProperties>();

		mesh = properties.MeshInstance.gameObject;

		meshCount = mesh.transform.childCount;


		childs = new Transform[meshCount];

		int i = 0;
		foreach (Transform child in mesh.transform)
		{
			childs[i] = child;
			i++;
		}
	}

	void Die(Vector3 velocity)
	{
		dead = true;

		if (GetComponent<CameraTarget>())
		{
			GetComponent<CameraTarget>().Trackable = false;
		}

		Vector3 combinedVelocity = Vector3.Normalize(GetComponent<Flightmanager>().Velocity + velocity);

		for (int i = 0; i < meshCount; i++)
		{
			Transform child = childs[i];

			child.transform.SetParent(null);

			if (!child.gameObject.GetComponent<Rigidbody>())	//Unity, why?
			{
				Rigidbody rb = child.gameObject.AddComponent<Rigidbody>();
				rb.useGravity = true;


				rb.AddForce(combinedVelocity * Random.Range(10, 30), ForceMode.Impulse);
			}
		}

		GetComponent<Collider>().enabled = false;
		GetComponent<Flightmanager>().enabled = false;
		GetComponent<InputManager>().enabled = false;
		GetComponent<Rigidbody>().isKinematic = true;

		GetComponentInChildren<RopeManager>().Break();
	}

	public void EnableComponents()
	{
		GetComponent<Flightmanager>().enabled = true;
		GetComponent<InputManager>().enabled = true;
		GetComponent<Rigidbody>().isKinematic = false;
		GetComponent<Collider>().enabled = true;

		GetComponentInChildren<RopeManager>().Connect();
	}

	void OnCollisionEnter(Collision col)
	{
		if (dead)
		{
			return;
		}

		if (col.transform.GetComponent<RopeVictim>())
		{
			Die(col.transform.GetComponent<RopeVictim>().Velocity);
		}

		if (col.transform.GetComponent<DestructableStatic>())
			Die(Vector3.zero);
	}

	void Update()
	{
		if (!mesh)
		{
			mesh = properties.MeshInstance.gameObject;
			
			int i = 0;
			foreach (Transform child in mesh.transform)
			{
				childs[i] = child;
				i++;
			}
		}
	}
}
