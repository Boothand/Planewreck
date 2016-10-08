using UnityEngine;

static public class GlobalVariables
{
    static public int numberOfPlayers;
    static public ControlType[] inputs = new ControlType[4];
	static public string[] names = new string[4];
	static public Color[] colors = new Color[4];

	public enum ControlType
	{
		Noone, Arrows, WASD, Joystick1, Joystick2
	}
}