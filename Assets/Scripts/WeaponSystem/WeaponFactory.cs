using UnityEngine;
using System.Collections;

public class WeaponFactory : MonoBehaviour
{
	protected static WeaponFactory instance;
	
	public static WeaponFactory Instance {
		get {
			if (instance == null) {
				instance = Object.FindObjectOfType (typeof(WeaponFactory)) as WeaponFactory;
				
				if (instance == null) {
					GameObject go = new GameObject ("Factory_Weapon");
					DontDestroyOnLoad (go);
					instance = go.AddComponent<WeaponFactory> ();
				}
			}
			return instance;
		}
	}
	
	public Weapon Get (Weapon prefab)
	{
		Weapon p = CachePool<Weapon>.Instance.GetObjectOfType (prefab.GetType ().Name);
		if (p == null)
			p = Instantiate<Weapon> (prefab);
		return p;
	}

}
