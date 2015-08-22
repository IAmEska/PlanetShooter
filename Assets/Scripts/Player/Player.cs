using UnityEngine;
using System.Collections;

[RequireComponent(typeof(WeaponManager))]
[RequireComponent(typeof(MovementManager))]
public class Player : MonoBehaviour
{

	protected WeaponManager weaponManager;
	protected MovementManager movementManager;

	// Use this for initialization
	void Start ()
	{
		weaponManager = GetComponent<WeaponManager> ();
		movementManager = GetComponent<MovementManager> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey (KeyCode.Space)) {
			weaponManager.Fire ();
		}

		movementManager.Move (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));

		if (Input.GetKeyDown (KeyCode.Q))
			weaponManager.SwitchWeapon (false);

		if (Input.GetKeyDown (KeyCode.W))
			weaponManager.SwitchWeapon (true);

		if (Input.GetKeyDown (KeyCode.A))
			weaponManager.SwitchSideWeapon (false);

		if (Input.GetKeyDown (KeyCode.S))
			weaponManager.SwitchSideWeapon (true);
	}
}
