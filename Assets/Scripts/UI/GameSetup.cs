//using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameSetup : MonoBehaviour
{
	private int numberOfPlayers;
	
	private List<GameObject> menus = new List<GameObject>();

	[SerializeField]
	private Scene defaultGameScene;

	void Start ()
	{
		foreach (Transform child in transform)
		{
			menus.Add(child.gameObject);
		}
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
		int sceneNum = defaultGameScene.buildIndex;
		SceneManager.LoadScene(sceneNum);
	}

	public void DisplayMenu(GameObject menu)
	{
		foreach (GameObject obj in menus)
		{
			obj.SetActive(false);
		}

		menu.SetActive(true);
	}

	void Update ()
	{
		
	}
}