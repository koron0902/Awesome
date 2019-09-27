using System;
namespace awesome.Utilities.Model.UI {
	public class timeLineRow {
		public string createdAt_ { get; private set; }
		public string content_ { get; private set; }
		public bool enabled = true;
		public int localId;

		public timeLineRow(string _created, string _content, int _lId = 0, bool _enabled = true) {
			createdAt_ = _created;
			content_ = _content;
			localId = _lId;
			enabled = _enabled;
		}
	}
}
