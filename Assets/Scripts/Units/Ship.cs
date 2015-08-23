using UnityEngine;
using System.Collections;

[RequireComponent(typeof(WeaponManager), typeof(MovementManager))]
[RequireComponent(typeof(SpriteRenderer), typeof(HealthManager))]
public abstract class Ship : MonoBehaviour
{

	public enum PlayerState
	{
		Alive,
		Dead,
		Idle,
		Respawn
	}

	public ParticleSystem motorParticle;
	public Animator destroyEffect;
	
	protected WeaponManager weaponManager;
	protected MovementManager movementManager;

	protected HealthManager healthManager;
	protected SpriteRenderer spriteRenderer;
	
	protected PlayerState state = PlayerState.Alive;
	protected PlayerState prevState;



	// Use this for initialization
	void Start ()
	{
		Init ();
	}

	protected virtual void Init ()
	{
		weaponManager = GetComponent<WeaponManager> ();
		movementManager = GetComponent<MovementManager> ();
		healthManager = GetComponent<HealthManager> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		healthManager.Killed += OnKilled;
	}

	void OnKilled ()
	{
		state = PlayerState.Dead;
	}

	// Update is called once per frame
	void Update ()
	{
		if (state == PlayerState.Alive) {
			AliveBehaviour ();
		} else if (state == PlayerState.Dead && prevState != PlayerState.Dead) {
			DeadBehaviour ();
		} else if (state == PlayerState.Respawn && prevState != PlayerState.Respawn) {
			RespawnBehaviour ();
		}
		prevState = state;
	}

	protected abstract void AliveBehaviour ();

	protected virtual void RespawnBehaviour ()
	{
		state = PlayerState.Alive;
		spriteRenderer.enabled = true;
		foreach (SpriteRenderer sr in gameObject.GetComponentsInChildren<SpriteRenderer>()) {
			if (sr.gameObject.GetInstanceID () != destroyEffect.gameObject.GetInstanceID ())
				sr.enabled = true;
		}
		destroyEffect.gameObject.SetActive (false);
		motorParticle.enableEmission = true;
		healthManager.Reset ();

	}

	protected virtual void DeadBehaviour ()
	{
		destroyEffect.gameObject.SetActive (true);
		spriteRenderer.enabled = false;
		foreach (SpriteRenderer sr in gameObject.GetComponentsInChildren<SpriteRenderer>()) {
			if (sr.gameObject.GetInstanceID () != destroyEffect.gameObject.GetInstanceID ())
				sr.enabled = false;
		}
		motorParticle.enableEmission = false;
		StartCoroutine (AnimationWait ());
	}

	IEnumerator AnimationWait ()
	{
		yield return new WaitForSeconds (2);
		state = PlayerState.Respawn;
	}
}
