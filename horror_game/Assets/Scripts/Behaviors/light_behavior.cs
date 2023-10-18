using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class light_behavior : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float flashlightRange = 4.5f;
    [SerializeField] float flashlightBrightRange = 10f;
    [SerializeField] float flashlightInnerAngle = 40;
    [SerializeField] float flashlightOuterAngle = 70;
    float FlashlightBattery = 100f;
    bool FlashLightBurning = false;

    Light2D flashlight;

    void Start()
    {
        flashlight = GetComponent<Light2D>();
        flashlight.pointLightInnerAngle = flashlightInnerAngle;
        flashlight.pointLightOuterAngle = flashlightOuterAngle;
    }

    // Update is called once per frame
    void Update()
    {
        // set the Radius of the flashlight
        FlashLightOn();
        
    }

    void FlashLightOn()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && FlashlightBattery > 0)
        {
            Debug.Log("fire");
            FlashLightBurning = true;
            flashlight.pointLightOuterRadius = flashlightBrightRange;
        } 

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Debug.Log("stop fire");
            FlashLightBurning = false;
        }

        if (FlashLightBurning) {
            StartCoroutine(DepleteFlashlight());

        } else {
            StopCoroutine(DepleteFlashlight());
            flashlight.pointLightOuterRadius = flashlightRange;
        }
    }

    IEnumerator DepleteFlashlight()
        {
            while (FlashLightBurning && FlashlightBattery > 0)
            {
                FlashlightBattery -= 0.5f;
                if (flashlightRange > 2.0) {
                    flashlightRange -= 0.01f;
                }
                yield return new WaitForSeconds(0.5f);
            }
        }
}
