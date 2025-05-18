using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CegaroManager : MonoBehaviour
{
    #region Variables
    public TextMeshProUGUI _cegaroManager;

    [Header("Cinemática")]
    [Tooltip("Arrastra aquí el objeto Cinematica_Muerte (con un PlayableDirector)")]
    public PlayableDirector cinematicaMuerte;

    private int _remainingCegaros;
    private int _cegarosVivos;
    private bool _cinematicaIniciada = false;
    #endregion

    #region Unity Methods    
    void Start()
    {
        _remainingCegaros = 6;
        _cegarosVivos     = 0;

        _cegaroManager.text = "Cégaros restantes: " + _remainingCegaros;

        if (cinematicaMuerte != null)
        {
            cinematicaMuerte.stopped += OnCinematicaTerminada;
        }
    }

    public void UpdateCegaros()
    {
        _remainingCegaros = Mathf.Max(0, _remainingCegaros - 1);
        _cegarosVivos     = Mathf.Max(0, _cegarosVivos - 1);

        _cegaroManager.text = "Cégaros restantes: " + _remainingCegaros;

        if (_remainingCegaros == 0 && cinematicaMuerte != null && !_cinematicaIniciada)
        {
            _cinematicaIniciada = true;
            cinematicaMuerte.Play();
        }
    }

    public bool CanSpawnCegaro()
    {
        return _remainingCegaros > 0 && _cegarosVivos < _remainingCegaros;
    }

    public void RegistrarCegaroVivo()
    {
        _cegarosVivos++;
    }

    private void OnCinematicaTerminada(PlayableDirector director)
    {
        int escenaActual = SceneManager.GetActiveScene().buildIndex;
        int siguienteEscena = escenaActual + 1;

        if (siguienteEscena < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(siguienteEscena);
        }
        else
        {
            Debug.LogWarning("No hay una siguiente escena definida en Build Settings.");
        }
    }

    private void OnDestroy()
    {
        if (cinematicaMuerte != null)
        {
            cinematicaMuerte.stopped -= OnCinematicaTerminada;
        }
    }
    #endregion
}
