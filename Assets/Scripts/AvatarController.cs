using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarController : MonoBehaviour
{
    public int hitPoints, maxHitPoints;
    public int superMeter;
    public int attack, defense, speed;
    Rigidbody2D rb2D;
    public bool isGrounded = true;

    public Transform hitBox;
    public float attackRange = .5f;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    public Animator animator;

    public LayerMask enemyLayers;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += transform.right * Time.deltaTime * speed;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= transform.right * Time.deltaTime * speed;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            isGrounded = false;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            
        }

        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (Time.time >= nextAttackTime)
            {
                Attack();
            }

        }
    }

    void Attack()
    {
        animator.SetTrigger("Punch1");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(hitBox.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<SlimeScript>().TakeDamage(attack * (int).4);
        }
        nextAttackTime = Time.time + (1f / attackRate);

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            
            animator.SetTrigger("Punch2");

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<SlimeScript>().TakeDamage(attack * (int).6);
            }

            nextAttackTime = Time.time + (2f / attackRate);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(hitBox == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(hitBox.position, attackRange);
    }
}
