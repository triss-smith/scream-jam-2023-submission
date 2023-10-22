using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquiddyLightFollow : MonoBehaviour
{
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // follow the player
        transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y, -1);
    }
}
