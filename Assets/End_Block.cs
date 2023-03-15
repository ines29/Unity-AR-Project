using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_Block : MonoBehaviour
{

	public GameObject winPanel;
	public float effectTime;
	public float panelTime;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			Destroy(other.gameObject);
			StartCoroutine(Win());
		}
		
	}

	IEnumerator Win()
	{
		yield return new WaitForSeconds(effectTime);
		gameObject.GetComponent<AudioSource>().Play();
		winPanel.SetActive(true);

		//winEffect.SetActive(true);

		yield return new WaitForSeconds(panelTime);

		//gameObject.GetComponent<AudioSource>().Stop();
		winPanel.SetActive(false);
		//winEffect.SetActive(false);
	}
}

