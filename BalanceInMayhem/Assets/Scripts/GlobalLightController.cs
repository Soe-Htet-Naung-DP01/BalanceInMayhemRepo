using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class GlobalLightController : MonoBehaviour
{
    Light2D globalLight;
    bool isPlayerInRange = false;
    void Start()
    {
        globalLight = GetComponent<Light2D>();
    }
    private void Update()
    {
        GoDark();
    }

    //Reduce light intensity of global light to 0 with the rate of 0.03f per sec
    void GoDark()
    {
        if (isPlayerInRange == false)
        {
            if (globalLight.intensity > 0)
            {
                globalLight.intensity -= 0.03f * Time.deltaTime;
            }
            else if (globalLight.intensity <= 0)
            {
                globalLight.intensity = 0;
            }
        }
    }

    //if player is in range, global light intensity goes up to 1 with the rate of 0.05f.
    private void OnTriggerStay2D(Collider2D colstay)
    {
        if (colstay.tag == "Player")
        {
            isPlayerInRange = true;
            if (globalLight.intensity < 1)
            {
                globalLight.intensity += 0.05f * Time.deltaTime;
            }
            else if (globalLight.intensity >= 1)
            {
                globalLight.intensity = 1;
            }
        }
    }

    //when player leave the range, go dark.
    private void OnTriggerExit2D(Collider2D colex)
    {
        if(colex.tag == "Player")
        {
            isPlayerInRange = false;
        }
    }

}
