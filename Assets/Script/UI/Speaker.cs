using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speaker : MonoBehaviour
{
    public List<AudioClip> audioClips = new List<AudioClip>();

    private AudioClip currentClip;

    [SerializeField] private AudioSource speakerOneShot;
    [SerializeField] private AudioSource speakerRemune;
    private void Start()
    {
    }

    public void PlayAudioOneShot(string name)
    {
        Debug.Log("Action: " + name);
        AudioClip audioClip = audioClips.Find(x =>  x.name == name);
        if (audioClip != null) speakerOneShot.PlayOneShot(audioClip);
    }

    public void PlayAudioRemune(string name) // Play audio duy trì 
    {
        AudioClip audioClip = audioClips.Find(x => x.name == name);
        if (audioClip != null && audioClip != currentClip)
        {
            currentClip = audioClip;
            speakerRemune.clip = audioClip;
            speakerRemune.Play();
        }
    }

    public void StopAudioRemune()
    {
        speakerRemune.Stop();
        currentClip = null;
    }
}
