using UnityEngine;
using System.Collections;

public class DelayStory : MonoBehaviour {
	public float DelayTime;
	public GameObject NextButton;

	void Start () {
		StartCoroutine (Delay ());
	}

	IEnumerator Delay(){
		yield return new WaitForSeconds (DelayTime);
		NextButton.SetActive (true);
	}
}
