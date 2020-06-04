using UnityEngine;
using System.Collections;

class Stars {
	public int Level { get; set; }		//inisialisasi fungsi level dengan fungsi get & set
	public int Star { get; set; }		//inisialisasi fungsi Star dengan fungsi get & set
	public Stars(int level, int star){	//inisialisasi fungsi levels dengan parameter level dan star
		this.Level = level;				//inisialisasi fungsi Level
		this.Star = star;				//inisialisasi fungsi Star
	}

	public override string ToString ()
	{
		return string.Format ("[Level: Level={0}, Star={1}]", Level, Star);
	}
}
