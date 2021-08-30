using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public AudioSource mainSource;
    public AudioSource soundSource;

    public AudioClip gameOver;
    public AudioClip normalMusic;
    public AudioClip soundError;
    public AudioClip pop;
    public AudioClip footstep;
    public AudioClip hit;
    public AudioClip jesusMalverde;
    public AudioClip grass;
    public AudioClip watered;
    public AudioClip readyToCrop;
    public AudioClip newOrder;
    public AudioClip jesusReady;
    public AudioClip niceRonald;

    public bool gameOverReady = false;
    public static MusicScript instance;

    public void Awake() => instance = this;

    public void gameOverMusic()
    {
        if (!gameOverReady)
        {
            mainSource.clip = gameOver;
            mainSource.pitch = 1f;
            mainSource.Play();
            gameOverReady = true;
        }
    }

    public void playError(float vol)
    {
        soundSource.PlayOneShot(soundError, vol);
    }

    public void playGrass(float vol)
    {
        soundSource.PlayOneShot(grass, vol);
    }

    public void playWatered(float vol)
    {
        soundSource.PlayOneShot(watered, vol);
    }

    public void playPop(float vol)
    {
        soundSource.PlayOneShot(pop, vol);
    }

    public void playFootstep(float vol)
    {
        soundSource.PlayOneShot(footstep, vol);
    }

    public void playHit(float vol)
    {
        soundSource.PlayOneShot(hit, vol);
    }

    public void playJesusMalverde(float vol)
    {
        soundSource.PlayOneShot(jesusMalverde, vol);
    }

    public void playReadytoCrop(float vol)
    {
        soundSource.PlayOneShot(readyToCrop, vol);
    }

    public void playNice(float vol)
    {
        soundSource.PlayOneShot(niceRonald, vol);
    }

    public void playJesusReady(float vol)
    {
        soundSource.PlayOneShot(jesusReady, vol);
    }

    public void playNewOrder(float vol)
    {
        soundSource.PlayOneShot(newOrder, vol);
    }
}
