using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetectorEnemy : MonoBehaviour
{
	public Cegaro cegaro;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Blocks"))
		{
			cegaro.SetIsOnStone(true);
		}
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.CompareTag("Blocks"))
		{
			cegaro.SetIsOnStone(true);
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Blocks"))
		{
			cegaro.SetIsOnStone(false);
		}
	}
}
