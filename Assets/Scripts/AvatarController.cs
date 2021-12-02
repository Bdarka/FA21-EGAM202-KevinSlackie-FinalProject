using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarController : MonoBehaviour
{
    public int hitPoints;
    public int superMeter;
    public int attack, defense, speed;
    public bool isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {

        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            
        }

        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            Attack();
        }
    }

    void Attack()
    {

    }
}
