using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BgMusicManager : MonoBehaviour
{
    public AudioSource source;
    public List<AudioClip> bgMusic;
    public float playingTime;
    private void Start()
    {
        var randomBgMusic = bgMusic[Random.Range(0, bgMusic.Count)];
        source.clip = randomBgMusic;
        source.Play();
    }
    private void Update()
    {
        if(source.isPlaying)
        {
            playingTime += Time.deltaTime;
        }
    }
}
