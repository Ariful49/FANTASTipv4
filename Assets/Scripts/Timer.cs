using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
	Text counterText;
	public float timer;
	public GameObject LoseNotif, NoMove;
	public static float winTimerLeft;

	public void Start(){
		winTimerLeft = timer;							//inisialisasi variabel winTimerLeft
		counterText = GetComponent<Text> ()as Text;		//Inisialisasi counterText dengan component text
	}

	public void Update(){
		if (Check.finished == true)						//apabila variabel finished dari kelas check bernilai true
			return;										//mengembalikan ke kelas check
		timer -= Time.deltaTime;						//hitungan mundur dimulai dengan mengurangi waktu timer
		if (timer < 0) {								//Jika timer mencapai 0
			timer = 0;									//variabel timer diset 0 sehingga tidak bernilai negatif
			timeout();									//memanggil fungsi timeout
		}
		counterText.text = timer.ToString ("0");		//menampilkan waktu timer pada variabel counterText yang ditampilkan di GUI
		winTimerLeft = timer;							//Set variabel winTimerLeft dengan nilai timer
	}

	public void timeout(){
		LoseNotif.SetActive (true);						//menampilkan LoseNotif
		NoMove.SetActive (true);						//menampilkan NoMove
	}

	public float timeleft(){
		return winTimerLeft;							//memberikan nilai balikan winTimerLeft
	}
}