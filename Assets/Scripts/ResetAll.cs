using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ResetAll : MonoBehaviour {
	private string connectionString;
	// Use this for initialization
	public void StartReset () {
		Path ("Level.sqlite");
		DeleteScore ();
		DeleteLevelUnlock ();
		DeleteStar ();
		DeleteModeUnlock ();
		DeleteStarCount ();
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



	private void DeleteScore(){
		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) {
			dbConnection.Open ();
			using (IDbCommand dbCmd = dbConnection.CreateCommand ()) {
				string sqlQuery = string.Format ("UPDATE HighScores SET score = 0");
				dbCmd.CommandText = sqlQuery;
				dbCmd.ExecuteScalar ();
			}
			dbConnection.Close ();
		}
	}
	private void DeleteStar(){
		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) {
			dbConnection.Open ();
			using (IDbCommand dbCmd = dbConnection.CreateCommand ()) {
				string sqlQuery = string.Format ("UPDATE Levels_star SET Star = 0");
				dbCmd.CommandText = sqlQuery;
				dbCmd.ExecuteScalar ();
			}
			dbConnection.Close ();
		}
	}

	private void DeleteModeUnlock(){
		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) {
			dbConnection.Open ();
			using (IDbCommand dbCmd = dbConnection.CreateCommand ()) {
				string sqlQuery = string.Format ("UPDATE Mode SET isLocked = 'lock' WHERE Mode = 2;\nUPDATE Mode SET isLocked = 'lock' WHERE Mode = 3;");
				dbCmd.CommandText = sqlQuery;
				dbCmd.ExecuteScalar ();
			}
			dbConnection.Close ();
		}
	}

	private void DeleteStarCount(){
		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) {
			dbConnection.Open ();
			using (IDbCommand dbCmd = dbConnection.CreateCommand ()) {
				string sqlQuery = string.Format ("UPDATE StarCount SET totalstar=0");
				dbCmd.CommandText = sqlQuery;
				dbCmd.ExecuteScalar ();
			}
			dbConnection.Close ();
		}
	}

	private void DeleteLevelUnlock(){
		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) {
			dbConnection.Open ();
			using (IDbCommand dbCmd = dbConnection.CreateCommand ()) {
				string sqlQuery = string.Format ("UPDATE Levels SET IsLocked = 'lock' WHERE Level= 2;\nUPDATE Levels SET IsLocked = 'lock' WHERE Level= 3;\nUPDATE Levels SET IsLocked = 'lock' WHERE Level= 4;\nUPDATE Levels SET IsLocked = 'lock' WHERE Level= 5;\nUPDATE Levels SET IsLocked = 'lock' WHERE Level= 6;\nUPDATE Levels SET IsLocked = 'lock' WHERE Level= 7;\nUPDATE Levels SET IsLocked = 'lock' WHERE Level= 8;\nUPDATE Levels SET IsLocked = 'lock' WHERE Level= 10;\nUPDATE Levels SET IsLocked = 'lock' WHERE Level= 11;\nUPDATE Levels SET IsLocked = 'lock' WHERE Level= 12;\nUPDATE Levels SET IsLocked = 'lock' WHERE Level= 13;\nUPDATE Levels SET IsLocked = 'lock' WHERE Level= 14;\nUPDATE Levels SET IsLocked = 'lock' WHERE Level= 15;\nUPDATE Levels SET IsLocked = 'lock' WHERE Level= 16;\nUPDATE Levels SET IsLocked = 'lock' WHERE Level= 18;\nUPDATE Levels SET IsLocked = 'lock' WHERE Level= 19;\nUPDATE Levels SET IsLocked = 'lock' WHERE Level= 20;\nUPDATE Levels SET IsLocked = 'lock' WHERE Level= 21;\nUPDATE Levels SET IsLocked = 'lock' WHERE Level= 22;\nUPDATE Levels SET IsLocked = 'lock' WHERE Level= 23;\nUPDATE Levels SET IsLocked = 'lock' WHERE Level= 24;");
				dbCmd.CommandText = sqlQuery;
				dbCmd.ExecuteScalar ();
			}
			dbConnection.Close ();
		}
	}

	public class LoadLevel : MonoBehaviour {
		public void ChangeScene(string sceneName) {
			SceneManager.LoadScene (sceneName);			//memanggil scene dari parameter sceneName
		}
	}
}
