using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScript : MonoBehaviour
{
    public int maxhealth = 100;
    public HealthBarScript healthBar;

    public int attack, defense, hitPoints, speed;
    public int timeOfAttack;
    public int attackLength;
    public enum Adjective { Strong, Angry, Meek, Relaxed }

    public enum Decide { Idle, Attack, ContinueAttack, BulkUp, RunAway, Sleep, Rage}

    public Decide currentState;

    int rollAdjective;
    public Adjective adjective;

    public GameObject Player;
    public Transform playerPosition;
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


        rollAdjective = Random.Range(1, 4);

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
    }

    // Update is called once per frame
    void Update()
    {

        //distance from player 
        float distToPlayer = Vector2.Distance(transform.position, playerPosition.position);
        
        
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

    void Attack()
    {
        animator.SetTrigger("Attack");

        timeOfAttack = (int)Time.time + attackLength;
        currentState = Decide.ContinueAttack;
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
    }


    void Strong()
    {
        switch (currentState)
        {
            case Decide.Idle:
                {
                    break;
                }

            case Decide.Attack:
                {
                    Attack();
                    break;
                }

            case Decide.ContinueAttack:
                {
                    Attack();
                    break;
                }

            case Decide.BulkUp:
                {
                    break;
                }
        }
    }

    void Angry()
    {
        switch (currentState)
        {
            case Decide.Idle:
                {
                    break;
                }

            case Decide.Attack:
                {
                    Attack();
                    break;
                }

            case Decide.ContinueAttack:
                {
                    break;
                }

            case Decide.Rage:
                {
                    break;
                }
        }
    }

    void Meek()
    {
        switch (currentState)
        {
            case Decide.Idle:
                {
                    break;
                }

            case Decide.Attack:
                {
                    Attack();
                    break;
                }

            case Decide.ContinueAttack:
                {
                    break;
                }

            case Decide.RunAway:
                {
                    break;
                }
        }
    }

    void Relaxed()
    {
        switch (currentState)
        {
            case Decide.Idle:
                {
                    break;
                }

            case Decide.Attack:
                {
                    Attack();
                    break;
                }

            case Decide.ContinueAttack:
                {
                    break;
                }

            case Decide.Sleep:
                {
                    break;
                }
        }



        if (Time.time < timeOfAttack)
        {

        }
        else
        {
            currentState = Decide.Idle;
        }
    }
}
