using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{

	public float effectTime = 10;
	public float panelTime = 10;
	public float confettisDelay = 1.0f;
	public GameObject youWon;
	public GameObject confetti;

	void OnTriggerEnter(Collider other)
	{
		Destroy(other.gameObject);
		PlayerProgress.wonLabyrinth = true;
		StartCoroutine(Win());
	}

	IEnumerator Win()
	{
		confetti.SetActive(true);
		yield return new WaitForSecondsRealtime(1);
		youWon.SetActive(true);
		yield return new WaitForSecondsRealtime(4);
		SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
	}

}

