using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GuiManager))]
public class GameManager : MonoBehaviour
{
	protected static GameManager instance;

	protected GuiManager guiManager;
	public Player player;

	// Use this for initialization
	void Start ()
	{
		guiManager = GetComponent<GuiManager> ();
		player.FuelChanged += OnFuelChanged;
		player.HealthChanged += OnHealthChanged;
	}

	void OnFuelChanged (float value)
	{
		guiManager.SetFuelSliderPercentage (value);
	}

	void OnHealthChanged (float value)
	{
		guiManager.SetHealthSliderPercentage (value);
	}

	public static GameManager Instance {
		get {
			if (instance == null) {
				instance = Object.FindObjectOfType (typeof(GameManager)) as GameManager;
				
				if (instance == null) {
					GameObject go = new GameObject ("GameManager");
					DontDestroyOnLoad (go);
					instance = go.AddComponent<GameManager> ();
				}
			}
			return instance;
		}
	}

}
