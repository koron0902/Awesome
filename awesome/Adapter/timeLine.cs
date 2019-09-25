using System;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;

namespace awesome.Adapter {
	public class timeLine : RecyclerView.Adapter {
		List<awesome.Utilities.Model.UI.timeLineRow> rows_;
		public timeLine(List<Utilities.Model.UI.timeLineRow> _rows) {
			rows_ = _rows;
		}

		public override int ItemCount => rows_.Count;

		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
			((ViewHolder.timeLine)(holder)).created_.Text = rows_[position].createdAt_;
			((ViewHolder.timeLine)(holder)).content_.Text = rows_[position].content_;
			((ViewHolder.timeLine)(holder)).content_.Click += (sender, e) => {
				((Android.Widget.TextView)sender).Enabled = false;
			};
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
			return new ViewHolder.timeLine(LayoutInflater
						.From(parent.Context)
						.Inflate(Resource.Layout.card,
								parent,
								false));
		}
	}
}
