using UnityEngine;
using System.Collections;
using System;
using System.Data;
using Mono.Data.Sqlite;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;

public class ShowStarCount : MonoBehaviour {
	private string connectionString;					//Deklarasi variabel yang digunakan untuk koneksi DB

	int [] starCount=new int[1];

	Text StarCount;
	int startotal;
	private List <StarsCount> Stc = new List <StarsCount>();

	void Start () {
		StarCount=GetComponent<Text> ()as Text;
		Path ("Level.sqlite");							//memanggil fungsi path dengan parameter nama database
		GetStarCount ();
	}
	public void Path(string p){
		#if UNITY_EDITOR												//Jika fungsi dijalankan di editor unity
		var dbPath = string.Format(@"Assets/StreamingAssets/{0}", p);	//Set variabel dbPath dengan format string direktori database
		#else
		string filepath = Application.persistentDataPath + "/" + p;		//Set variabel filepath dengan direktori persistent data path atau direktori yang digunakan secara umum oleh user 
		if (!File.Exists(filepath))										//Jika tidak ada file database di direktori tsb
		{
			Debug.Log("Database not in Persistent path");
		#if UNITY_ANDROID 												//Jika fungsi dijalankan di perangkat android
		WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/" + p);	//database yang ada dalam asset akan di load ke direktori android
		while(!loadDB.isDone) {}														
		File.WriteAllBytes(filepath, loadDB.bytes);										//melakukan proses write database di dalam direktori android
		#endif
		}
		var dbPath = filepath;											//set variabel dbPath dengan filepath
		#endif
		connectionString = "URI=file:" + dbPath;						//set variabel connectionString dengan URI file database
	}

	private void GetStarCount(){
		Stc.Clear ();																			//menghapus isi list Lv
		using (IDbConnection dbConnection = new SqliteConnection(connectionString)){			//fungsi untuk mengkonekkan dengan DB
			dbConnection.Open();
			using (IDbCommand dbCmd = dbConnection.CreateCommand ()) {							//fungsi untuk membuat Query
				string sqlQuery = "SELECT * FROM StarCount";										//Deklarasi sqlQuery dengan query
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
					startotal = starCount [0];
					StarCount.text = startotal.ToString();
					reader.Close ();															//menutup fungsi Read DB
				}
				if (startotal > 19) {
					sqlQuery = string.Format ("UPDATE Mode SET isLocked = 'unlock' WHERE Mode = 2");
					dbCmd.CommandText = sqlQuery;
					dbCmd.ExecuteScalar ();
				}
				if (startotal > 34) {
					sqlQuery = string.Format ("UPDATE Mode SET isLocked = 'unlock' WHERE Mode = 3");
					dbCmd.CommandText = sqlQuery;
					dbCmd.ExecuteScalar ();
				}
			}
			dbConnection.Close ();																//menutup koneksi DB
		}
	}
}