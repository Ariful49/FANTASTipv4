using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadRandomLevel : MonoBehaviour {
	public void ChangeScene(string level ) {
		string[]sublevel = new string[3] { "_1", "_2", "_3" };
		int random = Random.Range(0, 3);
		SceneManager.LoadScene(level+sublevel[random]);
	}
}