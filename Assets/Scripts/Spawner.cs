using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    #region Variables
    public GameObject cegaroPrefab;
	private int _contadorCegaros;
	private bool _canSpawn;
	private float _respawnDelay = 3f;
	#endregion

	#region Unity Methods    

	// Start is called before the first frame update
	void Start()
    {
        _contadorCegaros = 0;
        _canSpawn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_canSpawn && GameObject.Find("Punto de inicio") == null)
        {
            _canSpawn = true;
        }
        else if (_canSpawn && _contadorCegaros < 1)
        {
            _contadorCegaros++;
			GameObject cegaro = Instantiate(cegaroPrefab, transform.position, Quaternion.identity);

			Cegaro c = cegaro.GetComponent<Cegaro>();
			c.SetSpawner(this);
		}
	}

    public void CegaroDestruido()
    {
		StartCoroutine(EsperarYReiniciar());
	}

	private IEnumerator EsperarYReiniciar()
	{
		yield return new WaitForSeconds(_respawnDelay);
		_contadorCegaros--;
	}

	#endregion
}
