using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSourceOptimization : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioListener audioListener;
    private float distanceFromPlayer;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioListener = Camera.main.GetComponent<AudioListener>();
    }

    private void Update()
    {
        distanceFromPlayer = Vector3.Distance(transform.position, audioListener.transform.position);

        ToggleAudioSource(distanceFromPlayer <= audioSource.maxDistance);
    }

    private void ToggleAudioSource(bool isAudible)
    {
        if (!isAudible && audioSource.isPlaying)
        {
            audioSource.Pause();
        }
        else if (isAudible && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
