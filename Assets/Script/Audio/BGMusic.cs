using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic : Singleton<BGMusic>
{
    [SerializeField] AudioSource[] listOfSongs;
    int currentSong = 0;

    private void Start()
    {
        listOfSongs[currentSong].Play();
    }
    public void PlayASong(int song)
    {
        StopAllSong();
        currentSong = song;
        StartCoroutine(PlayNewSong(song));
    }
    public void StopAllSong()
    {
        foreach (var song in listOfSongs)
        {
            song.Stop();
        }
    }
    IEnumerator PlayNewSong(int i)
    {
        yield return new WaitForSeconds(2f);

        listOfSongs[i].Play();
    }
}
