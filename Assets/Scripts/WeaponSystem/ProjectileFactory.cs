using UnityEngine;
using System.Collections;

public class ProjectileFactory : MonoBehaviour
{
	protected static ProjectileFactory instance;
	
	public static ProjectileFactory Instance {
		get {
			if (instance == null) {
				instance = Object.FindObjectOfType (typeof(ProjectileFactory)) as ProjectileFactory;
				
				if (instance == null) {
					GameObject go = new GameObject ("Factory_Projectile");
					DontDestroyOnLoad (go);
					instance = go.AddComponent<ProjectileFactory> ();
				}
			}
			return instance;
		}
	}
	
	public Projectile Get (Projectile prefab)
	{
		Projectile p = CachePool<Projectile>.Instance.GetObjectOfType (prefab.GetType ().Name);
		if (p == null)
			p = Instantiate<Projectile> (prefab);
		return p;
	}
}
