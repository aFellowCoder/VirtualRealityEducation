using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttons : MonoBehaviour
{

  float activateVal = 0.005f;
  private GameObject button;
  private GameObject buttonTop;
  private ConfigurableJoint buttonJoint;
  private float buttonLocation;
  bool buttonPush = false;
 
    void Start()
    {

      buttonTop = GameObject.Find("button");
      buttonLocation = buttonTop.transform.Find("Push").transform.position.y;
      Debug.Log("buttonLocation" + buttonLocation);

    }

    // Update is called once per frame
    void Update()
    {
      float currentLocation = buttonTop.transform.Find("Push").transform.position.y;
      //Debug.Log("buttonLocation" + currentLocation);
      Debug.Log("Difference" + (buttonLocation - currentLocation));

      if (((buttonLocation - currentLocation)) > activateVal && buttonPush == false){
        Debug.Log("Pressed");
        buttonPush = true;
      }

      if (((buttonLocation - currentLocation)) < (activateVal) && buttonPush == true){
        Debug.Log("Not Pressed");
        buttonPush = false;
      }

    }
}
