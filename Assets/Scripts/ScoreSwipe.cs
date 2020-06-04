using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;
using System.Collections.Generic;

public class ScoreSwipe : MonoBehaviour {
	private string connectionString;								//Deklarasi variabel yang digunakan untuk koneksi DB
	public int minScore, scorestar2, scorestar3, levtoupdate,levstartoupdate;		//deklarasi variabel public untuk skor minimal, skor bintang2, dan skor bintang 3
	float TimeLeft, score;											//Deklarasi variabel TimeLeft & score
	Text YourScore;													//Deklarasi variabel YourScore

	int levelstar=0;
	int levscore=0;
	int checkstar;
	int startotal;
	int starsum;
	int oldscore;

	int [] star=new int[24];					//deklarasi array locked
	int [] starCount=new int[1];
	int [] skor =new int[24];

	public GameObject starleft, starmid, starright;
	public GameObject inactive_starmid, inactive_starright;

	private List <Stars> St = new List <Stars>();		//deklarasi list dengan variabel kelas levels
	private List <StarsCount> Stc = new List <StarsCount>();
	private List <HighScores> Sc = new List <HighScores>();

	void Start () {
		Path ("Level.sqlite");
		score = 0;									//Inisialisasi variabel score dengan nilai 0
		TimeLeft = TimerSwipe.winTimerLeft;				//inisialisasi variabel TimeLeft dengan nilai winTimerLef pada kelas Timer
		YourScore = GetComponent<Text> ()as Text;	//Inisialisasi variabel YourScore dengan menset menjadi komponen text
		TextScore ();								//memanggil fungsi TextScore
		Star ();									//memanggil fungsi star
		updateLevel(levtoupdate);
		updateStar(levstartoupdate,levelstar);
		updateScore (levstartoupdate, levscore);
	}
	
	void TextScore () {
		score = minScore + (TimeLeft * 1000);		//Set nilai score
		score = (int)score;							//Konversi tipe data score dari float ke integer
		levscore=(int)score;
		YourScore.text = score.ToString();			//Menampilkan score
	}

	void Star(){
		if (score > scorestar3) {					//Kondisi jika mencapai nilai bintang3
			starleft.SetActive (true);				//menampilkan bintang kiri
			starmid.SetActive (true);				//menampilkan bintang tengah
			starright.SetActive (true);				//menampilkan bintang kanan
			if (checkstar == 3) {
				return;
			} else if (checkstar == 2) {
				if (levelstar < 4) {
					levelstar += 1;
				}
			} else if (checkstar == 1) {
				if (levelstar < 4) {
					levelstar += 2;
				}
			} else if (checkstar == 0) {
				if (levelstar < 4) {
					levelstar += 3;
				}
			}

		}
		else if (score > scorestar2) {				//Kondisi jika mencapai nilai bintang2
			starleft.SetActive (true);				//menampilkan bintang kiri
			starmid.SetActive (true);				//menampilkan bintang tengah
			inactive_starright.SetActive (true);				//menampilkan bintang kanan
			if (checkstar == 3) {
				return;
			} else if (checkstar == 2) {
				return;
			} else if (checkstar == 1) {
				if (levelstar < 3) {
					levelstar += 1;
				}
			} else if (checkstar == 0) {
				if (levelstar < 3) {
					levelstar += 2;
				}
			}
		}

		else if (score > minScore) {				//Kondisi jika mencapai nilai bintang1
			starleft.SetActive (true);				//menampilkan bintang kiri
			inactive_starmid.SetActive (true);		//menampilkan bintang hitam tengah
			inactive_starright.SetActive (true);	//menampilkan bintang hitam kanan
			if (checkstar == 3) {
				return;
			} else if (checkstar == 2) {
				return;
			} else if (checkstar == 1) {
				return;
			} else if (checkstar == 0) {
				if (levelstar < 2) {
					levelstar += 1;
				}
			}
		}
	}

