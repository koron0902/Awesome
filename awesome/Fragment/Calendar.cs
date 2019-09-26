
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;

namespace awesome.Fragment {
	public class Calendar : DialogFragment {
		Java.Util.Calendar calendar_;
		DatePickerDialog datePicker_;
		Context context_;
		public Calendar(Context _context) {
			context_ = _context;
		}

		protected Calendar(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) {
		}

		public override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);
		}


		public override Dialog OnCreateDialog(Bundle savedInstanceState) {
			base.OnCreateDialog(savedInstanceState);
			calendar_ = Java.Util.Calendar.Instance;
			datePicker_ = new DatePickerDialog(context_);
			datePicker_.DateSet += (sender, e) => {
				var str = e.Date.ToLocalTime().ToString("YYYY-MM-DD HH:mm:ss");
			};
			calendar_.Get(Java.Util.CalendarField.Year);
			calendar_.Get(Java.Util.CalendarField.Month);
			calendar_.Get(Java.Util.CalendarField.DayOfMonth);
			return datePicker_;
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);

			return base.OnCreateView(inflater, container, savedInstanceState);
		}
	}
}
