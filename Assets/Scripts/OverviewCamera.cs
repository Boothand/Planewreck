using UnityEngine;
using System.Collections;

public class OverviewCamera : MonoBehaviour
{
	private CameraTarget[] cameraTargets;
	private Camera cam;

	[SerializeField]
	private float lerpSpeed = 4f;

	[SerializeField]
	private float maxDistance = -100f;

	[SerializeField]
	private float minDistance = -30f;

	private int trackableObjectCount;
	private Vector3 startPos;

	private bool trackObjects;

	public Vector3 StartPosition { get { return startPos; } }

	void Start ()
	{
		cam = GetComponent<Camera>();

		cameraTargets = GameObject.FindObjectsOfType<CameraTarget>();

		startPos = transform.position;
	}

	IEnumerator ZoomOnTargetRoutine(Vector3 targetPos, float zoomTime, float distance)
	{
		trackObjects = false;
		Vector3 startPos = transform.position;
		float timer = 0f;

		while (timer < zoomTime)
		{
			timer += Time.deltaTime;

			transform.position = Vector3.Lerp(startPos, targetPos - (Vector3.forward * distance), timer);

			yield return new WaitForEndOfFrame();
		}

		trackObjects = true;
	}

	public void ZoomOnTarget(Transform target, float zoomTime, float distance)
	{
		StartCoroutine(ZoomOnTargetRoutine(target.position, zoomTime, distance));
	}

	public void SetCameraTargetPosition(Vector3 targetPos, float travelTime)
	{
		StartCoroutine(ZoomOnTargetRoutine(targetPos, travelTime, 0f));
	}

	void TrackSceneObjects()
	{
		Vector2 lowerLeft = Vector2.zero;
		Vector2 upperRight = Vector2.zero;

		bool isSet = false;
		trackableObjectCount = 0;

		foreach (CameraTarget target in cameraTargets)
		{
			if (!target.Trackable)
			{
				continue;
			}
			else
			{
				trackableObjectCount++;
			}

			if (!isSet)
			{
				lowerLeft = target.transform.position;
				upperRight = target.transform.position;

				isSet = true;
			}
			else
			{
				if (target.transform.position.y < lowerLeft.y)
				{
					lowerLeft.y = target.transform.position.y;
				}

				if (target.transform.position.x < lowerLeft.x)
				{
					lowerLeft.x = target.transform.position.x;
				}

				if (target.transform.position.y > upperRight.y)
				{
					upperRight.y = target.transform.position.y;
				}

				if (target.transform.position.x > upperRight.x)
				{
					upperRight.x = target.transform.position.x;
				}
			}
		}

		Vector3 camPos = (upperRight + lowerLeft) / 2;

		float zDistance = Vector2.Distance(upperRight, lowerLeft);
		camPos.z = -zDistance;
		camPos.z = Mathf.Clamp(camPos.z, maxDistance, minDistance);

		transform.position = Vector3.Lerp(transform.position, camPos, Time.deltaTime * lerpSpeed);

#if false
		Debug.DrawLine(Vector3.zero, new Vector3(camPos.x, camPos.y, 0f), Color.red);

		Debug.DrawLine(lowerLeft, upperRight);
		Debug.DrawLine(new Vector3(lowerLeft.x, upperRight.y), new Vector3(upperRight.x, lowerLeft.y));
#endif
	}
	
	void FixedUpdate ()
	{
		if (trackObjects)
		{
			TrackSceneObjects();
		}

		//if (trackableObjectCount == 0)
		//{
		//	Vector3 endPos = startPos;
		//	endPos.z = maxDistance;
		//	transform.position = endPos;
		//}		
	}
}