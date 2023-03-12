using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{

	public GameObject winPanel;
	public float effectTime = 10;
	public float panelTime = 10;

	void OnTriggerEnter(Collider other)
	{
		Destroy(other.gameObject);
		StartCoroutine(Win());
		gameObject.GetComponent<AudioSource>().Play();
	}

	IEnumerator Win()
	{
		yield return new WaitForSeconds(effectTime);
		winPanel.SetActive(true);

		//winEffect.SetActive(true);

		yield return new WaitForSeconds(panelTime);

		//gameObject.GetComponent<AudioSource>().Stop();
		winPanel.SetActive(false);
		//winEffect.SetActive(false);
	}
}

