using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DropSwipe : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

	public void OnPointerEnter(PointerEventData eventData){
		Debug.Log ("OnPointerEnter");
	}

	public void OnPointerExit(PointerEventData eventdData){
		Debug.Log ("OnPointerExit");
	}

	public GameObject option{
		get{
			if (transform.childCount > 0) {					//mencek apakah objek memiliki child atau tidak
				return transform.GetChild (0).gameObject;	//jika terdapat child memberikan nilai balikan berupa mengembalikan child ke game Object sebelumnya
			}
			return null;									//jika tidak ada child memberikan nilai balikan null
		}
	}

	public void OnDrop(PointerEventData eventData){
		Debug.Log (eventData.pointerDrag.name + " was drop on " + gameObject.name);
		DraggableSwipe d = eventData.pointerDrag.GetComponent<DraggableSwipe> ();				//inisialisasi variabel d dengan nilai posisi pointer saat digerakkan
		if (!option) {																
			d.parentToReturnTo = this.transform;									//menset status parent baru
		}
	}
}
