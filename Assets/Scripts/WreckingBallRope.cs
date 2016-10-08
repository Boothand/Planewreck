//using System.Collections;
using UnityEngine;

public class WreckingBallRope : RopeManager
{
	private RopeVictim smasher;

	new void Start ()
	{
		base.Start();

		smasher = ropeObject.GetComponent<RopeVictim>();
	}

	public override void Connect()
	{
		base.Connect();

		if (ropeObject.GetComponent<CameraTarget>())
		{
			ropeObject.GetComponent<CameraTarget>().Trackable = true;
		}
	}

	public override void Break()
	{
		base.Break();

		if (ropeObject.GetComponent<CameraTarget>())
		{
			ropeObject.GetComponent<CameraTarget>().Trackable = false;
		}
	}

	new void Update ()
	{
		base.Update();

		if (smasher.colliding)
		{
			smasher.MultiplyVelocity(0.25f);
		}
	}
}