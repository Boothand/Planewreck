using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
	private GameObject mesh;

	private AirplaneManager properties;

	private int meshCount;

	Transform[] childs;
	private bool dead = false;

	public bool Dead { get { return dead; } set { dead = value; } }

	void Start()
	{
		properties = GetComponent<AirplaneManager>();

		mesh = properties.meshInstance;

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

		Vector3 combinedVelocity = GetComponent<Flightmanager>().velocity + velocity;

		for (int i = 0; i < meshCount; i++)
		{
			Transform child = childs[i];

			child.transform.SetParent(null);

			if (!child.gameObject.GetComponent<Rigidbody>())	//Unity, why?
			{
				Rigidbody rb = child.gameObject.AddComponent<Rigidbody>();
				rb.useGravity = true;

				float rMin = -1f;
				float rMax = 3f;

				Vector3 newVelocity = new Vector3(combinedVelocity.x * Random.Range(rMin, rMax), combinedVelocity.y * Random.Range(rMin, rMax), combinedVelocity.z * Random.Range(rMin, rMax));
				rb.AddForce(newVelocity, ForceMode.Impulse);

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
		if (!mesh)
		{
			if (properties.meshInstance)
			{
				mesh = properties.meshInstance;

				int i = 0;
				foreach (Transform child in mesh.transform)
				{
					childs[i] = child;
					i++;
				}
			}
		}

		if (dead)
		{
			return;
		}

		if (col.transform.GetComponent<WreckingBall>() && col.transform.root != transform.root)
		{
			Die(col.transform.GetComponent<WreckingBall>().getVelocity);
		}

		if (col.transform.GetComponent<DestructibleBuilding>())
			Die(Vector3.zero);
	}
}
