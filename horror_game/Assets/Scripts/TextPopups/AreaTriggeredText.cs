using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaTriggeredText : MonoBehaviour
{
    public GameObject UIObject;
    public GameObject trigger;
    // Start is called before the first frame update
    void Start()
    {
        UIObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            UIObject.SetActive(true);
            print("working");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        UIObject.SetActive(false);
        Destroy(trigger);
    }
}
