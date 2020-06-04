using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MultipleDropSwipe : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {
	public void OnPointerEnter(PointerEventData eventData){
		Debug.Log ("OnPointerEnter");
	}

	public void OnPointerExit(PointerEventData eventdData){
		Debug.Log ("OnPointerExit");
	}


	public void OnDrop(PointerEventData eventData){
		Debug.Log (eventData.pointerDrag.name + " was drop on " + gameObject.name);
		DraggableSwipe d = eventData.pointerDrag.GetComponent<DraggableSwipe> ();		//inisialisasi variabel d dengan nilai posisi pointer saat digerakkan
		if (d != null) {													//Jika d tidak null
			d.parentToReturnTo = this.transform;							//menset status parent baru
			if (Check.answerfalse == false) {
			}
		}
	}
}
