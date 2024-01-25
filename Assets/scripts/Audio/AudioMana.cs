using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMana : PersistentSingleton<AudioMana>
{
    [SerializeField] AudioSource SfxPlayer;
    [SerializeField] float MaxPit;
    [SerializeField] float MinPit;
    public void playSFX(AudioData data)
    {
        SfxPlayer.PlayOneShot(data.clip,data.volume);
    }
    public void playSFXRandomly(AudioData data)
    {
        SfxPlayer.pitch = Random.Range(MinPit, MaxPit);
        SfxPlayer.PlayOneShot(data.clip,data.volume);
    }
    public void playSFXRandomly(AudioData[] data)
    {
        var i = Random.Range(0, data.Length);
        SfxPlayer.pitch = Random.Range(MinPit, MaxPit);
        SfxPlayer.PlayOneShot(data[i].clip, data[i].volume);
    }
}

[System.Serializable] public class AudioData
{
    public AudioClip clip;
    public float volume;
}
