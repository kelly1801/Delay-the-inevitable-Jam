using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingScreen : MonoBehaviour
{
  
    private UIDocument uiDocument;
    private Slider musicSlider;
    private Slider effectsSlider;

    [SerializeField] AudioSource musicSource;
    public AudioSource effectsSource;

  

    void OnEnable()
    {
        // Initialize the Button variable and add a click event listener
        uiDocument = GetComponent<UIDocument>();
        musicSlider = uiDocument.rootVisualElement.Q<Slider>("MusicSlider");
        effectsSlider = uiDocument.rootVisualElement.Q<Slider>("EffectsSlider");

        musicSlider.value = musicSource.volume;

        // Bind the function to the slider's valueChanged event
        musicSlider.RegisterValueChangedCallback(evt => UpdateMusicVolume(musicSource,evt.newValue));
        effectsSlider.RegisterValueChangedCallback(evt => UpdateMusicVolume(effectsSource,evt.newValue));

    }

    public void UpdateMusicVolume(AudioSource audioSource, float newVolume)
    {
        audioSource.volume = newVolume;
    }
    void OnDisable()
    {
        // Remove event listeners
        musicSlider.UnregisterValueChangedCallback(evt => UpdateMusicVolume(musicSource,evt.newValue));
        effectsSlider.UnregisterValueChangedCallback(evt => UpdateMusicVolume(effectsSource, evt.newValue));
    }

}

