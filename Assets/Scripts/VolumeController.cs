using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider volumeSlider;


    void Start()
    {
        // Inicializa el slider con el volumen actual
        float volume;
        mixer.GetFloat("MasterVolume", out volume);
        if(volumeSlider!=null){
            volumeSlider.value = Mathf.Pow(10, volume / 20);
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
        // if((!MusicOn && !volumeSlider)){
        //     gameObject.SetActive(false);
        // }
        
    }

    public void SetVolume(float value)
    {
        // El volumen va en decibelios, por eso se hace logaritmo
        
        if (value <= 0.0001f )
        {
            mixer.SetFloat("MasterVolume", -80f); // Silencio total
        }
        else
        {
            mixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
        }

        PlayerPrefs.SetFloat("volume", value);
        
    }
}
