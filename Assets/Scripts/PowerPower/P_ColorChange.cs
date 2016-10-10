using System.Collections;
using UnityEngine;

public class P_ColorChange : PowerBase
{
	[SerializeField] Color color = Color.green;
	float lerpSpeed = 1f;

	protected override void Start()
	{
		base.Start();
	}

	protected override void OnPowerStart()
	{
		StartCoroutine(TweenColor(airplane.color, color));
		base.OnPowerStart();
	}

	IEnumerator TweenColor(Color oldColor, Color newColor)
	{
		float timer = 0f;

		while (timer < 1f)
		{
			Color lerpColor = Color.Lerp(oldColor, newColor, timer);
			timer += Time.deltaTime * lerpSpeed;

			airplane.SetColor(lerpColor);

			yield return new WaitForEndOfFrame();
		}
	}

	protected override IEnumerator EndPowerRoutine()
	{
		yield return StartCoroutine(TweenColor(color, airplane.color));
	}
}