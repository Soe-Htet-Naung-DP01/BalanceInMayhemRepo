using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GlobalLightController : MonoBehaviour
{
    Light globalLight;
    float Timer;
    public float darkTime;
    // Start is called before the first frame update
    void Start()
    {
        globalLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GoDark()
    {

    }
}
