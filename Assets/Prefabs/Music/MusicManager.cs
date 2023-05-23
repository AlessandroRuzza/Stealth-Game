using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;
    public static MusicManager self;

    public float GetVolume()
    {
        return audioSource.volume;
    }

    private void Awake()
    {
        // Singleton implementation
        if (self == null)
        {
            self = this;
            transform.parent = null; // Detaches the GameObject from its parent
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        // Play the music initially
        audioSource.Play();
    }

    public void SetVolume(float volume)
    {
        // Set the volume. The passed volume should be in the range [0, 1]
        audioSource.volume = volume;
    }
}
