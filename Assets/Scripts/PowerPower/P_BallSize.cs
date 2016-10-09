using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_BallSize : PowerBase
{

	float startSize;
	Transform smasher;

	public bool destroyMe = false;

	float lerpSpeed = 2f;


	protected override void Update()
	{
		base.Update();
		if (destroyMe)
		{
			Stop();
			destroyMe = false;
		}
	}

	protected override void Start()
	{
		base.Start();
	}

	protected override void OnPowerStart()
	{
		base.OnPowerStart();
		startSize = airplane.getSmasher().localScale.x;
		smasher = airplane.getSmasher();
		StartCoroutine(TweenSize(10f));
	}

	protected override void OnPowerEnd()
	{
		base.OnPowerEnd();
		StartCoroutine(TweenSize(startSize));
	}

	IEnumerator TweenSize(float size)
	{
		print("tween start");

		print("Size" + Mathf.Abs(smasher.localScale.x - size));
		while (Mathf.Abs(smasher.localScale.x-size) > 0.01f )
		{
			smasher.localScale = Vector3.Lerp(smasher.localScale, new Vector3(size, size, size), Time.deltaTime * lerpSpeed);
			yield return new WaitForEndOfFrame();
		}
		smasher.localScale = new Vector3(size, size, size);
		print("tween end");
	}



	IEnumerator KillMeNow()
	{
		print("kill start");
		yield return StartCoroutine(TweenSize(startSize));
		print("kill end");
		Destroy(this);
	}
	public override void Stop()
	{
		StopAllCoroutines();
		StartCoroutine(KillMeNow());
	}
}
