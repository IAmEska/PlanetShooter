using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{

	public Enemy enemy;
	public Vector2[] spawnPositions;
	public int enemyCount = 5;
	public GameObject bounds;

	// Use this for initialization
	void Start ()
	{
		Player p = FindObjectOfType<Player> ();
		for (int i=0; i<enemyCount; i++) {
			var en = Instantiate (enemy);
			en.player = p;
			en.bounds = bounds;
			WeaponManager wm = en.GetComponent<WeaponManager> ();
			wm.mainWeaponSelected = Random.Range (0, wm.mainWeaponTypes.Length);

			Vector2 spawnPos = spawnPositions [Random.Range (0, spawnPositions.Length)];
			en.transform.position = spawnPos;
			en.spawnPosition = spawnPos;
		}
	}

}
