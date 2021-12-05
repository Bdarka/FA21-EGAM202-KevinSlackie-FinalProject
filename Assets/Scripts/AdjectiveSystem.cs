using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjectiveSystem : MonoBehaviour
{
    public GameObject Slime;
    public GameObject SlimeSpawn;
    public int attack, defense, hitPoints;
    public enum Adjective {Strong, Angry, Meek, Relaxed}
    int rollAdjective;
    Adjective adjective;

    // Start is called before the first frame update
    void Start()
    {

        rollAdjective = Random.Range(1, 4);

        if(rollAdjective == 1)
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


        /*


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


        Instantiate(Slime, SlimeSpawn.transform.position, Quaternion.identity);
        */
    }

}
