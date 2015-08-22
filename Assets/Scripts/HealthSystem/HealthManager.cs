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

	public float maxHealth, maxArmor;
	public HealthType healthType;

	protected HealthStatus status, prevStatus;
	protected Damageable damageable;

	// Use this for initialization
	void Start ()
	{
		damageable = new Health (maxHealth);

		switch (healthType) {
		case HealthType.WITH_ARMOR:
			damageable = new Armor (damageable, maxArmor);
			break;
		}
	}

	public void TakeDamage (float damage)
	{
		damageable.TakeDamage (damage);
		if (damageable.GetTotalCurrentHealth () == 0)
			status = HealthStatus.DEAD;
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
