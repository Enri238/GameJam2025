using UnityEngine;
using System.Collections;

public class HeroKnightv2 : MonoBehaviour
{
	[SerializeField] float m_speed = 4.0f;
	[SerializeField] GameObject m_slideDust;

	private Animator m_animator;
	private Rigidbody2D m_body2d;
	private int m_currentAttack = 0;
	private float m_timeSinceAttack = 0.0f;
	private float m_delayToIdle = 0.0f;

	// Use this for initialization
	void Start()
	{
		m_animator = GetComponent<Animator>();
		m_body2d = GetComponent<Rigidbody2D>();

		m_animator.SetBool("Grounded", true);
		m_body2d.gravityScale = 0;
	}

	// Update is called once per frame
	void Update()
	{
		// Increase timer that controls attack combo
		m_timeSinceAttack += Time.deltaTime;

		// Check if the player is attacking
		bool isAttacking = m_animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack1") ||
						   m_animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack2") ||
						   m_animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack3");

		// -- Handle input and movement --
		if (!isAttacking) // Only allow movement if not attacking
		{
			float inputX = Input.GetAxis("Horizontal");
			float inputY = Input.GetAxis("Vertical");

			// Swap direction of sprite depending on walk direction
			if (inputX > 0)
			{
				GetComponent<SpriteRenderer>().flipX = false;
			}
			else if (inputX < 0)
			{
				GetComponent<SpriteRenderer>().flipX = true;
			}

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
		if (Input.GetKeyDown("q"))
		{
			m_animator.SetTrigger("Hurt");
		}
		// Attack
		else if (Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.25f)
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
