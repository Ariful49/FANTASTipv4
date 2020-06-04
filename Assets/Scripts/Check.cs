using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Check : MonoBehaviour,IDropHandler {
	public GameObject WinNotif, NoMove;						//Deklarasi gameobject WinNotif dan NoMove
	public static bool finished,answerfalse,temp_true;		//Deklarasi variabel static finished, answerfalse,temptrue dengan tipe data boolean
	public static int answer;								//Deklarasi variabel static answer dengan tipe data integer

	public GameObject checkChild {							//Fungsi untuk mencek apabila gameobject memiliki child atau tidak
		get {
			if (transform.childCount > 0) {					//mencek apakah objek memiliki child atau tidak
				return transform.GetChild (0).gameObject;	//jika terdapat child memberikan nilai balikan berupa mengembalikan child ke game Object sebelumnya
			}
			return null;									//jika tidak ada child memberikan nilai balikan null
		}
	}

	public void OnDrop(PointerEventData eventData){
		string option = eventData.pointerDrag.name;			//Inisialisasi variabel option dari nama gameobject dengan fungsi pointer drag
		string target = gameObject.name;					//Inisialisasi variabel target dari nama GameObject tujuan
		if (!checkChild) {									//Jika fungsi check child memberikan nilai balikan null
			if (option == target) {							//Jika nilai variabel option sama dengan target
				temp_true = true;							//Set nilai temp_true menjadi true
				answeristrue ();							//Memanggil fungsi answeristrue
			} else {										//Jika nilai variabel option tidak sama dengan target
				temp_true = false;							//Set nilai temp_true menjadi false
				answerisfalse ();							//memanggil fungsi answerisfalse
			}
		}
	}

	public void answeristrue(){
		if (temp_true==true) {							
			Debug.Log ("+1");
			answer += 1;
			answerfalse = false;
		}
	}

	public void answerisfalse(){
		if (temp_true==false) {							
			Debug.Log ("0");
			answerfalse = true;
		}
	}

	void Start(){
		answer = 0;
		finished = false;									//Inisialisasi variabel statis finished dengan nilai false
		answerfalse=true;

		temp_true = false;
	}

	void Update(){
		if (WinCondition.win == true) {						//Jika total jawaban benar sementara mencapai total jawaban benar
			finished = true;								//variabel finish diset bernilai true
			WinNotif.SetActive (true);						//menampilkan winnotif & nomove
			NoMove.SetActive (true);
		}
	}

	bool EndLevel(){
		return finished;									//memberikan nilai balikan finished
	}
}