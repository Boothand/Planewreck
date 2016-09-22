using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
	[SerializeField]
	private Text countDownText;

	void Start ()
	{
		DisableMiddleScreenText();
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

	void Update ()
	{
		
	}
}