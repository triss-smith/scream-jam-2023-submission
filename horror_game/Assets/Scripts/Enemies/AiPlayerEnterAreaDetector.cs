using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SVS.AI
{
public class AiPlayerEnterAreaDetector : MonoBehaviour
{

 public record AiPlayerEnterAreaDetector(bool PlayerInArea, Transform player) : MonoBehaviour
{
    [field: SerializeField]
    public bool PlayerInArea { get; private set; } = PlayerInArea;
    public Transform player { get; private set; } = player;

    [SerializeField]
    private string dectionTag = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(dectionTag))
        {
            PlayerInArea = true;
            player = collision.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collder2D collision)
    {
        if (collision.CompareTag(dectionTag))
        {
            PlayerInArea = false;
            player = null;
        }
    }
}
}
}
