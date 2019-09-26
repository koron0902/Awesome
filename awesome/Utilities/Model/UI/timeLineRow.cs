using System;
namespace awesome.Utilities.Model.UI {
	public class timeLineRow {
		public string createdAt_ { get; private set; }
		public string content_ { get; private set; }
		public bool enabled = true;

		public timeLineRow(string _created, string _content) {
			createdAt_ = _created;
			content_ = _content;
		}
	}
}
