
using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Database.Sqlite;
using Android.Database;
using Android.Runtime;

namespace awesome.Utilities.SQLite {
	public class TimeLine : SQLiteOpenHelper {
		private static int databaseVersion_ => 3;
		private static string databaseName_ => "Awesome.db";
		private Model.SQLite.TimeLine timelineModel_;

		public TimeLine(Context _context) : base(_context, databaseName_, null, databaseVersion_) {
			timelineModel_ = new Model.SQLite.TimeLine();
		}

		public override void OnCreate(SQLiteDatabase db) {
			db.ExecSQL(timelineModel_.createEntry());
		}

		public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
			OnCreate(db);
		}

		public override void OnDowngrade(SQLiteDatabase db, int oldVersion, int newVersion) {
			base.OnDowngrade(db, oldVersion, newVersion);
		}

		public override void OnOpen(SQLiteDatabase db) {
			base.OnOpen(db);
		}

		public override void OnConfigure(SQLiteDatabase db) {
			base.OnConfigure(db);
		}

		public void write(Model.SQLite.TimeLine.column _column) {
			var db = this.WritableDatabase;
			db.Insert(timelineModel_.tableName_, null, timelineModel_.insertEntry(_column));
		}

		public string read() {
			var db = ReadableDatabase;
			var cursor = db.Query(timelineModel_.tableName_,
				new string[] {
					Model.SQLite.TimeLine.HEADER.name_,
					Model.SQLite.TimeLine.HEADER.text_ },
				null,
				null,
				null,
				null,
				null);


			var str = "";
			cursor.MoveToFirst();
			for(var i = 0;i < cursor.Count; i++) {
				str += cursor.GetString(0) + " : " + cursor.GetString(1) + "\n";
				cursor.MoveToNext();
			}
			cursor.Close();

			return str;
		}
	}
}