using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioClip pickupSound;
    public AudioClip winSound;
    public AudioClip pinSound;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayPickupSound()
    {
        PlaySound(pickupSound);
    }

    public void PlayWinSound()
    {
        PlaySound(winSound);
    }

    public void PlayPinSound()
    {
        PlaySound(pinSound);
    }

    void PlaySound(AudioClip _newSound)
    {
        //Set the audiosources audioclip to be the passed in sound
        audioSource.clip = _newSound;
        //Play the audiosource
        audioSource.Play();
    }

    public void PlayCollisionSound(GameObject _go)
    {
        //Check to see if the collided object has an Audiosource. 
        //This is a failsafe in case we forgot to attach one to our wall
        if (_go.GetComponent<AudioSource>() != null)
        {
            //Play the audio on the wall object
            _go.GetComponent<AudioSource>().Play();
        }
    }
}
