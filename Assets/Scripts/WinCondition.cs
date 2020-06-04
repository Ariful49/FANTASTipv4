using UnityEngine;
using System.Collections;

public class WinCondition : MonoBehaviour {
	public int total_answer;
	public static bool win;
	void Start () {
		win = false;
	}
	
	void Update () {
		if (Check.answer == total_answer) {
			win = true;
		}
	}
	bool winning(){
		return win;
	}
}
