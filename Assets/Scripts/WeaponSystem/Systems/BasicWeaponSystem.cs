using UnityEngine;
using System.Collections;

public class BasicWeaponSystem : WeaponSystem
{
	protected Weapon weapon;

	public BasicWeaponSystem (Weapon weapon)
	{
		this.weapon = weapon;
	}

	#region implemented abstract members of WeaponSystem

	public override void Fire ()
	{
		this.weapon.Fire ();
	}

	#endregion
}
