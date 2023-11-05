using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource music;
    public AudioClip intro;
    public AudioClip loop;
    public AudioClip final;
    public JogoManagement jm;
    // Start is called before the first frame update
    void Start()
    {
        jm = FindObjectOfType<JogoManagement>();
        music.clip = intro;
        music.loop = false;
        music.Play();
       
    }

    void Update()
    {
        if(music.clip == intro && music.isPlaying == false)
        {
            music.clip = loop;
            music.loop = true;
            music.Play();
        }
        if (jm.P1_HP <= 20 || jm.P2_HP <= 20)
        {
            music.loop = false;
            if (music.isPlaying == false)
            {
                music.clip = final;
                music.Play();
            }
        }
    }
}
    

    

