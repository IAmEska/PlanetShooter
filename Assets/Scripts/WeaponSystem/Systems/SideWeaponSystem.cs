using UnityEngine;
using System.Collections;

public class SideWeaponSystem : WeaponDecor
{

	protected Weapon[] weapons;

	public SideWeaponSystem (Weapon[] weapons, WeaponSystem system) : base(system)
	{
		this.weapons = weapons;
	}

	public override void Fire ()
	{
		base.Fire ();
		foreach (Weapon weapon in weapons) {
			weapon.Fire ();
		}
	}
}
