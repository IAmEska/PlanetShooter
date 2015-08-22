using UnityEngine;
using System.Collections;

//Monobehaviour cannot be generic
public abstract class AbstractFactory<T> : MonoBehaviour where T : UnityEngine.Object
{
	
	protected static AbstractFactory<T> instance;

	public static AbstractFactory<T> Instance {
		get {
			if (instance == null) {
				instance = Object.FindObjectOfType (typeof(AbstractFactory<T>)) as AbstractFactory<T>;
				
				if (instance == null) {
					GameObject go = new GameObject ("Factory_" + typeof(T).Name);
					DontDestroyOnLoad (go);
					instance = go.AddComponent<AbstractFactory<T>> ();
				}
			}
			return instance;
		}
	}
	
	public T Get (T prefab)
	{
		T p = CachePool<T>.Instance.GetObjectOfType (prefab.GetType ().Name);
		if (p == null)
			p = Instantiate<T> (prefab);
		return p;
	}
}
