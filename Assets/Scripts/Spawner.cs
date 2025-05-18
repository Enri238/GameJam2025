using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    #region Variables
    public GameObject cegaroPrefab;
	private int _contadorCegaros;
	private bool _spawnEnabled;
	private float _respawnDelay = 3f;
	private CegaroManager _cegaroManager;
	#endregion

	#region Unity Methods    

	// Start is called before the first frame update
	void Start()
    {
        _contadorCegaros = 0;
        _spawnEnabled = false;
		_cegaroManager = FindObjectOfType<CegaroManager>();
	}

    // Update is called once per frame
    void Update()
    {
		if (!_spawnEnabled && GameObject.Find("Punto de inicio") == null)
		{
			_spawnEnabled = true;
		}
		else if (_spawnEnabled && _contadorCegaros < 1)
		{
			if (_cegaroManager.CanSpawnCegaro())
			{
				_contadorCegaros++;
				_cegaroManager.RegistrarCegaroVivo();

				GameObject cegaro = Instantiate(cegaroPrefab, transform.position, Quaternion.identity);
				Cegaro c = cegaro.GetComponent<Cegaro>();
				c.SetSpawner(this);
			}
			else
			{
				Destroy(gameObject);
			}
		}
	}

    public void CegaroDestruido()
    {
		StartCoroutine(EsperarYReiniciar());
	}

	private IEnumerator EsperarYReiniciar()
	{
		_cegaroManager.UpdateCegaros();
		yield return new WaitForSeconds(_respawnDelay);
		_contadorCegaros--;
	}

	#endregion
}
