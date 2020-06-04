using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour {

	public void OnMouseDown () {
		SceneManager.LoadScene (Application.loadedLevel);	//memanggil kembali (restart) scene yang aktif
	}
}
