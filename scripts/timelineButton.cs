using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using System.IO;
using UnityEngine.UI;

public class timelineButton : MonoBehaviour
{

  [SerializeField] private float threshold = .1f;
  [SerializeField] private float deadZone = 0.025f;

  private bool isPressed;
  private Vector3 startPos;
  private ConfigurableJoint joint;

  private PlayableDirector timeline;
  public UnityEvent onPressed, onReleased;

  private float lookAwayCount;
  private float timeSpentWatchingLesson = 0;
  private bool isItPlaying;

  private GameObject sun;

    // Start is called before the first frame update
    void Start()
    {
      startPos = transform.localPosition;
      joint = GetComponent<ConfigurableJoint>();

      timeline = GameObject.Find("Timeline").GetComponent<PlayableDirector>();

      //sun  = GameObject.Find("sun");

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


      if (isItPlaying) {
        timeSpentWatchingLesson += Time.deltaTime;
        //Debug.Log("Playing");
        sun  = GameObject.Find("sun");
        if (sun.activeSelf) {
          saveData();
          //Debug.Log("Found sun active");
          isItPlaying = false;
          timeline.Stop();
        }
      }
      //Debug.Log("State " + isItPlaying);
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
      timeline.Play();
      //Debug.Log("Started");

      var parentName = transform.parent.name;

      if(string.Compare(parentName, "timelineButtonPlay") == 0) {
        timeline.Play();
        isItPlaying = true;
        //Debug.Log("timelineButton");

      }


      else if(string.Compare(parentName, "timelineButtonResumeTemp") == 0) {
        timeline.playableGraph.GetRootPlayable(0).SetSpeed(1);
        isItPlaying = false;
        //Debug.Log("timelineButtonRestart");

      }

      else if(string.Compare(parentName, "timelineButtonPauseTemp") == 0) {
        timeline.playableGraph.GetRootPlayable(0).SetSpeed(0);
        isItPlaying = false;
        //Debug.Log("timelineButtonRestart");

      }

      else if(string.Compare(parentName, "timelineButtonStopTemp") == 0) {
        timeline.Stop();
        isItPlaying = false;
        //Debug.Log("timelineButtonRestart");

      }

    }

    private void Released()
    {
      isPressed = false;
      onReleased.Invoke();
      Debug.Log("Released");

    }

    private void saveData()
    {

      string fname = "userDataTimeSpent.txt";
      string path = Path.Combine(Application.persistentDataPath, fname);

      if (!System.IO.File.Exists(path))
      {
        using (StreamWriter file = System.IO.File.CreateText(path))
        {
          //file.WriteLine("Look Away Count: " + string.Format("{0:N0}", lookAwayCount));
          file.WriteLine("Time spent in lesson area: " + string.Format("{0:N5}", timeSpentWatchingLesson) + "\n");


        }
      }

      else if (System.IO.File.Exists(path))
      {
        using (StreamWriter file = System.IO.File.AppendText(path))
        {
          //file.WriteLine("Look Away Count: " + string.Format("{0:N0}", lookAwayCount));
          file.WriteLine("Time spent in lesson area: " + string.Format("{0:N5}", timeSpentWatchingLesson) + "\n");
        }
      }
      Debug.Log("Data Saved!");

    }



}
