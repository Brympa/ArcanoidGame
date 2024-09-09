using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager MusicInstance;

    [SerializeField] private AudioSource _audioSource;

    private void Awake()
    {
        if (MusicInstance == null)
        {
            MusicInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic()
    {
        _audioSource.Play();
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }
}
