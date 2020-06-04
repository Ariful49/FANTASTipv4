using UnityEngine;
using System.Collections;

class HighScores {
	public int Level { get; set; }		//inisialisasi fungsi level dengan fungsi get & set
	public int Score { get; set; }		//inisialisasi fungsi Star dengan fungsi get & set
	public HighScores(int level, int score){	//inisialisasi fungsi levels dengan parameter level dan star
		this.Level = level;				//inisialisasi fungsi Level
		this.Score = score;				//inisialisasi fungsi Star
	}

	public override string ToString ()
	{
		return string.Format ("[Level: Level={0}, Sc={1}]", Level, Score);
	}
}