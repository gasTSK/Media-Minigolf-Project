using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    public AudioSource menuMusic;
    public AudioSource[] levelTracks;

    private bool levelMusicPlaying;
    private int currentTrack;

    public AudioSource[] sfx;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(levelMusicPlaying == true)
        {
            if (levelTracks[currentTrack].isPlaying == false)
            {
                currentTrack++;

                if(currentTrack >= levelTracks.Length)
                {
                    currentTrack = 0;
                }

                levelTracks[currentTrack].Play();
            }
        }
    }

    public void PlayMenuMusic()
    {
        menuMusic.Play();

        levelMusicPlaying = false;

        levelTracks[currentTrack].Stop();
    }

    public void PlayLevelMusic()
    {
        menuMusic.Stop();

        levelMusicPlaying = true;

        if (levelTracks[currentTrack].isPlaying == false)
        {
            levelTracks[currentTrack].Play();
        }
    }

    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Stop();
        sfx[sfxToPlay].Play();
    }
}
