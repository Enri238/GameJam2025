using UnityEngine;
using System.Collections;

public class Banditv2 : MonoBehaviour {

    [SerializeField] float      m_speed = 4.0f;
    [SerializeField] Transform  target;

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private bool                m_attack = false;
    private bool                m_isDead = false;

    // Use this for initialization
    void Start () {
		m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();

		m_animator.SetBool("Grounded", true);
        m_body2d.gravityScale = 0;
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
		// Attack if reached target
		if (distanceToTarget < 0.5f)
		{
			m_attack = true;
		} else
        {
            m_attack = false;
        }

		// Move <-- Los comentarios de este if hacen que no se pueda empujar. Si se prefiere: descomentar
		if (!m_attack && !isAttacking)
		{
			//m_body2d.bodyType = RigidbodyType2D.Dynamic;
			transform.position = Vector2.MoveTowards(transform.position, target.position, Time.deltaTime * m_speed);
		} else
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

        //Hurt
        else if (Input.GetKeyDown("q"))
            m_animator.SetTrigger("Hurt");

        //Attack
        else if (m_attack && !isAttacking)
        {
            m_animator.SetTrigger("Attack");
        }

        //Run
        else if (!m_attack)
            m_animator.SetInteger("AnimState", 2);
    }
}
