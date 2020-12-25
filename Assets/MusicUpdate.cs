using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicUpdate : MonoBehaviour
{   
    //we are using this script to make the music volumne slider work 
    List<AudioSource> music = new List<AudioSource>(); //making a list of music sounds
    // Start is called before the first frame update
    public void Start()
    {
        AudioSource[] allAS = GameObject.FindWithTag("gamedata").GetComponentsInChildren<AudioSource>();
        music.Add(allAS[0]);
        Slider musicSlider = this.GetComponent<Slider>();//getting music slider from UI

        if (PlayerPrefs.HasKey("musicvolume"))//if key  exists then we use it to change slider's value
        {
            musicSlider.value = PlayerPrefs.GetFloat("musicvolume");
            UpdateMusicVolume(musicSlider.value);
        }
        else
        {
            musicSlider.value = 1; //if music hasn't been set already then set it to full 1st time 
            UpdateMusicVolume(1);
        }
    }
    //setting music in dictionary
    public void UpdateMusicVolume(float value)
    {
        PlayerPrefs.SetFloat("musicvolume", value);
        foreach(AudioSource m in music)
        {
            m.volume = value; 
        }
    }
}
