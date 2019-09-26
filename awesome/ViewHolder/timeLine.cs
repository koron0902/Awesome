using System;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using Android.Runtime;

namespace awesome.ViewHolder {
	public class timeLine: RecyclerView.ViewHolder {
		public TextView created_;
		public TextView content_;
		public bool enabled = true;

		public timeLine(View itemView) : base(itemView) {
			created_ = itemView.FindViewById<TextView>(Resource.Id.postedTime);
			content_ = itemView.FindViewById<TextView>(Resource.Id.postedComment);
		}

		public timeLine(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) {
		}
	}
}
