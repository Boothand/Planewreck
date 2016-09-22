using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {
	[SerializeField]
	private GameObject mesh;

	private int meshCount;

	Transform[] childs;
	private bool dead = false;

	void Start()
	{
		meshCount = mesh.transform.childCount;
		print("Start" + meshCount);
		childs = new Transform[meshCount];

		int ii = 0;
		foreach (Transform child in mesh.transform)
		{
			childs[ii] = child;
			ii++;
		}
	}

	void Die(Vector3 velocity)
	{
		if (!dead)
		{
			dead = true;

			print(velocity);
			print("DIES");
			GetComponent<Collider>().enabled = false;

			Vector3 combinedVelocity = Vector3.Normalize(GetComponent<Flightmanager>().Velocity + velocity);

			for (int i = 0; i < meshCount; i++)
			{
				Transform child = childs[i];

				child.transform.SetParent(null);
				Rigidbody rb = child.gameObject.AddComponent<Rigidbody>();
				rb.useGravity = true;


				rb.AddForce(combinedVelocity * Random.Range(10, 30), ForceMode.Impulse);
			}

			GetComponent<Flightmanager>().enabled = false;
			GetComponent<InputManager>().enabled = false;
			GetComponent<Rigidbody>().isKinematic = true;

			GetComponentInChildren<RopeManager>().Break();
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.transform.GetComponent<RopeVictim>())
		{
			Die(col.transform.GetComponent<RopeVictim>().Velocity);
		}

		if (col.transform.GetComponent<DestructableStatic>())
			Die(Vector3.zero);
	}
}
