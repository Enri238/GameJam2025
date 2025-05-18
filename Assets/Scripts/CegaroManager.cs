using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CegaroManager : MonoBehaviour
{

	#region Variables
	public TextMeshProUGUI _cegaroManager;

    private int _remainingCegaros;
	private int _cegarosVivos;
	#endregion

	#region Unity Methods    

	void Start()
    {
        _remainingCegaros = 6;
		_cegarosVivos = 0;

		_cegaroManager.text = "Cégaros restantes: " + _remainingCegaros;
	}

	public void UpdateCegaros()
	{
		_remainingCegaros--;
		_cegarosVivos--;

		_cegaroManager.text = "Cégaros restantes: " + _remainingCegaros;
	}

	public bool CanSpawnCegaro()
	{
		return _remainingCegaros > 0 && _cegarosVivos < _remainingCegaros;
	}

	public void RegistrarCegaroVivo()
	{
		_cegarosVivos++;
	}

	#endregion
}
