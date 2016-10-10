using System.Collections;
using UnityEngine;

public abstract class PowerBase : MonoBehaviour
{
	protected AirplaneManager airplane;

	[SerializeField]
	protected bool hasDuration = true;

	[SerializeField]
	protected float duration = 5f;

	protected virtual void Awake()
	{
		airplane = transform.parent.GetComponent<AirplaneManager>();
	}

	protected virtual void Start()
	{
		StartCoroutine(PowerRoutine());
	}	

	IEnumerator PowerRoutine()
	{
		OnPowerStart();

		if (hasDuration)
			yield return new WaitForSeconds(duration);

		OnPowerEnd();

		yield return StartCoroutine(EndPowerRoutine());
		Destroy(gameObject);
	}

	protected virtual void OnPowerStart()
	{

	}

	protected virtual void OnPowerEnd()
	{

	}

	protected virtual IEnumerator EndPowerRoutine()
	{
		yield return null;
	}

	public void Stop()
	{
		StopAllCoroutines();
		StartCoroutine(EndApruptlyRoutine());
	}

	IEnumerator EndApruptlyRoutine()
	{
		yield return StartCoroutine(EndPowerRoutine());

		Destroy(gameObject);
	}
}