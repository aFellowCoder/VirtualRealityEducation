using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthOrbitAroundSun : MonoBehaviour
{
  [SerializeField]
  float speed = 10f;


  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    var Sun = GameObject.Find("sunSmall5");
    this.transform.RotateAround(Sun.transform.position, Vector3.left,
      speed * Time.deltaTime);
  }
}
