using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SlimeScript : MonoBehaviour
{
    public int maxhealth = 100;
    public HealthBarScript healthBar;

    public int attack, defense, hitPoints;
    public float speed;
    public int timeOfAttackEnd;
    public int attackLength;
    public enum Adjective { Strong, Angry, Meek, Relaxed }

    public enum State { Deciding, Idle, StartAttack, ContinueAttack, Advance, BulkUp, BulkUpRecovery, RunAway, Sleep, Rage}

    public State currentState;

    int rollAdjective;
    public Adjective adjective;
    public TextMeshPro EnemyName;
    

    public GameObject Player;
    public Transform playerPosition;
    public Transform castPoint;
    public float distToPlayer;
    public float aggroRange;

    Rigidbody2D rb2d;
    public Animator animator;
    

    // Start is called before the first frame update
    void Start()
    {
        hitPoints = maxhealth;
        healthBar.SetMaxHealth(maxhealth);

        attack = 10;
        defense = 10;
        rb2d = GetComponent<Rigidbody2D>();
        /*

        rollAdjective = Random.Range(1, 5);

        if (rollAdjective == 1)
        {
            adjective = Adjective.Strong;
        }
        else if (rollAdjective == 2)
        {
            adjective = Adjective.Angry;
        }
        else if (rollAdjective == 3)
        {
            adjective = Adjective.Meek;
        }
        else if (rollAdjective == 4)
        {
            adjective = Adjective.Relaxed;
        }
        */

        adjective = Adjective.Strong;

        animator = GetComponent<Animator>();

        switch (adjective)
        {
            case Adjective.Strong:
                attack += 5;
                defense += 5;
                hitPoints += 20;
                break;

            case Adjective.Angry:
                attack += 10;
                defense -= 5;
                break;

            case Adjective.Meek:
                attack -= 5;
                defense -= 10;
                hitPoints -= 20;
                break;

            case Adjective.Relaxed:
                hitPoints -= 20;
                break;
        }

        EnemyName.GetComponent<TextMesh>();
        EnemyName.text = adjective + " Slime";

    }

    // Update is called once per frame
    void Update()
    {
        distToPlayer = Vector2.Distance(transform.position, playerPosition.position);

        switch (adjective)
        {
            case Adjective.Strong:
            {
                Strong();
                break;
            }

            case Adjective.Angry:
            {
                Angry();
                break;
            }

            case Adjective.Meek:
            {
                Meek();
                break;
            }

            case Adjective.Relaxed:
            {
                Relaxed();
                break;
            }
        }
    }

    void StartAttack()
    {
        animator.SetTrigger("Attack");

 



        timeOfAttackEnd = (int)Time.time + attackLength;
        currentState = State.ContinueAttack;
    }

    void ContinueAttack()
    {

        if (timeOfAttackEnd > Time.time)
        {

        }

        else
        {
            timeOfAttackEnd = 0;
            currentState = State.Deciding;
        }
    }

    void Advance()
    { 
        transform.position += Vector3.left * Time.deltaTime * speed;
        currentState = State.Deciding;
    }

    public void TakeDamage(int damage)
    {
        animator.SetTrigger("Hit");
        hitPoints -= (damage - defense);
        healthBar.SetHealth(hitPoints);

       if(hitPoints <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("Dead", true);

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        Destroy(this.gameObject);
    }

    void CanSeePlayer(float distance)
    {
        float castDist = distance;

        Vector2 endPos = castPoint.position + Vector3.right * distance;

        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Player"));
        
        if(hit.collider != null)
        {
            
        }
    }


    void Strong()
    {
        switch (currentState)
        {
            case State.Deciding:
                {
                    Debug.Log("Decide");

                    if (Random.value < (1f * Time.deltaTime))
                    {
                        currentState = State.BulkUp;
                    }

                    else if(distToPlayer < 2)
                    {
                        currentState = State.StartAttack;
                    }
                    else
                    {
                        currentState = State.Advance;
                    }

                    break;
                }
            
            case State.Advance:
                {
                    Advance();
                    break;
                }

            case State.StartAttack:
                {
                    StartAttack();
                    break;
                }

            case State.ContinueAttack:
                {
                    ContinueAttack();
                    break;
                }

            case State.BulkUp:
                {
                    Debug.Log("Bulk Up");
                    currentState = State.Deciding;
                    break;
                }

            case State.BulkUpRecovery:
                {
                    break;
                }
        }
    }

    void Angry()
    {
        switch (currentState)
        {
            case State.Idle:
                {
                    break;
                }

            case State.StartAttack:
                {
                    StartAttack();
                    break;
                }

            case State.ContinueAttack:
                {
                    break;
                }

            case State.Rage:
                {
                    break;
                }
        }
    }

    void Meek()
    {
        switch (currentState)
        {
            case State.Idle:
                {
                    break;
                }

            case State.StartAttack:
                {
                    StartAttack();
                    break;
                }

            case State.ContinueAttack:
                {
                    break;
                }

            case State.RunAway:
                {
                    break;
                }
        }
    }

    void Relaxed()
    {
        switch (currentState)
        {
            case State.Idle:
                {
                    break;
                }

            case State.StartAttack:
                {
                    StartAttack();
                    break;
                }

            case State.ContinueAttack:
                {
                    break;
                }

            case State.Sleep:
                {
                    break;
                }
        }
    }
}
