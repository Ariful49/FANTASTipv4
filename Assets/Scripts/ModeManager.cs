using UnityEngine;
using System.Collections;
using System;
using System.Data;
using Mono.Data.Sqlite;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;

public class ModeManager : MonoBehaviour {
	private string connectionString;					//Deklarasi variabel yang digunakan untuk koneksi DB
	public GameObject selectmode2, selectmode3, lockmode2, lockmode3;

	String [] mode =new string[3];					//deklarasi array locked

	private List <Modes> Mod = new List <Modes>();		//deklarasi list dengan variabel kelas levels

	public void Start () {
		Path ("Level.sqlite");							//memanggil fungsi path dengan parameter nama database
		GetMode();
		ModeUnlock ();
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

	private void GetMode(){
		Mod.Clear ();																			//menghapus isi list Lv
		using (IDbConnection dbConnection = new SqliteConnection(connectionString)){			//fungsi untuk mengkonekkan dengan DB
			dbConnection.Open();
			using (IDbCommand dbCmd = dbConnection.CreateCommand ()) {							//fungsi untuk membuat Query
				string sqlQuery = "SELECT * FROM Mode";										//Deklarasi sqlQuery dengan query

				dbCmd.CommandText = sqlQuery;													//mengeksekusi Query
				using (IDataReader reader = dbCmd.ExecuteReader ()) {							//fungsi untuk Read DB
					while (reader.Read ()) {
						Mod.Add (new Modes (reader.GetInt32 (0), reader.GetString (1)));		//Menambahkan isi list Lv dengan isi tabel pada DB
					}
					int i = 0;
					foreach (Modes mod in Mod) {
						Debug.Log ("Mode "+ mod.Level + " is" + mod.isLocked);
						mode [i] = mod.isLocked;												//Set nilai array locked dengan kolom isLocked pada DB
						i++;
					}
					reader.Close ();															//menutup fungsi Read DB
				}
			}
			dbConnection.Close ();																//menutup koneksi DB
		}
	}

	private void ModeUnlock(){
		if (mode [1] == "unlock") {
			selectmode2.SetActive (true);
			lockmode2.SetActive (false);
		}
		if (mode [2] == "unlock") {
			selectmode3.SetActive (true);
			lockmode3.SetActive (false);
		}
	}
}