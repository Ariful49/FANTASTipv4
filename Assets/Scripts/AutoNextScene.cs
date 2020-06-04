using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AutoNextScene : MonoBehaviour {
	public int DelayTime;		//variabel untuk delay
	public string NextLevel;	//variabel untuk scene yang dituju

	void Start () {
		StartCoroutine (LoadNextScene ());	//menjalankan fungsi coroutine (fungsi yg dapat dieksekusi secara berkelanjutan) pada fungsi LoadNextScene
	}

	IEnumerator LoadNextScene(){
		yield return new WaitForSeconds (DelayTime);	//memanggil fungsi delay
		if (NextLevel != ""){
			SceneManager.LoadScene (NextLevel);			//memanggil scene
		}
	}
}
