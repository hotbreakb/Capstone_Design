using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypingTextMesh : MonoBehaviour
{
	public GameObject text;
	TextMesh txt;
	string story;
	public float speed = 0.055f;

	void Awake()
	{
		txt = text.GetComponent<TextMesh>();
		story = txt.text;
		txt.text = "";

		// TODO: add optional delay when to start
		StartCoroutine("PlayText");
	}

	IEnumerator PlayText()
	{
		foreach (char c in story)
		{
			txt.text += c;
			yield return new WaitForSeconds(speed);
		}
	}
}
