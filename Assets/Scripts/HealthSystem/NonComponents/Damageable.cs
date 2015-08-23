using UnityEngine;
using System.Collections;

public abstract class Damageable
{
	public abstract float GetTotalCurrentHealth ();
	public abstract void TakeDamage (float damage);
	public abstract void Reset ();
	public abstract float GetBasicHealthPercentage (); 
}
