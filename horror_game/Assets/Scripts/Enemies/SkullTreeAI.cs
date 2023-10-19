using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullTreeAI : MonoBehaviour
{
    public Transform target;//set target from inspector instead of looking in Update
    public float speed = 3f;


    void Start()
    {

    }

    void Update()
    {

        //move towards the player
        if (Vector3.Distance(transform.position, target.position) > .6f)
        {//move if distance from target is greater than 1
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }

    }

}

