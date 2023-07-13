using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SlowmotionManager : MonoBehaviour
{
    public static SlowmotionManager instance;

    public float timeScaleVolumeApex;
    
    
    private PostProcessVolume _volume;
    
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        _volume = GetComponent<PostProcessVolume>();
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.X))
            SetTimeScale(.2f);
        if(Input.GetKey(KeyCode.C))
            SetTimeScale(1);
    }

    public void SetTimeScale(float newTimeScale)
    {
        
        //TODO smooth
        Time.timeScale = newTimeScale;

        float volumeWeight = 0;
        if (newTimeScale < 1)
            volumeWeight = timeScaleVolumeApex/newTimeScale;

        _volume.weight = volumeWeight;
        
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
    }
}
