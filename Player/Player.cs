using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, Pausable
{
	private int health = 100;

	private Rigidbody rb;

	public float speed = 4;

	private KeyCode up;
	private KeyCode down;
	private KeyCode right;
	private KeyCode left;

	public ParticleSystem bllodEffect;
	public Material DamageMaterial;
	private MeshRenderer meshRenderer;
	private Material defaultMat;
	public ParticleSystem dyingEffect;

	private Slider healthBar = null;

	public int ID = -1;

	void Awake()
	{
		rb = GetComponent<Rigidbody>();
		meshRenderer = GetComponent<MeshRenderer>();

		if (ID != -1)
		{
			up = Globals.Keys.playerKeys[ID].up;
			down = Globals.Keys.playerKeys[ID].down;
			right = Globals.Keys.playerKeys[ID].right;
			left = Globals.Keys.playerKeys[ID].left;
		}

		defaultMat = meshRenderer.material;

		GameManager.pausables.Add(this);
	}

	Vector3 input = Vector3.zero;
	[HideInInspector] public Vector3 lastInputNotZero = Vector3.forward;

	public void SetHealthBar(Slider healthBar)
	{
		this.healthBar = healthBar;
	}

	void Update()
	{
		input = Vector3.zero;

		if (Input.GetKey(up))
			input.z += 1;
		if (Input.GetKey(down))
			input.z -= 1;
		if (Input.GetKey(right))
			input.x += 1;
		if (Input.GetKey(left))
			input.x -= 1;

		if (transform.position.y < -10)
			Die(this);
	}

	void FixedUpdate()
	{
		rb.MovePosition(transform.position + input * speed * Time.fixedDeltaTime);

		if (input != Vector3.zero)
			lastInputNotZero = input;
	}

	public void GetDamege(int strength, MonoBehaviour damager)
	{
		if (!bllodEffect.isPlaying)
			bllodEffect.Play();
		else
		{
			bllodEffect.Stop();
			bllodEffect.Play();
		}

		StartCoroutine(ShineRed());

		health -= strength;
		healthBar.value = health;
		if (health <= 0)
		{
			Die(damager);
		}
	}

	IEnumerator ShineRed()
	{
		meshRenderer.material = DamageMaterial;

		yield return new WaitForSeconds(0.1f);

		meshRenderer.material = defaultMat;
	}

	public void Die(MonoBehaviour causer)
	{
		healthBar.value = 0;

		if (transform.up.y < 0) // перевернуть если он вниз направлен
			transform.Rotate(new Vector3(180, 0, 0));

		if (causer is Bullet)
		{
			StartCoroutine(Dying());
		}
		else if (causer is Lava)
		{
			GameObject.Find("GameManager").GetComponent<KillingManager>().IWasKilled(gameObject);
			Destroy(gameObject);
		}
		else if (causer is SawBehaviour)
		{
			StartCoroutine(Dying());
		}
		else if (causer is Sworder)
		{
			StartCoroutine(Dying());
		}
		else if (causer is Rocket)
		{
			StartCoroutine(Dying());
		}
		else if (causer is KillToucher)
		{
			StartCoroutine(Dying());
		}
		else if (causer is MeteorBehaviour)
		{
			StartCoroutine(Dying());
		}
		else
		{
			GameObject.Find("GameManager").GetComponent<KillingManager>().IWasKilled(gameObject);
			Destroy(gameObject);
		}
	}

	IEnumerator Dying()
	{
		dyingEffect.Play();

		yield return new WaitForSeconds(dyingEffect.main.duration + dyingEffect.main.startLifetime.constant / 2);

		GameObject.Find("GameManager").GetComponent<KillingManager>().IWasKilled(gameObject);
		Destroy(gameObject);
	}

	public void SetPause(bool pause)
	{
		if (ID <= Globals.Settings.numberOfPlayers - 1)
			gameObject.SetActive(!pause);
	}

	public void RemoveAllComponents()
	{
		Destroy(rb);
		Destroy(GetComponent<Hunter>());
		Destroy(this);
	}
}
