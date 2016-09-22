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


	
	new void Update ()
	{
		base.Update();

		if (smasher.Colliding)
		{
			print("OK");
			ropeObjectVelocity *= 0.25f;
		}

		smasher.Velocity = ropeObjectVelocity;
	}
}