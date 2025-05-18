using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HeroKnightv2 : MonoBehaviour
{
	[SerializeField] float m_speed = 4.0f; 
	[SerializeField] bool isGhost;
	[SerializeField] GameObject hitbox;
	[SerializeField] AudioSource slashAS, stepGrassAS, stepStoneAS;

	private Animator m_animator;
	private Rigidbody2D m_body2d;
	private int m_currentAttack = 0;
	private float m_timeSinceAttack = 0.5f;
	private float m_delayToIdle = 0.0f;
	private bool isOnStone = false;
	private Coroutine footstepCoroutine;

	// Use this for initialization
	void Start()
	{
		m_animator = GetComponent<Animator>();
		m_body2d = GetComponent<Rigidbody2D>();

		m_animator.SetBool("Grounded", true);
		m_body2d.gravityScale = 0;
		
		if (isGhost) // Cambiar color y desactivar sombra si está muerto
		{
			GetComponent<SpriteRenderer>().color = new Color(130f / 255f, 255f / 255f, 255f / 255f, 150f / 255f);
			transform.Find("Sombreado").gameObject.SetActive(false);
		}
	}

	void Update()
	{
		// Increase timer that controls attack combo
		m_timeSinceAttack += Time.deltaTime;

		// Check if the player is attacking
		bool isAttacking = m_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1") ||
						   m_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2") ||
						   m_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack3");
		bool isBeingHurt = m_animator.GetCurrentAnimatorStateInfo(0).IsName("Hurt");

		float inputX = Input.GetAxis("Horizontal");
		// Swap direction of sprite depending on walk direction
		if (inputX > 0)
		{
			transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
		}
		else if (inputX < 0)
		{
			transform.localScale = new Vector3(-0.5f, 0.5f, 1.0f);
		}

		// -- Handle input and movement --
		if (!isAttacking && !isBeingHurt) // Only allow movement if not attacking or being hurt
		{
			float inputY = Input.GetAxis("Vertical");

			m_body2d.velocity = new Vector2(inputX * m_speed, inputY * m_speed);

			// Run
			if (Mathf.Abs(inputX) > Mathf.Epsilon || Mathf.Abs(inputY) > Mathf.Epsilon)
			{
				// Reset timer
				m_delayToIdle = 0.05f;
				m_animator.SetInteger("AnimState", 1);

				if (footstepCoroutine == null && !isGhost)
				{
					footstepCoroutine = StartCoroutine(PlayFootsteps());
				}
			}
			// Idle
			else
			{
				// Prevents flickering transitions to idle
				m_delayToIdle -= Time.deltaTime;
				if (m_delayToIdle < 0)
					m_animator.SetInteger("AnimState", 0);

				if (footstepCoroutine != null)
				{
					StopCoroutine(footstepCoroutine);
					stepGrassAS.Stop();
					stepStoneAS.Stop();
					footstepCoroutine = null;
				}
			}
		}
		else
		{
			// Stop movement while attacking or being hurt
			m_body2d.velocity = Vector2.zero;
		}

		// -- Handle Animations --

		// Attack
		// Solo puede atacar si no está muerto y no le están haciendo daño
		if (Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.5f && !isGhost && !isBeingHurt)
		{
			m_currentAttack++;

			// Loop back to one after third attack
			if (m_currentAttack > 3)
				m_currentAttack = 1;

			// Reset Attack combo if time since last attack is too large
			if (m_timeSinceAttack > 1.0f)
				m_currentAttack = 1;

			// Call one of three attack animations "Attack1", "Attack2", "Attack3"
			m_animator.SetTrigger("Attack" + m_currentAttack);

			slashAS.Play();

			// Reset timer
			m_timeSinceAttack = 0.0f;

			// Activar hitbox brevemente
			StartCoroutine(EnableHitbox(0.2f)); // activa por 0.2 segundos
		}
	}

	private IEnumerator EnableHitbox(float duration)
	{
		hitbox.SetActive(true);
		yield return new WaitForSeconds(duration);
		hitbox.SetActive(false);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("EnemyAttack"))
		{
			TakeDamage();
		}
	}

	public void TakeDamage()
	{
		// Animación de daño
		m_animator.SetTrigger("Hurt");
	}

	public void SetIsOnStone(bool value)
	{
		isOnStone = value;
	}

	private IEnumerator PlayFootsteps()
	{
		while (true)
		{
			bool isAttacking = m_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1") ||
							   m_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2") ||
						       m_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack3");

			bool isBeingHurt = m_animator.GetCurrentAnimatorStateInfo(0).IsName("Hurt");

			if (!isAttacking && !isBeingHurt && !isGhost)
			{
				if (isOnStone)
				{
					if (!stepStoneAS.isPlaying)
					{
						stepGrassAS.Stop();
					}
					else
					{
						stepStoneAS.Stop();
					}
					stepStoneAS.Play();
				}
				else
				{
					if (!stepGrassAS.isPlaying)
					{
						stepStoneAS.Stop();
					}
					else
					{
						stepGrassAS.Stop();
					}
					stepGrassAS.Play();
				}
			}
			
			yield return new WaitForSeconds(0.5f);
		}
	}
}
