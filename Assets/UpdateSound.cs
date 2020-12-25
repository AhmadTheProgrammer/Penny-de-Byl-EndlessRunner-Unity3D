using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateSound : MonoBehaviour
{ 
    List<AudioSource> sfx = new List<AudioSource>();

    // Start is called before the first frame update
     public void Start()
    {
        AudioSource[] allAS = GameObject.FindWithTag("gamedata").GetComponentsInChildren<AudioSource>();
        //adding all sounds into the list
        for(int i=1; i < allAS.Length; i++)
        {
            sfx.Add(allAS[i]);
        }
       // sfx.Add(allAS[1]);
        Slider sfxSlider = this.GetComponent<Slider>();

        //making the slider work 
        if (PlayerPrefs.HasKey("sfxvolume"))
        {
            sfxSlider.value = PlayerPrefs.GetFloat("sfxvolume");
            UpdateSoundVolume(sfxSlider.value);
        }
        else
        {
            sfxSlider.value = 1;
            UpdateSoundVolume(1);
        }
    }

     
    public void UpdateSoundVolume(float value)
    {
        PlayerPrefs.SetFloat("sfxvolume", value);
        foreach (AudioSource s in sfx)
        {
            s.volume = value;
        }
    }
}
