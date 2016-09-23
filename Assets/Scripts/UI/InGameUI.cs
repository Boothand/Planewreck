using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
	[SerializeField]
	private Text countDownText;

	[SerializeField]
	private Transform[] scoreUIObjects = new Transform[4];

	private bool drawScores;

	private int scoreIndex;
	private string scoreName;

	void Start ()
	{
		DisableMiddleScreenText();

		foreach (Transform obj in scoreUIObjects)
		{
			obj.gameObject.SetActive(false);
		}
	}

	public void SetMiddleScreenText(string text, bool bestFit = false)
	{
		countDownText.enabled = true;
		
		countDownText.resizeTextForBestFit = bestFit;

		countDownText.text = text;
	}

	public void DisableMiddleScreenText()
	{
		countDownText.enabled = false;
	}

	public void DrawPlayerScore(int index, string name, int score)
	{
		Transform scoreObj = scoreUIObjects[index];

		scoreObj.gameObject.SetActive(true);

		Text scoreText = scoreObj.FindChild("Score Text").GetComponent<Text>();
		Text nameText = scoreObj.FindChild("Name Text").GetComponent<Text>();

		scoreText.text = score.ToString();
		nameText.text = name;
	}

	void Update ()
	{
		
	}
}