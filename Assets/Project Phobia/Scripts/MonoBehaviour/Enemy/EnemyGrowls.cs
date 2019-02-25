using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrowls : MonoBehaviour
{
    public SoundCollection EnemyGrowlSounds;              // Reference to the Scriptable object with the Enemz Growls
    public AudioSource EnemyAudio;					// Reference to the Audio Source of the Enemy.

    void Start()
    {
        EnemyAudio = GetComponentInChildren<AudioSource>();
        StartCoroutine(PlaySound());
    }
 

    IEnumerator PlaySound()
    {
        Debug.Log("Startting corutire");
        yield return new WaitForSeconds(Random.Range(10f, 20f));
        Debug.Log("Startting startiing sound");

        EnemyAudio.clip = EnemyGrowlSounds.soundCollection[Random.Range(0, EnemyGrowlSounds.soundCollection.Count)];
            EnemyAudio.PlayOneShot(EnemyAudio.clip);
        
        yield return new WaitForSeconds(  EnemyAudio.clip.length);
        StartCoroutine(PlaySound());

    }
}