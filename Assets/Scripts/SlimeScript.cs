using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScript : MonoBehaviour
{
    public int health;
    public int attack, defense, hitPoints;
    public enum Adjective { Strong, Angry, Meek, Relaxed }

    Adjective adjective;

    // Start is called before the first frame update
    void Start()
    {
        switch (adjective)
        {
            case Adjective.Strong:
                attack += 5;
                defense += 5;
                hitPoints += 20;
                Strong();
                break;

            case Adjective.Angry:
                attack += 10;
                defense -= 5;
                Angry();
                break;

            case Adjective.Meek:
                attack -= 5;
                defense -= 10;
                hitPoints -= 20;
                Meek();
                break;

            case Adjective.Relaxed:
                hitPoints -= 20;
                Relaxed();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Strong()
    {

    }

    void Angry()
    {

    }

    void Meek()
    {

    }

    void Relaxed()
    {

    }

    void Attack()
    {

    }


}
