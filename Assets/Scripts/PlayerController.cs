using UnityEngine;

public class PlayerController : InputManager
{
	[SerializeField]
	private string horzString = "Horizontal";
	[SerializeField]
	private string vertString = "Vertical";

	public string HorzString { set { horzString = value; } }
	public string VertString { set { vertString = value; } }

	void Start ()
	{
		
	}
	
	void Update ()
	{
		horizontal = Input.GetAxisRaw(horzString);
		vertical = Input.GetAxisRaw(vertString);
	}
}