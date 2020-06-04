using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MultipleDrop : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {
	public void OnPointerEnter(PointerEventData eventData){
		Debug.Log ("OnPointerEnter");
	}

	public void OnPointerExit(PointerEventData eventdData){
		Debug.Log ("OnPointerExit");
	}


	public void OnDrop(PointerEventData eventData){
		Debug.Log (eventData.pointerDrag.name + " was drop on " + gameObject.name);
		Draggable d = eventData.pointerDrag.GetComponent<Draggable> ();		//inisialisasi variabel d dengan nilai posisi pointer saat digerakkan
		if (d != null) {													//Jika d tidak null
			d.parentToReturnTo = this.transform;							//menset status parent baru
			if (Check.answerfalse == false) {
			}
		}
	}
}
