using UnityEngine;
public class AudioManager : MonoBehaviour {
    private AudioClip hitSound;
    private AudioClip appearSound;
    private AudioClip winSound;
    public AudioSource audioSource;
    private void Awake() {
        hitSound = SoundsLibrary("hitSound");
        appearSound= SoundsLibrary("appearSound");
        winSound= SoundsLibrary("winSound");
    }
    public void PlayHit() {
        audioSource.PlayOneShot(hitSound);
    }
    public void AppearSound() {
        audioSource.PlayOneShot(appearSound);
    }
    public void WinSound() {
        audioSource.PlayOneShot(winSound);
    }
    private AudioClip SoundsLibrary(string audioClipName) {
        return (AudioClip)Resources.Load("Sounds/" + audioClipName, typeof(AudioClip));
    }
}