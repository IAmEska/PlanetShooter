using UnityEngine;
using System.Collections;

public class Armor : DamageableDecorator
{
	public float armor;

	public Armor (Damageable damageable, float armor) : base(damageable)
	{
		this.armor = armor;
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
				//TODO turnoff armor
			} else {
				damage = 0;
			}
		}
		base.TakeDamage (damage);
	}

}
