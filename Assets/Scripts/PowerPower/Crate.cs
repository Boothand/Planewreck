using System.Collections;
using UnityEngine;

public class Crate : MonoBehaviour
{
	[SerializeField]
	PowerBase power;

	void OnTriggerEnter(Collider col)
	{
		if (col.GetComponent<AirplaneManager>())
		{
			AirplaneManager airplane = col.GetComponent<AirplaneManager>();
			GameObject instance = Instantiate(power.gameObject, airplane.transform);

			Destroy(gameObject);
		}
	}
}
