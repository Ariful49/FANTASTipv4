using UnityEngine;
using System.Collections;

class StarsCount {
	public int Game { get; set; }		//inisialisasi fungsi level dengan fungsi get & set
	public int StarCount { get; set; }		//inisialisasi fungsi Star dengan fungsi get & set
	public StarsCount(int game, int starcount){	//inisialisasi fungsi levels dengan parameter level dan star
		this.Game = game;				//inisialisasi fungsi Level
		this.StarCount = starcount;				//inisialisasi fungsi Star
	}

	public override string ToString ()
	{
		return string.Format ("[Game: Game={0}, StarCount={1}]", Game, StarCount);
	}
}
