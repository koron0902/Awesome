using System;
using Android.Content;

namespace awesome.Utilities.Model.SQLite {
  public class UserInfo {
    public string tableName_ => "userinfo";

    public struct column {
      public string id_;
      public string name_;
      public string point_;
    }

    public static column HEADER;

    public UserInfo() {
      HEADER = new column {
        id_ = "Id",
        name_ = "Name",
        point_ = "Point"
      };
    }

    public string createEntry() {
      return "create table " + tableName_ + " (" +
          HEADER.id_ + " integer primary key autoincrement," +
        HEADER.name_ + "  text," +
        HEADER.point_ + " text)";
    }
  }
}
