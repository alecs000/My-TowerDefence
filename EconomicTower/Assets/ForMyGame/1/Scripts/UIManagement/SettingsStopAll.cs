using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsStopAll : MonoBehaviour
{
    [SerializeField] AudioMixerGroup Mixer;
    private void OnEnable()
    {
        Time.timeScale = 0;
    }
    private void OnDisable()
    {
        Time.timeScale = 1;
    }
    public void ChangeValume(float Volue)
    {
        Mixer.audioMixer.SetFloat("EffectsVolume", Mathf.Lerp( -80, 0, Volue));
    }
    public void ChangeMusic(float Volue)
    {
        Mixer.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, Volue));
    }
}
