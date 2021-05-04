using Leap.Unity.Interaction;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeControl : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioMixer masterMixer;
    // public Slider audioSlider;
    public InteractionSlider audioSlider;

    public void AudioControl()
    {
        float sound = audioSlider.HorizontalSliderValue;

        Debug.Log("sound: " + sound);

        if(sound == -40.0f) masterMixer.SetFloat("Effect Sound", -80);
        else masterMixer.SetFloat("Effect Sound", sound);
    }
}
