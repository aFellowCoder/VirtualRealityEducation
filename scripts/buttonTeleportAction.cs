using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//using System.IO;

public class buttonTeleportAction : MonoBehaviour
{

  [SerializeField] private float threshold = .1f;
  [SerializeField] private float deadZone = 0.025f;

  private bool isPressed;
  private Vector3 startPos;
  private ConfigurableJoint joint;

  private GameObject player, playerCam, FarCam, sun, earth, planetLight;

    public UnityEvent onPressed, onReleased;

    // Start is called before the first frame update
    void Start()
    {
      player = GameObject.Find("XRRig");
      playerCam = GameObject.Find("Main CameraX");
      startPos = transform.localPosition;
      joint = GetComponent<ConfigurableJoint>();

      FarCam = GameObject.Find("MainCameraFarFinal");
      sun  = GameObject.Find("sun");
      earth  = GameObject.Find("Earth");
      planetLight = GameObject.Find("directionalLightPlanet");

      sun.SetActive(false);
      earth.SetActive(false);
      planetLight.SetActive(false);


      //saveData();
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

      playerCam.GetComponent<sphereCast>().enabled = false;

      //Debug.Log("Pressed");
      player.transform.position = new Vector3(1.4f,2507f,504.79f);
      player.transform.eulerAngles = new Vector3(90f,0,0f);
      //playerCam.GetComponent<Camera>().nearClipPlane = 413.2f;
      //playerCam.GetComponent<Camera>().farClipPlane = 3735f;
      FarCam.GetComponent<Camera>().enabled = true;

      sun.SetActive(true);
      earth.SetActive(true);
      planetLight.SetActive(true);


    }

    private void Released()
    {
      isPressed = false;
      onReleased.Invoke();
      //Debug.Log("Released");

    }

    // private void saveData()
    // {
    //
    //   string fname = "userData.txt";
    //   string path = Path.Combine(Application.persistentDataPath, fname);
    //
    //   if (!System.IO.File.Exists(path))
    //   {
    //     using (StreamWriter file = System.IO.File.CreateText(path))
    //     {
    //       file.WriteLine("New Data");
    //       file.WriteLine("New Data");
    //       file.WriteLine("New Data");
    //     }
    //   }
    //
    //   else if (System.IO.File.Exists(path))
    //   {
    //     using (StreamWriter file = System.IO.File.AppendText(path))
    //     {
    //       file.WriteLine("Append Data");
    //       file.WriteLine("Append Data");
    //       file.WriteLine("Append Data");
    //     }
    //   }
    //   Debug.Log("Data Saved!");
    //
    // }

}
