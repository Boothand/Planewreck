//using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameSetup : MonoBehaviour
{
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

	public void StartGame()
	{
		if (GlobalVariables.numberOfPlayers > 1)
		{
			SceneManager.LoadScene(1);
		}
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