using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip[] backgroundMusic;

    public int songIndex;

    public bool shuffle;
    public bool skipSong;
    public bool loop;
    public bool dockLocked;

    public GameObject musicPlayer;

    public Button dockButton;
    public Button shuffleButton;
    public Button loopButton;

    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        StartCoroutine(PlayAllTracks());
    }

    // Update is called once per frame
    void Update()
    {

    }



    IEnumerator PlayAllTracks()
    {
        if (!skipSong && !loop) songIndex = Random.Range(0, backgroundMusic.Length);
        skipSong = false;

        for (int i = songIndex; i < backgroundMusic.Length; i += 0)
        {
            source.clip = backgroundMusic[songIndex];
            source.Play();
            while (source.isPlaying) yield return null;
            if (shuffle)
            {
                songIndex = Random.Range(0, backgroundMusic.Length);
            }
            else if (loop)
            {
                songIndex--;
            }
            songIndex++;
            if (songIndex >= backgroundMusic.Length - 1 && !loop) songIndex = 0;

        }
    }


    public void ToggleShuffle()
    {
        if (!shuffle)
        {
            loop = false;
            loopButton.GetComponent<Image>().color = Color.white;

            shuffleButton.GetComponent<Image>().color = Color.black;
            shuffle = true;
        }
        else
        {
            shuffleButton.GetComponent<Image>().color = Color.white;
            shuffle = false;
        }
    }

    public void ToggleLoop()
    {
        if (!loop)
        {
            shuffle = false;
            shuffleButton.GetComponent<Image>().color = Color.white;

            loopButton.GetComponent<Image>().color = Color.black;
            loop = true;
        }
        else
        {
            loopButton.GetComponent<Image>().color = Color.white;
            loop = false;
        }
    }



    public void SkipSong(int songChange)
    {
        source.Stop();
        StopAllCoroutines();
        //if (songChange == 0) loop = true;
        skipSong = true;
        if (shuffle)
        {
            if (!loop) songIndex = Random.Range(0, backgroundMusic.Length);
            StartCoroutine(PlayAllTracks());
            return;
        }
        songIndex += songChange;
        if (songIndex >= backgroundMusic.Length) songIndex = 0;
        if (songIndex < 0) songIndex = backgroundMusic.Length - 1;
        StartCoroutine(PlayAllTracks());
    }


    public void ToggleDock()
    {
        if (!dockLocked)
        {
            dockLocked = true;
            dockButton.GetComponent<Image>().color = Color.black;
        }
        else
        {
            dockLocked = false;
            //if (!FindObjectOfType<GameManager>().paused) musicPlayer.SetActive(false);
            dockButton.GetComponent<Image>().color = Color.white;
        }
    }





}
