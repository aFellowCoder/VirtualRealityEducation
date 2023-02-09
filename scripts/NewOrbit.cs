using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewOrbit : MonoBehaviour
{

  private float gravity = 100f;

  private GameObject earth;
  private GameObject sun;

  public bool velocityReset = false;

  // [SerializeField]
  // float aVal = 4000f;


    // Start is called before the first frame update
    void Start()
    {

      earth = GameObject.Find("Earth");
      sun = GameObject.Find("sun");

      //orbitalSpeed(sun, earth);
      orbitalSpeed(earth, sun, false);



    }

    // Update is called once per frame
    void Update()
    {


    }

    private void FixedUpdate()
    {
      Gravity(earth, sun);

      //Gravity(sun, earth);
      Debug.Log("velocity" + earth.GetComponent<Rigidbody>().velocity);

      if (velocityReset == true)
      {
        orbitalSpeed(earth, sun, true);
        velocityReset = false;
      }

    }

    void Gravity(GameObject planet1, GameObject planet2)
    {

      float massPlanet1 = planet1.GetComponent<Rigidbody>().mass;
      float massPlanet2 = planet2.GetComponent<Rigidbody>().mass;



      float radius = Vector3.Distance(planet1.transform.position, planet2.transform.position);
      Debug.Log("Radius" + radius);
      planet1.GetComponent<Rigidbody>().AddForce((planet2.transform.position - planet1.transform.position).normalized * (gravity * (massPlanet1 * massPlanet2) / (radius * radius)));
    }


    void orbitalSpeed(GameObject planet1, GameObject planet2, bool reset)
    {

      float massPlanet1 = planet1.GetComponent<Rigidbody>().mass;
      float massPlanet2 = planet2.GetComponent<Rigidbody>().mass;

      float aVal = 2500f - (massPlanet1 * 2);
      //float aVal = 2500f;

      float radius = Vector3.Distance(planet1.transform.position, planet2.transform.position);
      planet1.transform.LookAt(planet2.transform);

      //planet1.GetComponent<Rigidbody>().velocity += planet1.transform.right * Mathf.Sqrt((gravity * massPlanet2) / radius);
      //planet1.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
      if (reset == false)
      {
      //planet1.GetComponent<Rigidbody>().velocity += planet1.transform.right * Mathf.Sqrt((gravity*(massPlanet2)) * ((2f / radius) - (1f / aVal)));
      //planet1.GetComponent<Rigidbody>().velocity += new Vector3(0,0,0);
      planet1.GetComponent<Rigidbody>().velocity += planet1.transform.right * 150;
      }
      else
      {
        //planet1.GetComponent<Rigidbody>().velocity = planet1.transform.right * Mathf.Sqrt((gravity*(massPlanet2)) * ((2f / radius) - (1f / aVal)));
        planet1.GetComponent<Rigidbody>().velocity = planet1.transform.right * 150;

      }

    }

}
