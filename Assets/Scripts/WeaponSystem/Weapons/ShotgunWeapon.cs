using UnityEngine;
using System.Collections;

public class ShotgunWeapon : BasicWeapon
{
	public float angleStep = 15f;
	public int halfProjectileCount = 3;

	protected override void LaunchProjectile ()
	{
		base.LaunchProjectile ();
		for (int i=0; i<halfProjectileCount; i++) {
			Projectile proj1 = ProjectileFactory.Instance.Get (projectile);
			proj1.Launch (transform);
			proj1.transform.Rotate (proj1.transform.forward * angleStep * i);

			Projectile proj2 = ProjectileFactory.Instance.Get (projectile);
			proj2.Launch (transform);
			proj2.transform.Rotate (proj2.transform.forward * -angleStep * i);
		}
	}
}
