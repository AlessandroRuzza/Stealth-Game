using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    public Slider volumeSlider;
    void Start()
    {
        volumeSlider.value = MusicManager.self.GetVolume();
    }

    // Update is called once per frame
    public void UpdateVolume()
    {
        MusicManager.self.SetVolume(volumeSlider.value);
    }
}
