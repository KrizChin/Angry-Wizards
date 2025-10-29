using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// This script will make all buttons have the same sfx

[RequireComponent(typeof(Button))]
public class UIButtonSFX : MonoBehaviour, IPointerClickHandler
{
    public AudioClip clickSFX;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();

        if (audioSource == null)
        {
            GameObject audioObj = new GameObject("UIAudioSource");
            audioSource = audioObj.AddComponent<AudioSource>();
            DontDestroyOnLoad(audioObj);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickSFX != null)
        {
            audioSource.pitch = Random.Range(0.9f,1.1f);
            audioSource.PlayOneShot(clickSFX);
        }
    }
}
