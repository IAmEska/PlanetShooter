using UnityEngine;
using System.Collections;

public class WeaponSystemBuilder
{

	protected WeaponSystem weaponSystem;

	public WeaponSystemBuilder (Weapon weapon)
	{
		weaponSystem = new BasicWeaponSystem (weapon);
	}

	public void SetSideWeapons (Weapon[] weapons)
	{
		weaponSystem = new SideWeaponSystem (weapons, weaponSystem);
	}

	public WeaponSystem WeaponSystem {
		get {
			return weaponSystem;
		}
	}
}
