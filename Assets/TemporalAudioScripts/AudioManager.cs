using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance; // Instancia est�tica del AudioManager

    [SerializeField] private List<AudioClip> audioClips = new List<AudioClip>(); // Lista de AudioClips
    private AudioSource audioSource;

    // Propiedad est�tica para acceder a la instancia del AudioManager
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if (instance == null)
                {
                    // Si no se encuentra una instancia en la escena, crea un objeto AudioManager
                    GameObject audioManagerObject = new GameObject("AudioManager");
                    instance = audioManagerObject.AddComponent<AudioManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Si ya existe una instancia, destruye este objeto
            Destroy(gameObject);
        }

        // Inicializa el AudioSource
        audioSource = GetComponent<AudioSource>();
    }

    // M�todo para reproducir un efecto de sonido con un �ndice espec�fico y volumen
    public void PlaySound(int index, float volume)
    {
        if (index >= 0 && index < audioClips.Count)
        {
            audioSource.PlayOneShot(audioClips[index], volume);
        }
        else
        {
            Debug.LogError("Index out of range: " + index);
        }
    }
}