using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
	public HeroKnightv2 heroKnight;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Blocks"))
		{
			heroKnight.SetIsOnStone(true);
		}
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.CompareTag("Blocks"))
		{
			heroKnight.SetIsOnStone(true);
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Blocks"))
		{
			heroKnight.SetIsOnStone(false);
		}
	}
}
