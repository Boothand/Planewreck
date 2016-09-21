using UnityEngine;

public class PlayerController : InputManager
{
	[SerializeField]
	private string horzString = "Horizontal";
	[SerializeField]
	private string vertString = "Vertical";

	void Start ()
	{
		
	}
	
	void Update ()
	{
		horizontal = Input.GetAxisRaw(horzString);
		vertical = Input.GetAxisRaw(vertString);
	}
}