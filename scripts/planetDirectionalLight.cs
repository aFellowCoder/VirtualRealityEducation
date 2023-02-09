using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planetDirectionalLight : MonoBehaviour
{
    // Start is called before the first frame update


    private GameObject earth;
    private GameObject light;
    void Start()
    {
      earth = GameObject.Find("Earth");
      light = GameObject.Find("directionalLightPlanet");



    }

    // Update is called once per frame
    void Update()
    {

      light.transform.LookAt(earth.transform);

    }
}
