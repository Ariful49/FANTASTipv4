using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;
using System.Collections.Generic;

public class HighScoresManager : MonoBehaviour {
	private string connectionString;
	private List <HighScores> Sc = new List <HighScores>();
	int [] skor =new int[24];
	public GameObject scorePrefab;
	public Transform scoreParent;

	// Use this for initialization
	void Start () {
		Path ("Level.sqlite");
		ShowScores ();
	}

	public void Path(string p){
		#if UNITY_EDITOR
		var dbPath = string.Format (@"Assets/StreamingAssets/{0}", p);
		#else
		string filepath = Application.persistentDataPath + "/" + p;
		if (!File.Exists(filepath))
		{
		Debug.Log("Database not in Persistent path");
		#if UNITY_ANDROID 
		WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/" + p);
		while(!loadDB.isDone) {}
		File.WriteAllBytes(filepath, loadDB.bytes);
		#endif
		}
		var dbPath = filepath;
		#endif
		connectionString = "URI=file:" + dbPath;
	}

	private void ReadScore (){
		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) {
			Sc.Clear ();
			dbConnection.Open ();

			using (IDbCommand dbCmd = dbConnection.CreateCommand ()) {
				string sqlQuery = "SELECT * FROM HighScores";										//Deklarasi sqlQuery dengan query
				dbCmd.CommandText = sqlQuery;													//mengeksekusi Query
				using (IDataReader reader = dbCmd.ExecuteReader ()) {							//fungsi untuk Read DB
					while (reader.Read ()) {
						Sc.Add (new HighScores (reader.GetInt32 (0), reader.GetInt32 (1)));		//Menambahkan isi list Lv dengan isi tabel pada DB
					}
					int i = 0;
					foreach (HighScores scor in Sc) {
						skor [i] = scor.Score;												//Set nilai array locked dengan kolom isLocked pada DB
						i++;
					}
					reader.Close ();
				}
			}
		}
	}

	private void ShowScores(){
		ReadScore ();
		for (int i = 0; i < Sc.Count; i++) {
			GameObject tmpObjec = Instantiate (scorePrefab);

			HighScores tmpScore = Sc [i];

			tmpObjec.GetComponent<HighScoresScript> ().SetScore (tmpScore.Score.ToString (), tmpScore.Level.ToString ());
			tmpObjec.transform.SetParent (scoreParent);
			tmpObjec.GetComponent<RectTransform> ().localScale = new Vector3 (1, 1, 1);
		}
	}
}