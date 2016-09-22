//using System.Collections;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
	private bool trackable = true;

	public bool Trackable { get { return trackable; } set { trackable = value; } }

	void Start()
	{
		print(trackable);
	}
}