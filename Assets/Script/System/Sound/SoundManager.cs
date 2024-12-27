using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    public Slider mySlider;
    public AudioSource myAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        // myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        myAudioSource.volume = mySlider.value;
    }
}
