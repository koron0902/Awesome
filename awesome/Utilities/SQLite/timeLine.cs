using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Database.Sqlite;
using Android.Database;
using Android.Runtime;

namespace awesome.Utilities.SQLite {
  public class TimeLine : SQLiteOpenHelper {
    private static int databaseVersion_ => 1;
    private static string databaseName_ => "timeline.db";
    private Model.SQLite.TimeLine timelineModel_;

    public TimeLine(Context _context) : base(_context, databaseName_, null, databaseVersion_) {
      timelineModel_ = new Model.SQLite.TimeLine();
    }

    public override void OnCreate(SQLiteDatabase db) {
      db.ExecSQL(timelineModel_.createEntry());
    }

    public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
      db.ExecSQL(timelineModel_.deleteEntry());
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

    public void update(Model.SQLite.TimeLine.column _column) {
      var db = this.WritableDatabase;
      db.Update(timelineModel_.tableName_, timelineModel_.updateEntry(_column), Model.SQLite.TimeLine.HEADER.localId_ + " == ? ", new string[] { _column.localId_});
    }

    public void update(Model.UI.timeLineRow _row) {
      Utilities.Model.SQLite.TimeLine.column column = new Utilities.Model.SQLite.TimeLine.column();
      column.localId_ = _row.localId;
      column.enabled_ = _row.enabled.ToString();
      column.time_ = _row.createdAt_;
      column.text_ = _row.content_;
      update(column);
    }

    public void update(List<Model.UI.timeLineRow> _rows) {
      foreach(var row in _rows) {
        update(row);
      }
    }

    public List<Utilities.Model.UI.timeLineRow> read() {
      var db = ReadableDatabase;

      var cursor = db.Query(timelineModel_.tableName_,
        new string[] {
          Model.SQLite.TimeLine.HEADER.name_,
          Model.SQLite.TimeLine.HEADER.text_,
          Model.SQLite.TimeLine.HEADER.time_,
          Model.SQLite.TimeLine.HEADER.localId_,
          Model.SQLite.TimeLine.HEADER.enabled_},
        null,
        null,
        null,
        null,
        null);


      List<Model.UI.timeLineRow> row_ = new List<Model.UI.timeLineRow>();
      cursor.MoveToFirst();
      for(var i = 0;i < cursor.Count;i++) {
        row_.Add(new Model.UI.timeLineRow(cursor.GetString(2), cursor.GetString(1), cursor.GetString(3), bool.Parse(cursor.GetString(4))));
        cursor.MoveToNext();
      }
      cursor.Close();

      return row_;
      //return str;
    }

    /// TODO:
    /// Search系の関数を実装する．
    /// Searchキーとしてのenumも実装の必要あり．
    public List<Utilities.Model.UI.timeLineRow> search(string _at = null, string _until = null, string _from = null) {
      string query = null;
      List<string> dataSet = new List<string>();

      if(_at != null) {
        query += query == null ? "date(Time) == ?" : " and date(Time) == ?";
        dataSet.Add(_at);
      } else {
        if(_until != null) {
          query += query == null ? "date(Time) <= ?" : " and date(Time) <= ?";
          dataSet.Add(_until);
        }

        if(_from != null) {
          query += query == null ? "date(Time) >= ?" : " and date(Time) >= ?";
          dataSet.Add(_from);
        }
      }

      var db = ReadableDatabase;
      var cursor = db.Query(timelineModel_.tableName_,
        new string[] {
          Model.SQLite.TimeLine.HEADER.name_,
          Model.SQLite.TimeLine.HEADER.text_,
          Model.SQLite.TimeLine.HEADER.time_,
          Model.SQLite.TimeLine.HEADER.localId_,
          Model.SQLite.TimeLine.HEADER.enabled_},
        query,
        dataSet.Count == 0 ? null : dataSet.ToArray(),
        null,
        null,
        null);


      List<Model.UI.timeLineRow> row_ = new List<Model.UI.timeLineRow>();
      cursor.MoveToFirst();
      for(var i = 0;i < cursor.Count;i++) {
        row_.Add(new Model.UI.timeLineRow(cursor.GetString(2), cursor.GetString(1), cursor.GetString(3), bool.Parse(cursor.GetString(4))));
        cursor.MoveToNext();
      }
      cursor.Close();

      return row_;
    }
  }
}
