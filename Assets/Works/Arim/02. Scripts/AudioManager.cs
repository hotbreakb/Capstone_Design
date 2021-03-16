using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public AudioSource bomb1;
    public AudioSource bomb2;
    public AudioSource bomb3;
    public AudioSource bomb4;
    public AudioSource bomb5;


    public void Start()
    {
        StartCoroutine(Bomb1());
        StartCoroutine(Bomb2());
        StartCoroutine(Bomb3());
        StartCoroutine(Bomb4());
        StartCoroutine(Bomb5());


    }
    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolumn = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolumn * Time.deltaTime / FadeTime;
            yield return null;
        }
        audioSource.Stop();
    }
    IEnumerator Bomb1()
    {
        yield return new WaitForSeconds(0f);
        bomb1.Play();
    }
    IEnumerator Bomb2()
    {
        yield return new WaitForSeconds(0.1f);
        bomb2.Play();
    }
    IEnumerator Bomb3()
    {
        yield return new WaitForSeconds(0.15f);
        bomb3.Play();
    }
    IEnumerator Bomb4()
    {
        yield return new WaitForSeconds(0f);
        bomb4.Play();
    }
    IEnumerator Bomb5()
    {
        yield return new WaitForSeconds(0.13f);
        bomb5.Play();
        yield return new WaitForSeconds(2f);
        StartCoroutine(FadeOut(bomb1, 1.8f));
        StartCoroutine(FadeOut(bomb2, 1.8f));
        StartCoroutine(FadeOut(bomb3, 1.8f));
        StartCoroutine(FadeOut(bomb4, 1.8f));
        StartCoroutine(FadeOut(bomb5, 1.8f));

    }
}
