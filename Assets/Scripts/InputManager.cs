using UnityEngine;

public class InputManager : MonoBehaviour
{
	protected float accelerate;
	protected float decelerate;
	protected float tilt;

	public float Accelerate { get { return accelerate; } }
	public float Decelerate { get { return decelerate; } }
	public float Tilt { get { return tilt; } }
}