	public void Path(string p){
		#if UNITY_EDITOR
		var dbPath = string.Format(@"Assets/StreamingAssets/{0}", p);
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

	void updateLevel(int updateLevel){
		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) {
			dbConnection.Open ();

			using (IDbCommand dbCmd = dbConnection.CreateCommand ()) {
				string sqlQuery = string.Format ("UPDATE Levels SET isLocked = 'unlock' WHERE Level = \"{0}\"", updateLevel);
				dbCmd.CommandText = sqlQuery;
				dbCmd.ExecuteScalar ();
			}
			dbConnection.Close ();
		}
	}

	void updateScore(int updatescore,int updateLevel){
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
					oldscore = skor [levstartoupdate-1];
					reader.Close ();
				}
				if (levscore > oldscore) {
					sqlQuery = string.Format ("UPDATE HighScores SET score = \"{0}\" WHERE level = \"{1}\"", levscore, levstartoupdate);
					dbCmd.CommandText = sqlQuery;
					dbCmd.ExecuteScalar ();
				}

			}
			dbConnection.Close ();
		}		
	}

	void updateStar(int updatestar, int updateLevel){
		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) {
			St.Clear ();
			dbConnection.Open ();

			using (IDbCommand dbCmd = dbConnection.CreateCommand ()) {
				string sqlQuery = "SELECT * FROM Levels_star";										//Deklarasi sqlQuery dengan query
				dbCmd.CommandText = sqlQuery;													//mengeksekusi Query
				using (IDataReader reader = dbCmd.ExecuteReader ()) {							//fungsi untuk Read DB
					while (reader.Read ()) {
						St.Add (new Stars (reader.GetInt32 (0), reader.GetInt32 (1)));		//Menambahkan isi list Lv dengan isi tabel pada DB
					}
					int i = 0;
					foreach (Stars levstar in St) {
						star [i] = levstar.Star;												//Set nilai array locked dengan kolom isLocked pada DB
						i++;
					}
					checkstar = star [levstartoupdate - 1];
					reader.Close ();
				}

				sqlQuery = "SELECT * FROM StarCount";
				dbCmd.CommandText = sqlQuery;													//mengeksekusi Query
				using (IDataReader reader = dbCmd.ExecuteReader ()) {							//fungsi untuk Read DB
					while (reader.Read ()) {
						Stc.Add (new StarsCount (reader.GetInt32 (0), reader.GetInt32 (1)));		//Menambahkan isi list Lv dengan isi tabel pada DB
					}
					int i = 0;
					foreach (StarsCount starcounts in Stc) {
						starCount [i] = starcounts.StarCount;												//Set nilai array locked dengan kolom isLocked pada DB
						i++;
					}
					reader.Close ();
				}

				if (levelstar > checkstar) {
					sqlQuery = string.Format ("UPDATE Levels_star SET Star = \"{0}\" WHERE Level = \"{1}\"", levelstar, levstartoupdate);
					dbCmd.CommandText = sqlQuery;
					dbCmd.ExecuteScalar ();

					if (checkstar == 0 && levelstar == 3) {
						starsum = 3;
					} else if (checkstar == 0 && levelstar == 2) {
						starsum = 2;
					} else if (checkstar == 0 && levelstar == 1) {
						starsum = 1;
					} else if (checkstar == 1 && levelstar == 3) {
						starsum = 2;
					} else if (checkstar == 1 && levelstar == 2) {
						starsum = 1;
					} else if (checkstar == 2 && levelstar == 3) {
						starsum = 1;
					}
					startotal = starCount[0] + starsum;
					sqlQuery = string.Format ("UPDATE StarCount SET totalstar = \"{0}\" WHERE game = 1", startotal);
					dbCmd.CommandText = sqlQuery;
					dbCmd.ExecuteScalar ();
				}
			}
			dbConnection.Close ();
		}
	}
}
