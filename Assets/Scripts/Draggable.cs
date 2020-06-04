using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	
	public Transform parentToReturnTo=null;		//menset status parent menjadi null
	string parentname,event_d;					//Deklarasi variabel parentname dan event_d


	public void OnBeginDrag(PointerEventData eventData){
		
		parentname = this.transform.parent.name;					//set nilai variabel parentname dengan nama parent gameObject
		event_d = eventData.pointerDrag.name;						//set nilai event_d dengan nama game object yang didrag

		parentToReturnTo = this.transform.parent;					//inisialisasi status parent
		this.transform.SetParent (this.transform.parent.parent);	//menset game object menjadi parentnya parent
		GetComponent<CanvasGroup>().blocksRaycasts = false;			//mendisable raycast pada canvas group

		if (Check.answer == 0 || parentname == "option") {			//Jika ketika akan didrag jawaban masih kosong atau parentname bernilai option
			Debug.Log ("0");										
			return;													//memberikan nilai balikan kosong
		}else if (Check.answerfalse == true) {						//Jika ketika akan didrag jawaban salah
			if (parentname == event_d) {							//Jika nilai variabel parentname sama dengan event_d
				Check.answerfalse = false;							//Set nilai answerfalse menjadi false (jawaban benar)
				Debug.Log ("-1");
				Check.answer -= 1;									//variabel answer pada kelas check -1
			} else {
				Debug.Log ("0");
				return;												//memberikan nilai balikan kosong
			}
		}else if (Check.answerfalse == false ) {					//Jika ketika akan didrag jawaban benar
			if (parentname != event_d) {							//Jika variabel parentname tidak sama dengan variabel event_d
				Check.answerfalse = true;							//Set nilai answerfalse menjadi true (jawaban salah)
				Debug.Log ("0");
				return;												//Memberikan nilai balikan kosong
			} else {
				Debug.Log ("-1");
				Check.answer -= 1;									//variabel answer pada kelas check -1
			}
		}
	}

	public void OnDrag(PointerEventData eventData){
		//Debug.Log ("OnDrag");

		Vector2 currentScreenPoint = new Vector2(Input.mousePosition.x,Input.mousePosition.y);	//variabel currentScreen berdasarkan vector 2 posisi mouse
		Vector2 currentPos = Camera.main.ScreenToWorldPoint (currentScreenPoint);				//variabel currentPos dengan menset camera vector 2 WorldPoint dengan parameter currentScreen
		transform.position = currentPos;														//menset posisi gameobject dengan variabel currentPos

	}

	public void OnEndDrag(PointerEventData eventData){
		Debug.Log ("OnEndDrag");

		this.transform.SetParent(parentToReturnTo);			//mengembalikan status parent
		GetComponent<CanvasGroup> ().blocksRaycasts = true;	//mengenable kembali raycast pada canvas group
	}
}