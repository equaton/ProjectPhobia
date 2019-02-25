using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This Script once attached to a gameObject will play Sounds every a random amount of times, at random pitches

public class RandomSoundGenerator : MonoBehaviour
{
    public SoundCollection SoundCollection;         // Reference to the Scriptable object with the Audio Clips
    public AudioSource AudioSource;					// Reference to the Audio Source of the GameObject.
    public float randomTimeMin = 10f;               // Minimum amount of time interval for the random sound to play
    public float randomTimeMax = 20f;               // Maximum amount of time interval for the random sound to play
    public float randomPitchMin = 1f;              // Minimum amount of pitch for the sound to play
    public float randomPitchMax = 1f;              // Maximum amount of pitch for the sound to play

    void Start()
    {
        AudioSource = GetComponentInChildren<AudioSource>();
        StartCoroutine(PlaySound());
    }


    IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(Random.Range(randomTimeMin, randomTimeMax));

        AudioSource.clip = SoundCollection.soundCollection[Random.Range(0, SoundCollection.soundCollection.Count)];
        AudioSource.pitch = Random.Range(randomPitchMin, randomPitchMax);
        AudioSource.PlayOneShot(AudioSource.clip);

        yield return new WaitForSeconds(AudioSource.clip.length);
        StartCoroutine(PlaySound());

    }
}

