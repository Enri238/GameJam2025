using UnityEngine;
using System.Collections;

public class Cegaro : MonoBehaviour {

    [SerializeField] float      m_speed = 4.0f;
    [SerializeField] Transform  target;

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private bool                m_attack = false;
    private bool                m_isDead = false;
    private Spawner             m_spawner;

	// Use this for initialization
	void Start () {
		m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();

		m_animator.SetBool("Grounded", true);
        m_body2d.gravityScale = 0;

        target = GameObject.Find("Jugador").transform;
	}
	
	// Update is called once per frame
	void Update () {
		// -- Handle movement --

		// Swap direction of sprite depending on walk direction
        if (target.position.x < transform.position.x)
            transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
        else
            transform.localScale = new Vector3(-0.5f, 0.5f, 1.0f);

		float distanceToTarget = Vector3.Distance(transform.position, target.position);
		bool isAttacking = m_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");
		bool isBeingHurt = m_animator.GetCurrentAnimatorStateInfo(0).IsName("Hurt");
		// Attack if reached target
		if (distanceToTarget < 0.5f)
		{
			m_attack = true;
		} else
        {
            m_attack = false;
        }

		// Move <-- Los comentarios de este if hacen que no se pueda empujar. Si se prefiere: descomentar
		if (!m_attack && !isAttacking && !isBeingHurt)
		{
			//m_body2d.bodyType = RigidbodyType2D.Dynamic;
			transform.position = Vector2.MoveTowards(transform.position, target.position, Time.deltaTime * m_speed);
		} else if (isBeingHurt)
		{
			//
		}
		else
		{
			//m_body2d.bodyType = RigidbodyType2D.Kinematic;
			m_body2d.velocity = Vector2.zero; // Se para en seco cuando ataca
		}

        // -- Handle Animations -- // Cambiar
        //Death
        if (Input.GetKeyDown("h"))
        {
            if (!m_isDead)
                m_animator.SetTrigger("Death");
            else
                m_animator.SetTrigger("Recover");

            m_isDead = !m_isDead;
        }

        //Attack
        else if (m_attack && !isAttacking && !isBeingHurt)
        {
            m_animator.SetTrigger("Attack");
        }

        //Run
        else if (!m_attack && !isBeingHurt)
            m_animator.SetInteger("AnimState", 2);
    }

	public void SetSpawner(Spawner spawner)
	{
		m_spawner = spawner;
	}

	private void OnDestroy()
	{
        if (m_spawner != null)
        {
			m_spawner.CegaroDestruido();
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Attack"))
		{
			TakeDamage();
		}
	}

	public void TakeDamage()
	{
		m_animator.SetTrigger("Hurt");
	}
}
