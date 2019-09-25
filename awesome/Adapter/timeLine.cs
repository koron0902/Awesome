using System;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;

namespace awesome.Adapter {
	public class timeLine : RecyclerView.Adapter {
		List<awesome.Model.UI.timeLineRow> rows_;
		public timeLine(List<Model.UI.timeLineRow> _rows) {
			rows_ = _rows;
		}

		public override int ItemCount => rows_.Count;

		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
			((ViewHolder.timeLine)(holder)).created_.Text = rows_[position].createdAt_;
			((ViewHolder.timeLine)(holder)).content_.Text = rows_[position].content_;
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
