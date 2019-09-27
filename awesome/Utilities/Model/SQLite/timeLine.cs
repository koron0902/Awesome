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
			public string enabled_;
		}

		public static column HEADER;

		public TimeLine() {
			HEADER = new column {
				globalId_ = "gId",
				localId_ = "lId",
				name_ = "Name",
				text_ = "Text",
				time_ = "Time",
				enabled_ = "Enabled"
			};
		}

		public string createEntry() {
			return "create table " + tableName_ + " (" +
				HEADER.globalId_ + " integer," +
				HEADER.localId_+ " integer primary key autoincrement," +
				HEADER.name_ + " text," +
				HEADER.time_ + " text," +
				HEADER.text_ + " text," +
				HEADER.enabled_ + " text)";
		}

		public ContentValues insertEntry(column _column) {
			ContentValues values = new ContentValues();
			values.Put("gId", _column.globalId_);
			values.Put("Name", _column.name_);
			values.Put("Time", _column.time_);
			values.Put("Text", _column.text_);
			values.Put("Enabled", _column.enabled_);

			return values;
		}

		public ContentValues updateEntry(column _column) {
			ContentValues values = new ContentValues();

			values.Put("gId", _column.globalId_);
			values.Put("Name", _column.name_);
			values.Put("Time", _column.time_);
			values.Put("Text", _column.text_);
			values.Put("Enabled", _column.enabled_);

			return values;
		}

		public string deleteEntry() {
			return "drop table if exists " + tableName_;
		}
	}
}
