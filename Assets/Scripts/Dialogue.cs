using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{

	#region Variables
	[SerializeField] private GameObject _dialogueMark, _dialoguePanel;
	[SerializeField] private TMP_Text _dialogueText;
	[SerializeField] private bool _isMonologue;
	[SerializeField, TextArea(4, 6)] private string[] _dialogueLines;
	private bool _isPlayerInRange, _isDialogueActive;
	private int _lineIndex;
	private float _typingSpeed = 0.033f;
	#endregion

	#region Unity Methods    

	private void Start()
	{
		if (_isMonologue)
		{
			_dialogueMark.SetActive(false);
		}
	}

	void Update()
    {
        if (_isPlayerInRange && (Input.GetKeyDown(KeyCode.E) || (_isMonologue && !_isDialogueActive)))
		{
			if (!_isDialogueActive)
			{
				StartDialogue();
			} else if (_dialogueText.text == _dialogueLines[_lineIndex])
			{
				NextDialogueLine();
			} else
			{
				StopAllCoroutines();
				_dialogueText.text = _dialogueLines[_lineIndex];
			}
		}
    }

	private void StartDialogue()
	{
		_isDialogueActive = true;
		_dialoguePanel.SetActive(true);
		_dialogueMark.SetActive(false);
		_lineIndex = 0;

		GameObject player = GameObject.Find("Jugador");
		player.GetComponent<Rigidbody2D>().velocity = Vector2.zero; // Detener movimiento del jugador
		player.GetComponent<HeroKnightv2>().enabled = false; // Deshabilitar movimiento del jugador
		player.GetComponent<Animator>().SetInteger("AnimState", 0); // Detener animación

		StartCoroutine(ShowLine());
	}

	private void NextDialogueLine()
	{
		_lineIndex++;
		if (_lineIndex < _dialogueLines.Length)
		{
			StartCoroutine(ShowLine());
		}
		else
		{
			_isDialogueActive = false;
			_dialoguePanel.SetActive(false);
			_dialogueMark.SetActive(true);
			GameObject.FindGameObjectWithTag("Player").GetComponent<HeroKnightv2>().enabled = true; // Habilitar movimiento del jugador
			if (_isMonologue)
			{
				Destroy(this.gameObject);
			}
		}
	}

	private IEnumerator ShowLine()
	{
		_dialogueText.text = string.Empty;

		foreach (char ch in _dialogueLines[_lineIndex])
		{
			_dialogueText.text += ch;
			yield return new WaitForSeconds(_typingSpeed);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			_isPlayerInRange = true;
			_dialogueMark.SetActive(true);
		}
	}
	
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			_isPlayerInRange = false;
			_dialogueMark.SetActive(false);
		}
	}

	#endregion
}
