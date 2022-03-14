using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsStopAll : MonoBehaviour
{
    [SerializeField] AudioMixerGroup Mixer;
    static float music = 1;
    static float effects = 1;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider effectsSlider;
    private void OnEnable()
    {
        Time.timeScale = 0;
        musicSlider.value = music;
        effectsSlider.value = effects;
    }
    private void OnDisable()
    {
        Time.timeScale = 1;
    }
    private void Update()
    {
        Mixer.audioMixer.SetFloat("EffectsVolume", Mathf.Lerp(-80, 0, effects));
        Mixer.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, music));
    }
    public void ChangeValume(float Volue)
    {
        effects = Volue;
    }
    public void ChangeMusic(float Volue)
    {
        music = Volue;
    }
}
