using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour
{
	public enum WeaponStatus
	{
		IDLE,
		FIRE,
		RELOAD
	}

	public float reloadTime = 5f;

	protected WeaponStatus status;
	protected float weaponReloadedAt;

	void FixedUpdate ()
	{
		switch (status) {
		case WeaponStatus.FIRE:
			LaunchProjectile ();
			status = WeaponStatus.RELOAD;
			weaponReloadedAt = Time.timeSinceLevelLoad + reloadTime;
			break;
		case WeaponStatus.RELOAD:
			if (Time.timeSinceLevelLoad >= weaponReloadedAt)
				status = WeaponStatus.IDLE;
			break;
		}
	}

	public void Fire ()
	{
		if (status == WeaponStatus.IDLE)
			status = WeaponStatus.FIRE;
	}

	public void Destroy ()
	{
		status = WeaponStatus.IDLE;
		if (CachePool<Weapon>.Instance.ReturnObject (this))
			gameObject.SetActive (false);
		else
			DestroyObject (gameObject);
	}

	protected abstract void LaunchProjectile ();

}
