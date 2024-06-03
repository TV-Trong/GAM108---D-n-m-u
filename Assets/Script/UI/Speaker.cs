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
        //Debug.Log("Action: " + name);
        AudioClip audioClip = audioClips.Find(x =>  x.name == name);
        if (audioClip != null) speakerOneShot.PlayOneShot(audioClip);
        else Debug.Log("No Sound");
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

    public void PlaySoundLifeUp()
    {
        AudioClip lifeUpSound = audioClips.Find(x => x.name == "LifeUpSound");
        if (lifeUpSound != null) speakerOneShot.PlayOneShot(lifeUpSound);
    }
    
    public void MuteAudioRemune()
    {
        if (speakerRemune != null)
        {
            if (!speakerRemune.mute)
            {
                speakerRemune.mute = true;
            }
        }
    }

    public void StopAudioRemune()
    {
        if (speakerRemune != null)
        {
            speakerRemune.Stop();
            currentClip = null;
        }

    }
}
