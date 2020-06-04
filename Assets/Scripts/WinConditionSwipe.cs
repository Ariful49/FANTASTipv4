using UnityEngine;
using System.Collections;

public class WinConditionSwipe : MonoBehaviour {
	public int total_answer;
	public static bool win;
	void Start () {
		win = false;
	}
	
	void Update () {
		if (CheckSwipe.answer == total_answer) {
			win = true;
		}
	}
	bool winning(){
		return win;
	}
}
