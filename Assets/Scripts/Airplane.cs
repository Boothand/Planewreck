using UnityEngine;

[RequireComponent(typeof(Flightmanager))]
public class Airplane : MonoBehaviour
{
	Flightmanager flightManager;

	void Start ()
	{
		flightManager = GetComponent<Flightmanager>();
	}
	
	void Update ()
	{
		
	}
}