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
    
    public int bulkUpRecovery;
    public int bulkUpEnd;
    public int bulkEffectEnd;

    public int rageRecovery;
    public int rageUp;
    public int rageEffectEnd;
    public int rageAnimationEnd;
    public bool rageUsed;

    public int sleepRecovery;
    public int sleepEnd;

    public int deathAnimationEnd;
    public enum Adjective { Strong, Angry, Meek, Relaxed }

    public enum State { Deciding, Idle, StartAttack, ContinueAttack, Advance, BulkUp, RunAway, Sleep, Rage}

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
        
        //Line for debugging different actions
        //adjective = Adjective. ;

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

        bulkEffectEnd = 0;

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
        hitPoints -= (damage * damage / (damage + defense));
        healthBar.SetHealth(hitPoints);

       if(hitPoints <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("Dead", true);

        if (deathAnimationEnd > Time.time)
        {
            GetComponent<Collider2D>().enabled = false;
            this.enabled = false;
            Destroy(this.gameObject);
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

                    if(bulkEffectEnd == 0)
                    { 
                        defense += 5;

                        bulkUpEnd = (int)Time.time + bulkUpRecovery;
                        bulkEffectEnd = (int)Time.time + 20;
                    }


                    if (bulkUpEnd < Time.time)
                    {

                    }
                    else
                    {
                        bulkUpEnd = 0;
                        currentState = State.Deciding;
                    }

                    break;
                }
        }
    }

    void Angry()
    {
        switch (currentState)
        {
            case State.Deciding:
                {
                    if (Random.value < (1f * Time.deltaTime))
                    {
                        currentState = State.Rage;
                    }
                    else if(distToPlayer < 2)
                    {
                        currentState = State.StartAttack;
                    }
                    else if(rageEffectEnd > Time.time)
                    {
                        attack -= 5;
                        currentState = State.Deciding;
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

            case State.Rage:
                {
                    if(rageEffectEnd > Time.time)
                    {
                        rageUsed = false;
                    }

                    if (rageUsed == false)
                    {
                        animator.SetTrigger("Rage");

                        hitPoints -= 10;
                        attack += 5;

                        rageUp = (int)Time.time * rageEffectEnd;

                        rageAnimationEnd = (int)Time.time * rageRecovery;

                        if (rageAnimationEnd < Time.time)
                        {

                        }

                        else
                        {
                            rageUsed = true;
                            currentState = State.Deciding;
                        }
                    }
                    break;
                }
        }
    }

    void Meek()
    {
        switch (currentState)
        {
            case State.Deciding:
                {
                    if (Random.value < (1f * Time.deltaTime))
                    {
                        currentState = State.RunAway;
                    }
                    else if (distToPlayer < 2)
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

            case State.RunAway:
                {
                    transform.position += Vector3.right * speed;

                    currentState = State.Deciding;
                    break;
                }
        }
    }

    void Relaxed()
    {
        switch (currentState)
        {
            case State.Deciding:
                {
                    if (Random.value < (1f * Time.deltaTime))
                    {
                        currentState = State.Sleep;
                    }
                    else if (distToPlayer < 2)
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
                    break;
                }

            case State.Sleep:
                {
                    animator.SetTrigger("Sleep");

                    sleepEnd = (int)Time.time * sleepRecovery;

                    if(sleepEnd > Time.time)
                    {

                    }
                    else
                    {
                        sleepEnd = 0;
                        currentState = State.Deciding;
                    }


                    break;
                }
        }
    }
}
