using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource backgroundMusicSource;
    public AudioSource soundEffectSource;

    public AudioClip backgroundMusicClip;
    public List<AudioClip> soundEffectsClips;

    private void Start()
    {
        PlayBackgroundMusic();



    }

    public void PlayBackgroundMusic()
    {
        if (backgroundMusicClip != null)
        {
            backgroundMusicSource.clip = backgroundMusicClip;
            backgroundMusicSource.loop = true;
            backgroundMusicSource.Play();
        }
    }

    public void PlaySoundEffect(int index)
    {
        if (IsIndexValid(index, soundEffectsClips.Count))
        {
            soundEffectSource.PlayOneShot(soundEffectsClips[index]);
        }
    }

    public void PlayDrop() => PlaySoundEffect(0);
    public void PlayMerge() => PlaySoundEffect(1);
    public void PlayBtn() => PlaySoundEffect(2);



    public void AdjustBackgroundMusicVolume(float volume)
    {
        backgroundMusicSource.volume = volume;
    }

    public void AdjustSoundEffectsVolume(float volume)
    {
        soundEffectSource.volume = volume;
    }

    private bool IsIndexValid(int index, int arrayLength)
    {
        return index >= 0 && index < arrayLength;
    }
}
