using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Database.Sqlite;
using Android.Database;
using Android.Runtime;

namespace awesome.Utilities.SQLite {
  public class UserInfo : SQLiteOpenHelper {
    private static int databaseVersion_ => 2;
    private static string databaseName_ => "Awesome_UserInfo.db";
    private Model.SQLite.UserInfo userInfoModel_;

    public UserInfo(Context _context): base(_context, databaseName_, null, databaseVersion_) {
      userInfoModel_ = new Model.SQLite.UserInfo();
    }

    public override void OnCreate(SQLiteDatabase db) {
      db.ExecSQL(userInfoModel_.createEntry());
    }

    public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
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

    public void write() {
      var db = this.WritableDatabase;
      var value = new ContentValues();
      value.Put(Model.SQLite.UserInfo.HEADER.point_, 0);
      db.Insert(userInfoModel_.tableName_, null, value);
    }

    public void update(string _point) {
      var db = this.WritableDatabase;
      var value = new ContentValues();
      value.Put(Model.SQLite.UserInfo.HEADER.point_, _point);
      db.Update(userInfoModel_.tableName_, value, Model.SQLite.UserInfo.HEADER.id_ + " == ?", new string[] { "1" });
    }

    public int read() {
      var db = this.ReadableDatabase;
      var cursor = db.Query(userInfoModel_.tableName_,
        new string[] { Model.SQLite.UserInfo.HEADER.point_ },
        "Id == ?",
        new string[] { "1" },
        null,
        null,
        null);

      cursor.MoveToFirst();
      if(cursor.Count < 1) return 0;
      var point = cursor.GetInt(0);
      cursor.Close();

      return point;
    }
  }
}
