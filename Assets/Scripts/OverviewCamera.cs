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

	[SerializeField]
	private float minHeight = -32;

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

	public void SetTracking(bool enable)
	{
		trackObjects = enable;
	}

	IEnumerator ZoomOnTargetRoutine(Transform target, float zoomTime, float distance, float extraTime = 0f)
	{
		trackObjects = false;
		Vector3 startPos = transform.position;
		
		float timer = 0f;

		while (timer < zoomTime)
		{
			timer += Time.deltaTime;

			Vector3 targetPosition = target.position;
			targetPosition.z -= distance;

			transform.position = Vector3.Lerp(startPos, target.position, timer);

			yield return new WaitForEndOfFrame();
		}

		print("Here");
		transform.position = target.position - Vector3.forward * distance;
		yield return new WaitForSeconds(extraTime);

		trackObjects = true;
	}

	IEnumerator GoToPosRoutine(Vector3 pos, float duration)
	{
		trackObjects = false;
		Vector3 startPos = transform.position;
		float timer = 0f;

		while (timer < duration)
		{
			timer += Time.deltaTime;

			transform.position = Vector3.Lerp(startPos, pos, timer);

			yield return new WaitForEndOfFrame();
		}

		transform.position = pos;

		trackObjects = true;
	}

	public void ZoomOnTarget(Transform target, float zoomTime, float distance, float extraTime = 0f)
	{
		StartCoroutine(ZoomOnTargetRoutine(target, zoomTime, distance, extraTime));
	}

	public void SetCameraTargetPosition(Vector3 targetPos, float travelTime)
	{
		StartCoroutine(GoToPosRoutine(targetPos, travelTime));
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

		if (camPos.y < minHeight)
		{
			camPos.y = minHeight;
		}

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