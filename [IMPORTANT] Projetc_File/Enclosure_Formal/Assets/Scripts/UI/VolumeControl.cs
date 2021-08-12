using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float volumeValue = volumeSlider.value/100;
        AllData.volumeValue = volumeValue;
    }
    
}
