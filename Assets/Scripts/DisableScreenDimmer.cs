using UnityEngine;
using System.Collections;

public class DisableScreenDimmer : MonoBehaviour {			//fungsi agar layar tidak sleep selama aplikasi berjalan
	void Start () {
		Screen.sleepTimeout = SleepTimeout.NeverSleep;		//set sleep out dengan neversleep
	}
}
