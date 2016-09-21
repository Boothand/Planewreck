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
		lineRend.SetPosition(0, transform.position);
		lineRend.SetPosition(1, target.position);

		float distance = Vector3.Distance(transform.position, target.position);
		float width = (9 - distance) / 10f;
		width = Mathf.Clamp(width, ropeWidth, ropeWidth + distance);
		lineRend.SetWidth(width, width);
	}
}