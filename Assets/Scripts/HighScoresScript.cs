using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighScoresScript : MonoBehaviour {

	public GameObject Level;
	public GameObject LevelScore;

	public void SetScore(string Scores, string Level){
		this.LevelScore.GetComponent<Text> ().text = Scores;
		this.Level.GetComponent<Text> ().text = Level;
	}
}
