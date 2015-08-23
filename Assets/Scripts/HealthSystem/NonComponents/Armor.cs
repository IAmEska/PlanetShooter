using UnityEngine;
using System.Collections;

public class Armor : DamageableDecorator
{
	public delegate void ArmorDestroy ();
	public event ArmorDestroy ArmorDestroyed;

	public float armor;
	protected float defaultArmor;

	public Armor (Damageable damageable, float armor) : base(damageable)
	{
		this.armor = armor;
		this.defaultArmor = armor;
	}

	public override float GetTotalCurrentHealth ()
	{
		return base.GetTotalCurrentHealth () + armor;
	}

	public override void TakeDamage (float damage)
	{
		if (armor > 0) {
			armor -= damage;
			if (armor <= 0) {
				damage = -armor;
				armor = 0;
				if (ArmorDestroyed != null)
					ArmorDestroyed ();
			} else {
				damage = 0;
			}
		}
		base.TakeDamage (damage);
	}

	public override void Reset ()
	{
		armor = defaultArmor;
		base.Reset ();
	}

	public override float GetBasicHealthPercentage ()
	{
		return base.GetBasicHealthPercentage ();
	}
}
