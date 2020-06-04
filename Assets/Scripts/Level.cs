using UnityEngine;
using System.Collections;

class Levels {
	public int Level { get; set; }				//inisialisasi fungsi level dengan fungsi get & set
	public string isLocked { get; set; }		//inisialisasi fungsi isLocked dengan fungsi get & set
	public Levels(int level, string islocked){	//inisialisasi fungsi levels dengan parameter level dan islocked
		this.Level = level;						//inisialisasi fungsi Level
		this.isLocked = islocked;				//inisialisasi fungsi IsLocked
	}
	public override string ToString ()
	{
		return string.Format ("[Level: Level={0}, isLocked={1}]", Level, isLocked);
	}
}
