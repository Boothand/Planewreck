using UnityEngine;

static public class PlayerControllers
{
    static public int numberOfPlayers;
    static public ControllType[] inputs = new ControllType[4];
	static public string[] names = new string[4];
	static public Color[] colors = new Color[4];

	public enum ControllType
	{
		Noone, Arrows, WASD, Joystick1, Joystick2
	}
}
