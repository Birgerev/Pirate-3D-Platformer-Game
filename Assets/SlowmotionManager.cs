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
        
    }

    public void SetTimeScale(float newTimeScale)
    {
        Time.timeScale = newTimeScale;

        float volumeWeight = 0;
        if (newTimeScale < timeScaleVolumeApex)
            volumeWeight = newTimeScale/timeScaleVolumeApex;

        _volume.weight = volumeWeight;
    }
}
