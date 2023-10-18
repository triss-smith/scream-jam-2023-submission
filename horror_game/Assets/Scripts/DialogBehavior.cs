using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogBehavior : MonoBehaviour
{
    public static bool textActive;
    public TextMeshProUGUI textDisplay;
    public string[] lines;
    public float textSpeed;

    private int index;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        textActive = false;
        textDisplay.text = "";
        startDialog();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            if (textDisplay.text == lines[index]) {
                nextLine();
            } else {
                StopAllCoroutines();
                textDisplay.text = lines[index];
            }
        }
    }

    void startDialog() 
    {
        index = 0;
        StartCoroutine(typeLine());
    }

    IEnumerator typeLine() {
        foreach (char letter in lines[index].ToCharArray()) {
            textDisplay.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void nextLine() {
        if (index < lines.Length - 1) {
            index++;
            textDisplay.text = "";
            StartCoroutine(typeLine());
        } else {
            textDisplay.text = "";
            gameObject.SetActive(false);
        }
    }
}
