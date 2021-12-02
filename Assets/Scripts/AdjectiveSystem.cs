using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjectiveSystem : MonoBehaviour
{
    public GameObject Slime;
    public GameObject SlimeSpawn;
    public int attack, defense, hitPoints;
    public enum Adjective {Strong, Angry, Meek, Relaxed}

    Adjective adjective;

    // Start is called before the first frame update
    void Start()
    {
        hitPoints = 100;
        attack = 10;
        defense = 10;

        // randomly select adjective, set it in slime script to use different attack patterns 

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
    }

}
