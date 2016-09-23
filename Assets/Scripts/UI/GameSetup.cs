//using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameSetup : MonoBehaviour
{
	private int numberOfPlayers;

	[SerializeField]
	GameObject firstMenu;
	
	private List<GameObject> menus = new List<GameObject>();

	void Start ()
	{
		foreach (Transform child in transform)
		{
			menus.Add(child.gameObject);
		}

		DisplayMenu(firstMenu);
	}

	public void SetNumberOfPlayers(Slider slider)
	{
		numberOfPlayers = Mathf.FloorToInt(slider.value);
	}

	public void SetPlayerNumText(Text displayText)
	{
		displayText.text = numberOfPlayers.ToString();
	}

	public void StartGame()
	{
		//Set up game manager's properties.
		if (StaticControll.numberOfPlayers > 1)
		SceneManager.LoadScene(1);
	}

	public void DisplayMenu(GameObject menu)
	{
		foreach (GameObject obj in menus)
		{
			obj.SetActive(false);
		}

		menu.SetActive(true);
	}
}