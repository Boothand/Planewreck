using UnityEngine;

public class FaceObject : MonoBehaviour
{
	[SerializeField]
	protected Transform target;

	[SerializeField]
	protected bool flipForward = false;

	[SerializeField]
	protected Transform followObject;

	[SerializeField]
	protected Vector3 offset;

	protected virtual void Start()
	{

	}

	protected virtual void LateUpdate()
	{
		if (flipForward)
			transform.forward = transform.position - target.position;
		else
			transform.forward = target.position - transform.position;

		if (followObject)
		{
			transform.position = followObject.position + offset;
		}

	}
}
