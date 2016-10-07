using UnityEngine;

public class FaceCamera : FaceObject
{
	protected override void Start()
	{
		base.Start();
		if (!target)
			target = Camera.main.transform;
	}

	protected override void LateUpdate()
	{
		base.LateUpdate();
	}
}
