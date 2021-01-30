using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager snd;

    private AudioSource audioSrc;

    private AudioClip[] DieSounds;
    private AudioClip[] JumpSounds;
    private AudioClip[] PointSounds;

    void Start()
    {
        snd = this;
        audioSrc = GetComponent<AudioSource>();
        DieSounds = Resources.LoadAll<AudioClip>("DieSounds");
        JumpSounds = Resources.LoadAll<AudioClip>("JumpSounds");
        PointSounds = Resources.LoadAll<AudioClip>("PointSounds");
    }

    public void PlayDieSounds()
    {
        audioSrc.PlayOneShot(DieSounds[Random.Range(0,DieSounds.Length)]);
    }

    public void PlayJumpSounds()
    {
        audioSrc.PlayOneShot(JumpSounds[Random.Range(0, JumpSounds.Length)]);
    }
    public void PlayPointsSounds()
    {
        audioSrc.PlayOneShot(PointSounds[Random.Range(0, PointSounds.Length)]);
    }

}
