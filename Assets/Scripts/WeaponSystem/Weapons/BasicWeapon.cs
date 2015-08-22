using UnityEngine;
using System.Collections;

public class BasicWeapon : Weapon
{

	public Projectile projectile;

	#region implemented abstract members of Weapon

	protected override void LaunchProjectile ()
	{
		ProjectileFactory.Instance.Get (projectile).Launch (transform);
	}

	#endregion

}
