using System;
namespace awesome.Model {
	public class timeLineRow {
		public string createdAt_ { get; private set; }
		public string content_ { get; private set; }

		public timeLineRow(string _created, string _content) {
			createdAt_ = _created;
			content_ = _content;
		}
	}
}
