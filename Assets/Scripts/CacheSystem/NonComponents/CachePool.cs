using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class CachePool<T> where T : UnityEngine.Object
{
	public const int DEFAULT_CACHE_POOL_SIZE = 10;

	protected Dictionary<string, List<T>> pool = new Dictionary<string, List<T>> ();

	protected CachePool ()
	{
		size = DEFAULT_CACHE_POOL_SIZE;
	}

	protected CachePool (int size)
	{
		this.size = size;
	}

	public T GetObjectOfType (string prefabName)
	{
		if (!pool.ContainsKey (prefabName)) {
			Debug.Log ("Pool has no key - " + prefabName);
			return default(T);
		}
		
		if (pool [prefabName] == null || pool [prefabName].Count == 0) {
			Debug.Log ("Pool is empty - " + prefabName);
			return default(T);
		}
		
		T t = pool [prefabName] [0];
		pool [prefabName].Remove (t);
		return t;
	}

	public bool ReturnObject (T t)
	{
		string type = t.GetType ().Name;
		
		if (!pool.ContainsKey (type)) {
			Debug.Log ("Pool adding key _ " + type);
			pool.Add (type, new List<T> ());
		}
		
		if (pool [type] == null)
			pool [type] = new List<T> ();
		
		if (pool [type].Count < size) {
			pool [type].Add (t);
			return true;
		} else {
			return false;
		}
	}

	protected int size;

	protected static CachePool<T> instance = new CachePool<T> ();
	
	public static CachePool<T> Instance {
		get {
			return instance;
		}
	}
}
