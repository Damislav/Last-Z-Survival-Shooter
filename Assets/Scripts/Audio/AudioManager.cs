using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] AudioSource audioSource;

    [SerializeField] private AudioClip gunFire;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play()
    {
        audioSource.PlayOneShot(gunFire);
    }
}
