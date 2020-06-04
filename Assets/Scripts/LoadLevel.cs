using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour {
	public void ChangeScene(string sceneName) {
		SceneManager.LoadScene (sceneName);			//memanggil scene dari parameter sceneName
	}
}