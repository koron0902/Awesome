using System;

using Android.OS;
using Android.Widget;
using Android.Content;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Views.Animations;
using Android.Runtime;
using Android.App;
using Android.Graphics;

namespace awesome.Adapter {
	public class timeLine : RecyclerView.Adapter {
		List<awesome.Utilities.Model.UI.timeLineRow> rows_;
		Activity activity_;
		public timeLine(Activity _activity, List<Utilities.Model.UI.timeLineRow> _rows) {
			activity_ = _activity;
			rows_ = _rows;
		}

		public timeLine(Activity _activity) {
			activity_ = _activity;
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
