using UnityEngine;
using System.Collections;

public class Cegaro : MonoBehaviour {

    [SerializeField] float      m_speed = 4.0f;
    [SerializeField] Transform  target;

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private bool                m_attack = false;
	private bool				m_isDead = false;
    private Spawner             m_spawner;
	private int                 m_hits2die;

	private GameObject			m_hitbox;
	private float				m_timeSinceAttack;

	// Use this for initialization
	void Start () {
		m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();

		m_animator.SetBool("Grounded", true);
        m_body2d.gravityScale = 0;

        target = GameObject.Find("Jugador").transform;
		
		// Sistema de vida extremadamente simple (número de golpes para que se muera)
		// ^-- Con más tiempo lo hubiese hecho con un script de vida
		m_hits2die = 5;

		m_hitbox = transform.Find("Hitbox").gameObject;
		m_timeSinceAttack = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		if (m_isDead)
		{
			return;
		}

		m_timeSinceAttack += Time.deltaTime;

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
		if (distanceToTarget < 0.5f && m_timeSinceAttack > 1f)
		{
			m_attack = true;
		}
		else if (m_timeSinceAttack < 1f) // Combat idle
		{
			m_animator.SetInteger("AnimState", 1);
			isAttacking = true;
		}
		else
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

        // -- Handle Animations --

        //Attack
        if (m_attack && !isAttacking && !isBeingHurt)
        {
            m_animator.SetTrigger("Attack");

			m_timeSinceAttack = 0.0f;

			// Activar hitbox brevemente
			StartCoroutine(EnableHitbox(0.4f, 0.2f));
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

	private IEnumerator EnableHitbox(float delayBeforeActivation, float activeDuration)
	{
		yield return new WaitForSeconds(delayBeforeActivation);
		m_hitbox.SetActive(true);
		yield return new WaitForSeconds(activeDuration);
		m_hitbox.SetActive(false);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("PlayerAttack"))
		{
			TakeDamage();
		}
	}

	public void TakeDamage()
	{
		// Animaciones de daño y muerte
		if (m_hits2die > 1)
		{
			m_hits2die--;
			m_animator.SetTrigger("Hurt");
		}
		else
		{
			Die();
		}
	}

	private void Die()
	{
		m_isDead = true;
		// Parar en seco
		m_body2d.velocity = Vector2.zero;
		m_body2d.bodyType = RigidbodyType2D.Kinematic;
		// Activar animación de muerte
		m_animator.SetTrigger("Death");
		// Cambiar dimensiones de la sombra
		Transform sombra = transform.Find("Sombreado");
		sombra.transform.localScale = new Vector3(1.67f, sombra.transform.localScale.y, 1.0f);
		// Esperar y destruir
		StartCoroutine(WaitAndDestroy());
	}

	private IEnumerator WaitAndDestroy()
	{
		yield return new WaitForSeconds(1f);
		Destroy(gameObject);
	}
}
