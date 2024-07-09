using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	[SerializeField]
	float speed;

	Rigidbody2D _rigidbody;

	Vector2 _direction;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
		_rigidbody.velocity = _direction * speed * Time.deltaTime;
	}
	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Enemy"))
		{
			CentinelController controller = other.gameObject.GetComponent<CentinelController>();

			float points = controller.GetPoints();


			//Incrementar puntos cuando se da en el centinel
			UIController.Instance.IncreaseScore(points);
			Destroy(other.gameObject);
		}
		else if (other.gameObject.CompareTag("Boss"))
		{
			BossController controller = other.gameObject.GetComponent<BossController>();
			controller.SetBulletHits();
			float points = controller.GetPoints();

			//Incrementar los puntos cuando se mata el boss
			UIController.Instance.IncreaseScore(points);
			controller.Die();
			Destroy(gameObject);
		}
	}
	public void SetDirection(Vector2 direction)
	{
		_direction = direction;
	}
}