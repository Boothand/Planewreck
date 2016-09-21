using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
	public Rigidbody player;
	public Text velocityText;
	
	void Update ()
	{
		velocityText.text = player.velocity.ToString();
	}
}
