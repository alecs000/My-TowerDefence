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
    private void Start()
    {
        float saveEffect = PlayerPrefs.GetFloat("Effect");
        float saveMusic = PlayerPrefs.GetFloat("Music");
        int change = PlayerPrefs.GetInt("Changed");
        if (saveEffect==0&& change==0)
        {
            saveEffect = 1;
        }
        if (saveMusic == 0 && change==0)
        {
            saveMusic = 1;
        }
        effectsSlider.value = saveEffect;
        Mixer.audioMixer.SetFloat("EffectsVolume", Mathf.Lerp(-80, 0, saveEffect));
        musicSlider.value = saveMusic;
        Mixer.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, saveMusic));
    }
    private void OnEnable()
    {
        Time.timeScale = 0;
        musicSlider.value = music;
        effectsSlider.value = effects;
    }
    private void OnDisable()
    {
        PlayerPrefs.SetFloat("Effect", effects);
        PlayerPrefs.SetFloat("Music", music);
        Time.timeScale = 1;
    }
    private void Update()
    {
        Mixer.audioMixer.SetFloat("EffectsVolume", Mathf.Lerp(-80, 0, effects));
        Mixer.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, music));
    }
    public void ChangeValume(float Volue)
    {
        PlayerPrefs.SetInt("Changed", 1);
        effects = Volue;
    }
    public void ChangeMusic(float Volue)
    {
        PlayerPrefs.SetInt("Changed", 1);
        music = Volue;
    }
}
