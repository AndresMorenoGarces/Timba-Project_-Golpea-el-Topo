using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip hitSound;
    public AudioClip appearSound;
    public AudioClip winSound;
    public AudioSource audioSource;
    public void PlayHit() 
    {
        audioSource.PlayOneShot(hitSound);
    }
    public void AppearSound() 
    {
        audioSource.PlayOneShot(appearSound);
    }
    public void WinSound() 
    {
        audioSource.PlayOneShot(winSound);
    }
}