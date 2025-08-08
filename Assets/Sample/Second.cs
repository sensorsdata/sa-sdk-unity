using System.Collections;
using System.Collections.Generic;
using SensorsAnalytics;
using UnityEngine;

public class Second : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SensorsDataAPI.Track("View_Second_Scene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
