using System.Collections;
using UnityEngine;

public class Crate : MonoBehaviour
{
	[SerializeField]
	PowerBase power;

	bool taken;

	void OnTriggerEnter(Collider col)
	{
		if (taken)
		{
			return;
		}


		if (col.GetComponent<AirplaneManager>())
		{
			taken = true;

			AirplaneManager airplane = col.GetComponent<AirplaneManager>();
			GameObject instance = Instantiate(power.gameObject, airplane.transform);

			StartCoroutine(GetTaken(col.transform));
		}
	}

	IEnumerator GetTaken(Transform target)
	{
		Material mat = GetComponent<Renderer>().material;
		Color oldCol = mat.color;
		float timer = 0f;
		ParticleSystem particles = GetComponentInChildren<ParticleSystem>();
		ParticleSystem.Particle[] particle = new ParticleSystem.Particle[7];

		particles.Emit(7);

		while (timer < 1f)
		{
			particles.GetParticles(particle);

			if (timer > 0.4f)
			{
				for (int i = 0; i < particle.Length; i++)
				{
					particle[i].position = Vector3.MoveTowards(particle[i].position, transform.position, timer + 0.4f);

				}

				particles.SetParticles(particle, 7);
			}

			mat.color = Color.Lerp(oldCol, Color.white, timer);
			timer += Time.deltaTime * 1f;
			transform.localScale *= 0.95f;
			transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * 10f);
			transform.Rotate(Vector3.forward, Time.deltaTime * 400, Space.World);

			yield return new WaitForEndOfFrame();
		}

		Destroy(gameObject);
	}

	void Update()
	{
		transform.Rotate(Vector3.forward, Time.deltaTime * 40f);

		transform.position += Vector3.up * Mathf.Sin(Time.time * 5f) * Time.deltaTime;
	}
}
