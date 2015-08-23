using UnityEngine;
using System.Collections;

public class HealthManager : MonoBehaviour
{
	public enum HealthType
	{
		BASIC,
		WITH_ARMOR
	}

	public enum HealthStatus
	{
		ALIVE,
		DEAD
	}

	public delegate void Kill ();
	public event Kill Killed;

	public delegate void Damage (float amount);
	public event Damage Damaged;

	public GameObject armor;

	public float maxHealth, maxArmor;
	public HealthType healthType;

	protected HealthStatus status, prevStatus;
	protected Damageable damageable;
	protected GameObject myArmor;

	// Use this for initialization
	void Start ()
	{
		damageable = new Health (maxHealth);



		switch (healthType) {
		case HealthType.WITH_ARMOR:
			myArmor = Instantiate (armor);
			myArmor.transform.parent = transform;
			myArmor.transform.localPosition = Vector3.zero;

			Armor a = new Armor (damageable, maxArmor);
			a.ArmorDestroyed += () => 
			{
				Debug.Log ("Armor destryoed");
				myArmor.gameObject.SetActive (false);
			};
			damageable = a;
			break;
		}
	}

	public void TakeDamage (float damage)
	{
		damageable.TakeDamage (damage);

		if (Damaged != null)
			Damaged (damageable.GetBasicHealthPercentage ());

		if (damageable.GetTotalCurrentHealth () == 0)
			status = HealthStatus.DEAD;
	}

	public void Reset ()
	{
		damageable.Reset ();
		if (myArmor != null)
			myArmor.SetActive (true);
		status = HealthStatus.ALIVE;

		if (Damaged != null)
			Damaged (damageable.GetBasicHealthPercentage ());

	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		if (prevStatus != status) {
			switch (status) {
			case HealthStatus.DEAD:
				if (Killed != null)
					Killed ();
				break;
			}
			prevStatus = status;
		}

	}
}
