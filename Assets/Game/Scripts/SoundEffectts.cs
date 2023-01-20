using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEffectts : MonoBehaviour
{
    [SerializeField] private AudioClip paddleClip, wallClip, scoreClip;
    AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayPaddle() => source.PlayOneShot(paddleClip);
    public void PlayWall() => source.PlayOneShot(wallClip);
    public void PlayScore() => source.PlayOneShot(scoreClip);
}
