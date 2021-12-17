using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarController : MonoBehaviour
{
    public int hitPoints;
    public int maxHitPoints;
    public HealthBarScript healthBar;
    
    public int superMeter;
    public int attack, defense, speed;
    public int damageDealt;
    public int punchNo;
    
    Rigidbody2D rb2D;
    public bool isGrounded = true;

    public Transform hitBox;
    public float attackRange = .5f;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    float punchTime = 2f;

    public Animator animator;

    public LayerMask enemyLayers;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        punchNo = 1;
        hitPoints = maxHitPoints;
        healthBar.SetMaxHealth(maxHitPoints);
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
            //if (Time.time >= nextAttackTime || punchNo > 1) 
          //  {
                Attack();
           // }

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (punchTime < Time.time)
        {
            punchNo = 1;
        }
    }

    void Attack()
    {

        switch (punchNo)
        {

            case (1):
                {
                    Debug.Log("We Got to Punch1");

                    animator.SetTrigger("Punch1");

                    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(hitBox.position, attackRange, enemyLayers);

                    foreach (Collider2D enemy in hitEnemies)
                    {
                        damageDealt = (int)(attack * .4f);
                        enemy.GetComponent<SlimeScript>().TakeDamage(damageDealt);
                    }
                    punchTime = Time.time + 5;

                    punchNo = 2;
                    break;
                }

            case (2):
                {
                    animator.SetTrigger("Punch2");

                    Debug.Log("We Got to Punch2");

                    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(hitBox.position, attackRange, enemyLayers);

                    foreach (Collider2D enemy in hitEnemies)
                    {
                        damageDealt = (int)(attack * .6f);
                        enemy.GetComponent<SlimeScript>().TakeDamage(damageDealt);
                    }

                    punchTime = Time.time + 5;
                    punchNo = 3;
                    break;
                }

            case (3):
                {
                    animator.SetTrigger("Punch3");

                    Debug.Log("We Got to Punch3");

                    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(hitBox.position, attackRange, enemyLayers);

                    foreach (Collider2D enemy in hitEnemies)
                    {
                        damageDealt = (int)(attack * .8f);
                        enemy.GetComponent<SlimeScript>().TakeDamage(damageDealt);
                    }
                    punchTime = Time.time + 5;
                    punchNo = 4;
                    break;
                }

            case (4):
                {
                    animator.SetTrigger("Punch4");

                    Debug.Log("We Got to Punch4");

                    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(hitBox.position, attackRange, enemyLayers);

                    foreach (Collider2D enemy in hitEnemies)
                    {
                        damageDealt = attack;
                        enemy.GetComponent<SlimeScript>().TakeDamage(damageDealt);
                    }
                    punchNo = 1;
                    break;
                }
        }
    }

    public void PlayerDamaged(int damage)
    {
        Debug.Log("We Took Damage");
        hitPoints -= (damage * damage / (damage + defense));
        healthBar.SetHealth(hitPoints);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Felt Collision");

        if (collision.collider.tag == "EnemyAttack")
        {
            Debug.Log("Touched by an Enemy");

            //if (collision.gameObject.GetComponent<SlimeScript>().currentState == SlimeScript.State.StartAttack)
          //  {
                Debug.Log("The Slime hit us");

                PlayerDamaged(collision.gameObject.GetComponent<SlimeScript>().attack);
          //  }
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
