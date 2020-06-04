using UnityEngine;
using System.Collections;
using System;
using System.Data;
using Mono.Data.Sqlite;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;

public class LevelManagerCIDR : MonoBehaviour {

	private string connectionString;					//Deklarasi variabel yang digunakan untuk koneksi DB
	public GameObject l2lock,l2unlock,l2block, l3lock,l3unlock,l3block,l4lock,l4unlock,l4block,l5lock,l5unlock,l5block,l6lock,l6unlock,l6block,l7lock,l7unlock,l7block,l8lock,l8unlock,l8block;			//Deklarasi game Object
	public GameObject l1star1,l1star2,l1star3,l2star1,l2star2,l2star3,l3star1,l3star2,l3star3,l4star1,l4star2,l4star3,l5star1,l5star2,l5star3,l6star1,l6star2,l6star3,l7star1,l7star2,l7star3,l8star1,l8star2,l8star3;			//Deklarasi game Object

	String [] locked=new string[24];					//deklarasi array locked
	int [] star=new int[24];					//deklarasi array locked

	private List <Levels> Lv = new List <Levels>();		//deklarasi list dengan variabel kelas levels
	private List <Stars> St = new List <Stars>();		//deklarasi list dengan variabel kelas levels

	public void Start () {
		Path ("Level.sqlite");							//memanggil fungsi path dengan parameter nama database
		GetLevel ();									//memanggil fungsi GetLevel
		LevelUnlock ();									//memanggil fungsi LevelUnlock
		GetStar ();										//memanggil fungsi GetLevel
		LevelStar ();									//memanggil fungsi LevelUnlock
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

	private void GetLevel(){
		Lv.Clear ();																			//menghapus isi list Lv
		using (IDbConnection dbConnection = new SqliteConnection(connectionString)){			//fungsi untuk mengkonekkan dengan DB
			dbConnection.Open();
			using (IDbCommand dbCmd = dbConnection.CreateCommand ()) {							//fungsi untuk membuat Query
				string sqlQuery = "SELECT * FROM Levels";										//Deklarasi sqlQuery dengan query

				dbCmd.CommandText = sqlQuery;													//mengeksekusi Query
				using (IDataReader reader = dbCmd.ExecuteReader ()) {							//fungsi untuk Read DB
					while (reader.Read ()) {
						Lv.Add (new Levels (reader.GetInt32 (0), reader.GetString (1)));		//Menambahkan isi list Lv dengan isi tabel pada DB
					}
					int i = 0;
					foreach (Levels lev in Lv) {
						Debug.Log ("Level "+ lev.Level + " is" + lev.isLocked);
						locked [i] = lev.isLocked;												//Set nilai array locked dengan kolom isLocked pada DB
						i++;
					}
					reader.Close ();															//menutup fungsi Read DB
				}
			}
			dbConnection.Close ();																//menutup koneksi DB
		}
	}

	private void GetStar(){
		St.Clear ();																			//menghapus isi list Lv
		using (IDbConnection dbConnection = new SqliteConnection(connectionString)){			//fungsi untuk mengkonekkan dengan DB
			dbConnection.Open();
			using (IDbCommand dbCmd = dbConnection.CreateCommand ()) {							//fungsi untuk membuat Query
				string sqlQuery = "SELECT * FROM Levels_star";										//Deklarasi sqlQuery dengan query

				dbCmd.CommandText = sqlQuery;													//mengeksekusi Query
				using (IDataReader reader = dbCmd.ExecuteReader ()) {							//fungsi untuk Read DB
					while (reader.Read ()) {
						St.Add (new Stars (reader.GetInt32 (0), reader.GetInt32 (1)));		//Menambahkan isi list Lv dengan isi tabel pada DB
					}
					int i = 0;
					foreach (Stars levstar in St) {
						Debug.Log ("Level "+levstar.Level+ " get " + levstar.Star);
						star [i] = levstar.Star;												//Set nilai array locked dengan kolom isLocked pada DB
						i++;
					}
					reader.Close ();															//menutup fungsi Read DB
				}
			}
			dbConnection.Close ();																//menutup koneksi DB
		}
	}

	public void LevelUnlock(){
		if (locked[9] == "unlock") {
			l2lock.SetActive (false);
			l2block.SetActive (false);
			l2unlock.SetActive (true);
		}

		 if (locked[10] == "unlock") {
			l3lock.SetActive (false);
			l3block.SetActive (false);
			l3unlock.SetActive (true);
		}
		if (locked[11] == "unlock") {
			l4lock.SetActive (false);
			l4block.SetActive (false);
			l4unlock.SetActive (true);
		}
 		if (locked[12] == "unlock") {
			l5lock.SetActive (false);
			l5block.SetActive (false);
			l5unlock.SetActive (true);
		}
		if (locked[13] == "unlock") {
			l6lock.SetActive (false);
			l6block.SetActive (false);
			l6unlock.SetActive (true);
		}
		if (locked[14] == "unlock") {
			l7lock.SetActive (false);
			l7block.SetActive (false);
			l7unlock.SetActive (true);
		}
		if (locked[15] == "unlock") {
			l8lock.SetActive (false);
			l8block.SetActive (false);
			l8unlock.SetActive (true);
		}
	}

	public void LevelStar(){
		if (star [8] > 0) {
			l1star1.SetActive (true);
		}
		if (star [8] > 1) {
			l1star2.SetActive (true);
		}
		if (star [8] > 2) {
			l1star3.SetActive (true);
		}

		if (star [9] > 0) {
			l2star1.SetActive (true);
		}
		if (star [9] > 1) {
			l2star2.SetActive (true);
		}
		if (star [9] > 2) {
			l2star3.SetActive (true);
		}

		if (star [10] > 0) {
			l3star1.SetActive (true);
		}
		if (star [10] > 1) {
			l3star2.SetActive (true);
		}
		if (star [10] > 2) {
			l3star3.SetActive (true);
		}

		if (star [11] > 0) {
			l4star1.SetActive (true);
		}
		if (star [11] > 1) {
			l4star2.SetActive (true);
		}
		if (star [11] > 2) {
			l4star3.SetActive (true);
		}

		if (star [12] > 0) {
			l5star1.SetActive (true);
		}
		if (star [12] > 1) {
			l5star2.SetActive (true);
		}
		if (star [12] > 2) {
			l5star3.SetActive (true);
		}

		if (star [13] > 0) {
			l6star1.SetActive (true);
		}
		if (star [13] > 1) {
			l6star2.SetActive (true);
		}
		if (star [13] > 2) {
			l6star3.SetActive (true);
		}

		if (star [14] > 0) {
			l7star1.SetActive (true);
		}
		if (star [14] > 1) {
			l7star2.SetActive (true);
		}
		if (star [14] > 2) {
			l7star3.SetActive (true);
		}
		if (star [15] > 0) {
			l8star1.SetActive (true);
		}
		if (star [15] > 1) {
			l8star2.SetActive (true);
		}
		if (star [15] > 2) {
			l8star3.SetActive (true);
		}
	}
}