using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip giveCoin;
    [SerializeField] private AudioClip losePlayer;
    [SerializeField] private AudioClip winPlayer;

    public enum typeClips { Give, Lose, Win}

    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(typeClips clip)
    {
        switch (clip)
        {
            case typeClips.Give:
                audioSource.PlayOneShot(giveCoin);
                break;
            case typeClips.Win:
                audioSource.Stop();
                audioSource.PlayOneShot(winPlayer);
                break;
            case typeClips.Lose:
                audioSource.Stop();
                audioSource.PlayOneShot(losePlayer);
                break;
        }
    }
}
