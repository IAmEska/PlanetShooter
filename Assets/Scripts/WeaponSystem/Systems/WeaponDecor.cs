using UnityEngine;
using System.Collections;

public abstract class WeaponDecor : WeaponSystem
{

	protected WeaponSystem weaponSystem;

	public WeaponDecor (WeaponSystem weaponSystem)
	{
		this.weaponSystem = weaponSystem;
	}

	#region implemented abstract members of WeaponSystem
	public override void Fire ()
	{
		weaponSystem.Fire ();
	}
	#endregion
}
