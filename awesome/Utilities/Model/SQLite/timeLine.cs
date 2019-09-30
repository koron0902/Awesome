using System;
using Android.Content;
namespace awesome.Utilities.Model.SQLite {
  public class TimeLine {
    public string tableName_ => "timeline";

    public struct column {
      public string globalId_;
      public string localId_;
      public string name_;
      public string time_;
      public string text_;
    }

    public static column HEADER;

    public TimeLine() {
      HEADER = new column {
        globalId_ = "gId",
        localId_ = "lId",
        name_ = "Name",
        text_ = "Text",
        time_ = "Time"
      };
    }

    public string createEntry() {
      return "create table " + tableName_ + " (" +
        HEADER.globalId_ + " integer," +
        HEADER.localId_ + " integer primary key autoincrement," +
        HEADER.name_ + " text," +
        HEADER.time_ + " text," +
        HEADER.text_ + " text)";
    }

    public ContentValues insertEntry(column _column) {
      ContentValues values = new ContentValues();
      values.Put("gId", _column.globalId_);
      values.Put("name", _column.name_);
      values.Put("time", _column.time_);
      values.Put("text", _column.text_);

      return values;
    }
  }
}
