using UnityEngine;
using System.Collections;

class Modes {
	public int Level { get; set; }		//inisialisasi fungsi level dengan fungsi get & set
	public string isLocked { get; set; }		//inisialisasi fungsi Star dengan fungsi get & set

	public Modes(int level, string islocked){	//inisialisasi fungsi levels dengan parameter level dan star
		this.Level = level;				//inisialisasi fungsi Level
		this.isLocked = islocked;		//inisialisasi fungsi Star
	}

	public override string ToString ()
	{
		return string.Format ("[Level: Level={0}, isunlock={1}]", Level, isLocked);
	}
}
