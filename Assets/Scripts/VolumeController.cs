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
        volumeSlider.value = Mathf.Pow(10, volume / 20);
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float value)
    {
        // El volumen va en decibelios, por eso se hace logaritmo
        mixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
    }
}
