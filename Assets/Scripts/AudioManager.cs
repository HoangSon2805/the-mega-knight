using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundAudioSource;
    [SerializeField] private AudioSource effectAudioSource;

    [SerializeField] private AudioClip backGroundClip;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip coinClip;
    [SerializeField] private AudioClip powerupAppearClip;
    [SerializeField] private AudioClip powerUpClip;
    [SerializeField] private AudioClip swordCollectClip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayBackGroundMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayBackGroundMusic() { 
        backgroundAudioSource.clip = backGroundClip;
        backgroundAudioSource.Play();
    }
    public void PlayCoinSound() { 
        effectAudioSource.PlayOneShot(coinClip);
    }

    public void PlayJumpSound() { 
        effectAudioSource.PlayOneShot(jumpClip);
    }
    public void PlayPowerupAppearSound() {
        effectAudioSource.PlayOneShot(powerupAppearClip);
    }
    public void PlayPowerUpSound() {
        effectAudioSource.PlayOneShot(powerUpClip);
    }
    public void PlaySwordCollectSound() {
        effectAudioSource.PlayOneShot(swordCollectClip);
    }
}
