using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]AudioSource pushItToTheLimit;
    [SerializeField]AudioSource Ambience;

    private void Start()
    {
        Ambience.Play();
    }

    private void Update()
    {
        if (PlayerStats._player.GetComponent<PlayerMovement>().enabled == false && pushItToTheLimit.isPlaying == false)
        {
            pushItToTheLimit.Play();
        }
        else if(pushItToTheLimit.isPlaying ==true && PlayerStats._player.GetComponent<PlayerMovement>().enabled == true)
        {
            pushItToTheLimit.Pause();
        }
     
    }
}
