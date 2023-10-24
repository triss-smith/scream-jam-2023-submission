using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Additional game over logic (e.g., play an animation or sound) can be added here

            // Load the "GameOver" scene
            SceneManager.LoadScene("GameOver");
        }
    }
}
