using System.Collections;
using UnityEngine;

public class P_BallGravity : PowerBase
{
	WreckingBall ball;

	[SerializeField]
	Vector3 gravity = new Vector3(0f, 100f, 0f);

	Vector3 oldGravity;

	protected override void Awake()
	{
		base.Awake();

		ball = airplane.GetSmasher();
		oldGravity = ball.gravity;
	}

	protected override void Start()
	{
		base.Start();		
	}

	protected override void OnPowerStart()
	{
		base.OnPowerStart();

		ball.gravity = gravity;
	}

	protected override void OnPowerEnd()
	{
		base.OnPowerEnd();
		
		ball.gravity = oldGravity;
	}
}