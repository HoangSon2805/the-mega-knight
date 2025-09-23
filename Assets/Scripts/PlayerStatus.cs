using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerStatus : MonoBehaviour {
    public bool IsPowered { get; private set; }
    //[SerializeField] private ParticleSystem auraParticles;
    [SerializeField] private Light2D auraLight;

    void Awake() {
        //if (auraParticles) auraParticles.gameObject.SetActive(false);
        if (auraLight) auraLight.gameObject.SetActive(false);
    }

    public void PowerUp() {
        IsPowered = true;
        //if (auraParticles) auraParticles.gameObject.SetActive(true);
        if (auraLight) auraLight.gameObject.SetActive(true);

        var audio = FindAnyObjectByType<AudioManager>();
        if (audio) audio.PlayPowerUpSound();

        Debug.Log("[PlayerStatus] Player powered up!");
    }
}