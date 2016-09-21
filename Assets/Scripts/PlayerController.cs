using UnityEngine;

public class PlayerController : InputManager
{
	private string accelString = "Accelerate";
	private string decelString = "Decelerate";

	private string tiltString = "Tilt";

	void Start ()
	{
		
	}
	
	void Update ()
	{
		accelerate = Input.GetAxisRaw(accelString);
		decelerate = Input.GetAxisRaw(decelString);
		tilt = Input.GetAxisRaw(tiltString);
	}
}