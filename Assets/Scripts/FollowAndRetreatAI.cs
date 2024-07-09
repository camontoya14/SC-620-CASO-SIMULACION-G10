using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowAndRetreatAI : MonoBehaviour
{
	[SerializeField]
	Transform target;

	[SerializeField]
	float speed;

	[SerializeField]
	float stopDistance;


	[SerializeField]
	float bossFiretime = 5.0f;

	[SerializeField]
	float retreatDistance;

	[SerializeField]
	GameObject bulletPrefab;

	[SerializeField]
	float fireTimeout;

	[SerializeField]
	float bulletLifeTime;


	[SerializeField]
	string fireSoundSFX;

	[Header("Fire")]
	[SerializeField]
	Transform firePoint;

	bool isAlive = true;

	Rigidbody2D _rigidbody;

	float _fireTimer;

	bool _canShoot;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();

	}

	private void FixedUpdate()
	{

		if (!isAlive)
		{
			return;
		}

		float distance = Vector2.Distance(_rigidbody.position, target.position);

		if (distance > stopDistance)
		{
			_rigidbody.position =
				Vector2.MoveTowards(_rigidbody.position, target.position, speed * Time.deltaTime);

		}
		else if (distance < retreatDistance)
		{
			_rigidbody.position =
				Vector2.MoveTowards(_rigidbody.position, target.position, -speed * Time.deltaTime);

		}
		else if (distance < stopDistance && distance > retreatDistance)
		{
			_rigidbody.position = this._rigidbody.position;
		}

		transform.right = target.position - transform.position;


		HandleFire();

	}

	//FIRE TO PLAYER => 40 PUNTOS-
	private void HandleFire()
	{
		if (!isAlive)
		{
			return;
		}

		_fireTimer -= Time.deltaTime;

		if (!_canShoot)
		{
			if (_fireTimer <= 0.0f)
			{
				_canShoot = true;
				_fireTimer = bossFiretime;
			}
			return;
		}

		if (_fireTimer > 0.0f)
		{
			return;
		}

		GameObject bullet =
			Instantiate(bulletPrefab, firePoint.position, transform.rotation);

		Vector2 direction = (target.position - firePoint.position).normalized;

		BossBulletController controller = bullet.GetComponent<BossBulletController>();
		controller.SetDirection(direction);

		Destroy(bullet, bulletLifeTime);
		_fireTimer = bossFiretime;
		SoundManager.Instance.PlaySFX(fireSoundSFX);

	}
}



