using System.Collections;
using UnityEngine;

public class P_BallSize : PowerBase
{
	float startSize;
	WreckingBall smasher;

	[SerializeField]
	float lerpSpeed = 2f;

	[SerializeField]
	float ballSize = 10f;


	protected override void Start()
	{
		base.Start();
	}

	protected override void OnPowerStart()
	{
		base.OnPowerStart();

		smasher = airplane.GetSmasher();
		startSize = smasher.transform.localScale.x;

		StartCoroutine(TweenSize(ballSize));
	}

	IEnumerator TweenSize(float size)
	{
		while (Mathf.Abs(smasher.transform.localScale.x-size) > 0.01f )
		{
			smasher.transform.localScale = Vector3.Lerp(smasher.transform.localScale, new Vector3(size, size, size), Time.deltaTime * lerpSpeed);
			yield return new WaitForEndOfFrame();
		}

		smasher.transform.localScale = new Vector3(size, size, size);
	}

	protected override IEnumerator EndPowerRoutine()
	{
		yield return StartCoroutine(TweenSize(startSize));
	}
}