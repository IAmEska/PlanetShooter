using UnityEngine;
using System.Collections;

public class WeaponManager : MonoBehaviour
{
	public Weapon[] mainWeaponTypes;
	public int mainWeaponSelected = 0;
	public Vector2 mainWeaponPosition;

	public Weapon[] sideWeaponsTypes;
	public Vector2[] sideWeaponsPositions;
	public int sideWeaponsSelected = 0;


	protected Weapon mainWeapon;
	protected Weapon[] sideWeapons;
	protected int prevSelectedMainWeapon, prevSelectedSideWeapon;
	protected WeaponSystem weaponSystem;

	// Use this for initialization
	void Start ()
	{
		SwitchWeapon ();
	}

	public void SwitchWeapon (bool next)
	{
		mainWeaponSelected = GetNewWeaponTypePosition (mainWeaponSelected, mainWeaponTypes.Length, next);
	}

	public void SwitchSideWeapon (bool next)
	{
		sideWeaponsSelected = GetNewWeaponTypePosition (sideWeaponsSelected, sideWeaponsTypes.Length, next);
	}

	protected int GetNewWeaponTypePosition (int actualPosition, int maxCount, bool next)
	{
		int position = actualPosition;
		
		if (next)
			position++;
		else
			position--;
		
		if (position >= maxCount) {
			position = 0;
		} else if (position < 0)
			position = maxCount - 1;

		return position;
	}

	protected void SwitchWeapon ()
	{

		// create new, or recreate if main weapon changed
		if (mainWeapon == null || prevSelectedMainWeapon != mainWeaponSelected) {
			if (mainWeapon != null) // recycle old weapon
				mainWeapon.Destroy ();

			mainWeapon = InstantiateWeapon (mainWeaponTypes [mainWeaponSelected], mainWeaponPosition);
		}
			
		WeaponSystemBuilder builder = new WeaponSystemBuilder (mainWeapon);

		// create new, or reacreate if side weapons changed
		if (sideWeapons == null || prevSelectedSideWeapon != sideWeaponsSelected) {
			if (sideWeapons != null) { // recycle old weapons
				foreach (Weapon w in sideWeapons)
					w.Destroy ();
			}

			if (sideWeaponsPositions.Length > 0) {
				sideWeapons = new Weapon[sideWeaponsPositions.Length];
				for (int i=0; i< sideWeaponsPositions.Length; i++) {
					sideWeapons [i] = InstantiateWeapon (sideWeaponsTypes [sideWeaponsSelected], sideWeaponsPositions [i]);
				}
			}
		}

		if (sideWeapons != null)
			builder.SetSideWeapons (sideWeapons);

		weaponSystem = builder.WeaponSystem;
	}

	protected Weapon InstantiateWeapon (Weapon weapon, Vector2 position)
	{
		Weapon w = WeaponFactory.Instance.Get (weapon);
		w.gameObject.SetActive (true);
		w.transform.parent = transform;
		w.transform.rotation = transform.rotation;
		w.transform.localPosition = position;
		return w;
	}

	void Update ()
	{
		if (mainWeaponSelected != prevSelectedMainWeapon || sideWeaponsSelected != prevSelectedSideWeapon) {
			SwitchWeapon ();
			prevSelectedMainWeapon = mainWeaponSelected;
			prevSelectedSideWeapon = sideWeaponsSelected;
		}
	}

	public void Fire ()
	{
		weaponSystem.Fire ();
	}
}
