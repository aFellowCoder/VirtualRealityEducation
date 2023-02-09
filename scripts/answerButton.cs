using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.IO;
using UnityEngine.UI;

public class answerButton : MonoBehaviour
{

    [SerializeField] private float threshold = .1f;
    [SerializeField] private float deadZone = 0.025f;

    private bool isPressed;
    private Vector3 startPos;
    private ConfigurableJoint joint;


    //private GameObject player, playerCam;
    private GameObject earth, sun;

    public UnityEvent onPressed, onReleased;

    private static float distanceIncrement = 1;

    private string button;

      // Start is called before the first frame update
      void Start()
      {
        //player = GameObject.Find("XRRig");
        //playerCam = GameObject.Find("Main CameraX");


        startPos = transform.localPosition;
        joint = GetComponent<ConfigurableJoint>();


        //earth = GameObject.Find("Earth");
      }


      // Update is called once per frame
      void Update()
      {
        if (!isPressed && GetValue() + threshold >= 1) {
          Pressed();
        }

        if (isPressed && GetValue() - threshold <= 0) {
          Released();
        }
      }


      private float GetValue()
      {
        var value = Vector3.Distance(startPos, transform.localPosition) / joint.linearLimit.limit;

        if (Mathf.Abs(value) < deadZone)
          value = 0;

        return Mathf.Clamp(value, -1f, 1f);
      }

      private void Pressed()
      {
        isPressed = true;
        onPressed.Invoke();
        Debug.Log("Pressed");

        earth = GameObject.Find("Earth");
        sun = GameObject.Find("sun");

        var parentName = transform.parent.name;

        if(string.Compare(parentName, "massButton1") == 0) {
          Debug.Log("massButton1");
          sun.GetComponent<Rigidbody>().mass += 100000f;          //earth.GetComponent<TrailRenderer>().Clear();
          button = "massButton+";
          saveData();

        }

        else if(string.Compare(parentName, "massButton2") == 0) {
          Debug.Log("massButton2");
          sun.GetComponent<Rigidbody>().mass -= 100000f;          //earth.GetComponent<TrailRenderer>().Clear();
          button = "massButton-";
          saveData();
        }

        else if(string.Compare(parentName, "velocityButton1") == 0) {
          //earth.transform.position = new Vector3(0f,0f,2510f);
          //earth.GetComponent<NewOrbit>().velocityReset = true;
          Debug.Log("velocityButton1");

          earth.GetComponent<Rigidbody>().velocity *= 1.1f;
          button = "velocityButton+";
          saveData();
        }

        else if(string.Compare(parentName, "velocityButton2") == 0) {
          //earth.transform.position = new Vector3(0f,0f,2510f);
          //earth.GetComponent<NewOrbit>().velocityReset = true;
          Debug.Log("velocityButton2");
          earth.GetComponent<Rigidbody>().velocity *= 0.5f;
          button = "velocityButton-";
          saveData();
        }

        else if(string.Compare(parentName, "locationButton1") == 0) {
          distanceIncrement += 1;
          earth.transform.position = new Vector3(0f,0f,2510f);
          earth.transform.position += (new Vector3(0f,0f,100f) * distanceIncrement);
          earth.GetComponent<NewOrbit>().velocityReset = true;
          earth.GetComponent<TrailRenderer>().Clear();
          Debug.Log("locationButton1");
          button = "locationButton+";
          saveData();
        }

        else if(string.Compare(parentName, "locationButton2") == 0) {
          distanceIncrement -= 1;
          earth.transform.position = new Vector3(0f,0f,2510f);
          earth.transform.position += (new Vector3(0f,0f,100f) * distanceIncrement);
          earth.GetComponent<NewOrbit>().velocityReset = true;
          earth.GetComponent<TrailRenderer>().Clear();
          button = "locationButton-";
          saveData();


        }

        else if(string.Compare(parentName, "resetEarthButton") == 0) {
          earth.transform.position = new Vector3(0f,0f,2510f);
          earth.GetComponent<NewOrbit>().velocityReset = true;
          distanceIncrement = 1;
          sun.GetComponent<Rigidbody>().mass = 333000f;
          earth.GetComponent<TrailRenderer>().Clear();
          button = "resetButton";
          saveData();
        }


        //earth.GetComponent<Rigidbody>().mass = 600f;
        //earth.transform.position = new Vector3(0f,0f,2510f);

        //earth.GetComponent<NewOrbit>().velocityReset = true;

      }

      private void Released()
      {
        isPressed = false;
        onReleased.Invoke();
        Debug.Log("Released");

      }

      private void saveData()
      {

        string fname = "userDataInteractions.txt";
        string path = Path.Combine(Application.persistentDataPath, fname);

        if (!System.IO.File.Exists(path))
        {
          using (StreamWriter file = System.IO.File.CreateText(path))
          {
            //file.WriteLine("Look Away Count: " + string.Format("{0:N0}", lookAwayCount));
            file.WriteLine(button + "\n");


          }
        }

        else if (System.IO.File.Exists(path))
        {
          using (StreamWriter file = System.IO.File.AppendText(path))
          {
            //file.WriteLine("Look Away Count: " + string.Format("{0:N0}", lookAwayCount));
            file.WriteLine(button + "\n");
          }
        }
        Debug.Log("Data Saved!");

      }
}
