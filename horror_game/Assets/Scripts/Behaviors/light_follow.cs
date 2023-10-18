//using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light_follow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // follow the player
        transform.position = new Vector3(GameObject.Find("Player").transform.position.x, GameObject.Find("Player").transform.position.y, -1);
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        mousePosition.z = transform.position.z;
        Quaternion lightRotation = Quaternion.LookRotation(Vector3.forward, mousePosition - transform.position);

        transform.rotation = lightRotation;
    }
}
