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
    public bool bulkUpUsed;

    public int rageRecovery;
    public int rageUp;
    public int rageEffectEnd;
    public int rageAnimationEnd;
    public bool rageUsed;

    public int sleepRecovery;
    public int sleepUp;
    public int sleepEnd;
    public bool sleepUsed;

    public GameObject Protein;
    public GameObject Pete;
    public GameObject Sweat;
    public GameObject Mask;


    public GameObject EndScreen;

    public float deathAnimationEnd;
    public enum Adjective { Strong, Angry, Meek, Relaxed }

    public enum State { Deciding, Idle, StartAttack, ContinueAttack, Advance, BulkUp, RunAway, Sleep, Rage, Dead}

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
       // adjective = Adjective.Meek;

        animator = GetComponent<Animator>();

        switch (adjective)
        {
            case Adjective.Strong:
                attack += 5;
                defense += 5;
                hitPoints += 20;
                Protein.SetActive(true);
                break;

            case Adjective.Angry:
                attack += 10;
                defense -= 5;
                Pete.SetActive(true);
                break;

            case Adjective.Meek:
                attack -= 5;
                defense -= 10;
                hitPoints -= 20;
                Sweat.SetActive(true);
                break;

            case Adjective.Relaxed:
                hitPoints -= 20;
                Mask.SetActive(true);
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

        if (timeOfAttackEnd < Time.time)
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
            currentState = State.Dead;
            animator.SetTrigger("Dead");
            Invoke("Die", 5f);
        }
    }

    void Die()
    {

        GetComponent<Collider2D>().enabled = false;

        this.gameObject.SetActive (false);

        EndScreen.SetActive(true);
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
                    if(bulkUpUsed == false)
                    {
                        StartCoroutine(BulkTime(bulkEffectEnd));
                    }

                    break;
                }
            case State.Dead:
                {
                    break;
                }

        }
    }

    IEnumerator BulkTime(int bulkUpEnd)
    {
        animator.SetTrigger("Bulk");
        bulkUpUsed = true;
        defense += 2;

        yield return new WaitForSeconds(bulkUpEnd);

        currentState = State.Deciding;
        bulkUpUsed = false;
    }


    void Angry()
    {
        switch (currentState)
        {
            case State.Deciding:
                {
                    if (Random.value < (1f * Time.deltaTime) && rageUp < Time.time)
                    {
                        currentState = State.Rage;
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

            case State.Rage:
                {
                    if (rageUsed == false)
                    {
                        StartCoroutine(RageTime(rageAnimationEnd));
                    }
                    break;
                }

            case State.Dead:
                {
                    break;
                }
        }
    }

    IEnumerator RageTime(int rageAnimationEnd)
    {
        animator.SetTrigger("Rage");
        rageUsed = true;
        hitPoints -= 10;
        attack += 5;

        rageUp = (int)Time.time + rageEffectEnd;

        Debug.Log("We Waited");
        yield return new WaitForSeconds(rageAnimationEnd);
        Debug.Log("We Got to after the Wait");

        currentState = State.Deciding;
        rageUsed = false;
    }



    void Meek()
    {
        switch (currentState)
        {
            case State.Deciding:
                {
                    if (Random.value < (1f * Time.deltaTime) && transform.position.x < 6.5f)
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

            case State.Dead:
                {
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
                    ContinueAttack();
                    break;
                }

            case State.Sleep:
                {
                    if(sleepUsed == false)
                    {
                        StartCoroutine(SleepTime(sleepEnd));
                    }
                    break;
                }

            case State.Dead:
                {
                    break;
                }
        }
    }

    IEnumerator SleepTime(int sleepEnd)
    {
        animator.SetTrigger("Sleep");
        sleepUsed = true;
        hitPoints += 5;

        sleepUp = (int)Time.time + sleepEnd;

        Debug.Log("We Waited");
        yield return new WaitForSeconds(sleepEnd);
        Debug.Log("We Got to after the Wait");

        currentState = State.Deciding;
        sleepUsed = false;
    }
}
