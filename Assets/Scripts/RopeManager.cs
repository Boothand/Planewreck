using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RopeManager : MonoBehaviour
{
	[SerializeField]
	private int ropeSegments = 2;

	[SerializeField]
	private Transform target;

	[SerializeField]
	[Range(0f, 2f)]
	private float ropeWidth = 0.4f;

	LineRenderer lineRend;

	void Start ()
	{
		lineRend = GetComponent<LineRenderer>();
		lineRend.SetVertexCount(ropeSegments);
	}
	
	void Update ()
	{
		//lineRend.SetWidth(ropeWidth, ropeWidth);
		lineRend.SetPosition(0, transform.position);
		lineRend.SetPosition(1, target.position);

		float distance = Vector3.Distance(transform.position, target.position);
		float tiss = (9 - distance) / 10f;
		tiss = Mathf.Clamp(tiss, 0.2f, 0.2f + distance);
		print(tiss);
		lineRend.SetWidth(tiss, tiss);
	}
}