using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
	// 5 PLayer bullets destroy boss = 10 pts min 2:07:40
	// ya se vio como hacer acumuladores con los puntos aqui se puede hacer algo parecido
	// 1 sola bala del boss nos destruye

	[SerializeField]
	float points;

	[Header("Animations")]
	[SerializeField]
	float dieTimeout;


	[SerializeField]
	float dieWaitTime;
	int _bulletsHits;



	private void Update()
	{

	}
	public int GetBulletsHits() { return _bulletsHits; }

	public void SetBulletHits()
	{
		_bulletsHits++;

	}

	public void Die()
	{
		if (_bulletsHits >= 5)
		{
			Collider2D collider = GetComponent<Collider2D>();
			collider.enabled = false;

			FollowAndRetreatAI controller = collider.GetComponent<FollowAndRetreatAI>();
			controller.enabled = false;


			StartCoroutine(DieCoroutine());

		}


	}
	// GET de los puntos del boss
	public float GetPoints() { return points; }
	private IEnumerator DieCoroutine()
	{
		SpriteRenderer renderer = GetComponentInChildren<SpriteRenderer>();
		Color color = renderer.color;


		while (color.a > 0.0F)
		{
			color.a -= 0.1F;
			renderer.color = color;
			yield return new WaitForSeconds(dieTimeout);
		}

		yield return new WaitForSeconds(dieWaitTime);

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


	}
}
