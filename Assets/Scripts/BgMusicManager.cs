using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMusicManager : MonoBehaviour
{
    public AudioSource source;
    public List<AudioClip> bgMusic;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        var randomBgMusic = bgMusic[Random.Range(0, bgMusic.Count)];
        source.PlayOneShot(randomBgMusic);
    }
    private void Update()
    {
        if(!source.isPlaying)
        {
            var randomBgMusic = bgMusic[Random.Range(0, bgMusic.Count)];
            source.PlayOneShot(randomBgMusic);
        }
    }
}
