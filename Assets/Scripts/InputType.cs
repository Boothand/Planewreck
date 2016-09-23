using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputType : MonoBehaviour {
	public enum Type
	{
		Noone, Arrows, WASD, Joystick1, Joystick2
	}

	[SerializeField] private Type type;

	public Type getType
	{
		get {
			return type;
		}
	}
}
