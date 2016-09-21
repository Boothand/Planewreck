using UnityEngine;
using System.Collections;

public class OverviewCamera : MonoBehaviour
{
	[SerializeField]
	private Transform[] targets = new Transform[2];

	private Camera cam;

	void Start ()
	{
		cam = GetComponent<Camera>();
	}

	IEnumerator MoveCamToTarget(float distanceFromCenter)
	{
		Vector3 oldPos = transform.position;
		Vector3 newPos = transform.position;
		
		while (true)
		{
			Vector3 pos = transform.position;
			pos += cam.ViewportToWorldPoint((Vector3.right * distanceFromCenter));
			transform.position = pos;

			newPos = pos;
			newPos.z = oldPos.z;
			newPos.y = oldPos.y;

			break;
		}

		transform.position = oldPos;

		while (true)
		{
			transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * 2f);
			yield return new WaitForEndOfFrame();
		}
	}
	
	void Update ()
	{
		Vector3 camCenter = cam.ScreenToWorldPoint(new Vector2(0.5f, 0.5f));
		camCenter.z = 0;
		float safePadding = 0.3f;

		
		foreach (Transform target in targets)
		{
			Vector2 screenPos = cam.WorldToViewportPoint(target.position);
			float distanceFromCenter = screenPos.x - 0.5f;

			if (Mathf.Abs(distanceFromCenter) > safePadding)
			{
				StopAllCoroutines();
				//StartCoroutine(MoveCamToTarget(distanceFromCenter));
				break;
			}
		}
	}
}