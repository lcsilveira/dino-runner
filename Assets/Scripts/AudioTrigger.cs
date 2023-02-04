using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Slider sliderVolume;

    private void Start()
    {
        SetVolume();
    }

    public void SetVolume()
    {
        audioSource.volume = sliderVolume.value;
    }
}
