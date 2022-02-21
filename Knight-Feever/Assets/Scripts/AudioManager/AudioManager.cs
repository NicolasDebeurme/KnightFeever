using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class SoundData
    {
        public string Name;
        public AudioClip[] Clips;
    }
    public SoundData[] Data;

    public float VolumeMusic = 30f;
    float VolumeFootStep = 30f;
    public static AudioManager Instance;

    private void Awake()
    {
        Instance = this;
        
    }
    public GameObject PlayMusic(string name)
    {
        SoundData Data =GetSoundData(name);
        AudioClip clip = Data.Clips[0];
        if (clip != null)
        {
            GameObject go = new GameObject();
            go.name = name;
            AudioSource source = go.AddComponent<AudioSource>();
            source.clip = clip;
            source.volume = VolumeMusic;
            source.loop = true;
            source.Play();
            return go;
        }
        else
            return null;
    }

    public void PlaySound(string name)
    {
        SoundData Data = GetSoundData(name);
        AudioClip clip = GetRandomClip(Data);
        if (clip != null)
        {
            VolumeFootStep = UnityEngine.Random.Range(0.05f, 0.1f);
            GameObject go = new GameObject();
            go.name = name;
            AudioSource source = go.AddComponent<AudioSource>();
            source.clip = clip;
            source.volume = VolumeFootStep;
            source.Play();
            GameObject.Destroy(go, source.clip.length);
        }
    }
    public AudioClip GetRandomClip(SoundData Data)
    {
        int rnd = UnityEngine.Random.Range(0, Data.Clips.Length);
        return Data.Clips[rnd];
    }

    public SoundData GetSoundData(string name)
    {
        foreach (var item in Data)
        {
            if (item.Name == name)
                return item;
        }
        return null;
    }
}
