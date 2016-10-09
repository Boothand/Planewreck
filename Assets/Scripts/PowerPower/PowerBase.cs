using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerBase : MonoBehaviour
{
	protected bool hasDuration = true;
	protected float duration = 5f;

	protected AirplaneManager airplane;


	protected virtual void Start()
	{
		airplane = GetComponent<AirplaneManager>();
		StartCoroutine(PowerRoutine());
	}

	protected virtual void Update()
	{

	}

	protected virtual void OnPowerStart()
	{

	}

	protected virtual void OnPowerEnd()
	{
		
	}

	protected virtual IEnumerator PowerRoutine()
	{
		OnPowerStart();
		if (hasDuration)
			yield return new WaitForSeconds(duration);

		OnPowerEnd();
		Stop();
		Destroy(this);
	}

	public abstract void Stop();
}
