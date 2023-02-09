using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class answerButtonVelocity : MonoBehaviour
{

  float activateVal = 0.005f;
  private GameObject button, sun, earth;
  private GameObject buttonTop;
  private ConfigurableJoint buttonJoint;
  private float buttonLocation;
  bool buttonPush = false;
 
    void Start()
    {
      sun = GameObject.Find("sun");
      earth = GameObject.Find("Earth");
      buttonTop = this.transform.parent.gameObject;
      buttonLocation = buttonTop.transform.Find("Push").transform.position.y;
      Debug.Log("buttonLocation" + buttonLocation);

    }

    // Update is called once per frame
    void Update()
    {
      float currentLocation = buttonTop.transform.Find("Push").transform.position.y;
      //Debug.Log("buttonLocation" + currentLocation);
      //Debug.Log("Difference" + (buttonLocation - currentLocation));
      var parentName = transform.parent.name;
      if (((buttonLocation - currentLocation)) > activateVal && buttonPush == false){
        Debug.Log("Pressed");
        buttonPush = true;


      if(string.Compare(parentName, "velocityButton1") == 0) {
          //earth.transform.position = new Vector3(0f,0f,2510f);
          //earth.GetComponent<NewOrbit>().velocityReset = true;
              Debug.Log("velocityButton1");
              earth.GetComponent<Rigidbody>().velocity *= 1.1f;
              //Debug.Log("Test");
        }

      else if(string.Compare(parentName, "velocityButton2") == 0) {
                  //earth.transform.position = new Vector3(0f,0f,2510f);
                  //earth.GetComponent<NewOrbit>().velocityReset = true;
                  Debug.Log("velocityButton2");
                  earth.GetComponent<Rigidbody>().velocity *= 0.5f;
                }

      }

      if (((buttonLocation - currentLocation)) < (activateVal) && buttonPush == true){
        Debug.Log("Not Pressed");
        buttonPush = false;
      }

    }
}
