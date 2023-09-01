using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{

    public Slider gauge;
    public float gauge_amount;
    public float gauge_amount_float;

    // Start is called before the first frame update
    void Start()
    {
        gauge_amount = 100f;
        gauge.GetComponent<Slider>().value = gauge_amount;
        gauge_amount_float = 0.02f;
    }

    // Update is called once per frame
    void Update()
    {
        GaugeManagement();
        
    }

    public void GaugeManagement()
    {
        if(gauge_amount != 0)
        {
            gauge_amount -= gauge_amount_float;
            gauge.GetComponent<Slider>().value = gauge_amount;
        }
        else { }
        
    }

}
