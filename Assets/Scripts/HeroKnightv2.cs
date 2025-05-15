using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HeroKnightv2 : MonoBehaviour
{
	[SerializeField] float m_speed = 4.0f; 
	[SerializeField] bool isGhost;

	private Animator m_animator;
	private Rigidbody2D m_body2d;
	private int m_currentAttack = 0;
	private float m_timeSinceAttack = 0.0f;
	private float m_delayToIdle = 0.0f;
	private Vector2 m_ColliderOffset;

	// Use this for initialization
	void Start()
	{
		m_animator = GetComponent<Animator>();
		m_body2d = GetComponent<Rigidbody2D>();

		m_animator.SetBool("Grounded", true);
		m_body2d.gravityScale = 0;

		m_ColliderOffset = GetComponent<Collider2D>().offset;

		if (isGhost) // Cambiar color y desactivar sombra si está muerto
		{
			GetComponent<SpriteRenderer>().color = new Color(130f / 255f, 255f / 255f, 255f / 255f, 150f / 255f);
			transform.Find("Sombreado").gameObject.SetActive(false);
		}
	}

	// Update is called once per frame
	void Update()
	{
		// Increase timer that controls attack combo
		m_timeSinceAttack += Time.deltaTime;

		// Check if the player is attacking
		bool isAttacking = m_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1") ||
						   m_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2") ||
						   m_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack3");

		float inputX = Input.GetAxis("Horizontal");
		// Swap direction of sprite depending on walk direction
		if (inputX > 0)
		{
			GetComponent<SpriteRenderer>().flipX = false;
			GetComponent<Collider2D>().offset = m_ColliderOffset;
		}
		else if (inputX < 0)
		{
			GetComponent<SpriteRenderer>().flipX = true;
			GetComponent<Collider2D>().offset = new Vector2(-m_ColliderOffset.x, m_ColliderOffset.y);
		}

		// -- Handle input and movement --
		if (!isAttacking) // Only allow movement if not attacking
		{
			float inputY = Input.GetAxis("Vertical");

			m_body2d.velocity = new Vector2(inputX * m_speed, inputY * m_speed);

			// Run
			if (Mathf.Abs(inputX) > Mathf.Epsilon || Mathf.Abs(inputY) > Mathf.Epsilon)
			{
				// Reset timer
				m_delayToIdle = 0.05f;
				m_animator.SetInteger("AnimState", 1);
			}
			// Idle
			else
			{
				// Prevents flickering transitions to idle
				m_delayToIdle -= Time.deltaTime;
				if (m_delayToIdle < 0)
					m_animator.SetInteger("AnimState", 0);
			}
		}
		else
		{
			// Stop movement while attacking
			m_body2d.velocity = Vector2.zero;
		}

		// -- Handle Animations --

		// Hurt
		if (Input.GetKeyDown("q") && !isGhost) // Solo puede recibir daño si no está muerto
		{
			m_animator.SetTrigger("Hurt");
		}
		// Attack
		else if (Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.25f && !isGhost) // Solo puede atacar si no está muerto
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

			// Reset timer
			m_timeSinceAttack = 0.0f;
		}
	}
}
