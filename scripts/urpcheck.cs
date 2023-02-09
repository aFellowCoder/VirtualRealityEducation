using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class urpcheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(UnityEngine.Rendering.GraphicsSettings.renderPipelineAsset.GetType().Name);
    }
}
