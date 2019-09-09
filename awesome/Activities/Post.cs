
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Android.Support.Design.Widget;
using Android.Support.V7.App;

namespace awesome.Activities {
	[Activity(Label = "Post", Theme = "@style/AppTheme.NoActionBar")]
	public class Post : AppCompatActivity {
		protected override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);

			// Create your application here
			SetContentView(Resource.Layout.activity_post);

			Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar_post);
			toolbar.Title = "";
			SetSupportActionBar(toolbar);
		}
		public override bool OnCreateOptionsMenu(IMenu menu) {
			MenuInflater.Inflate(Resource.Menu.menu_post, menu);
			return true;
		}

		public override bool OnOptionsItemSelected(IMenuItem item) {
			int id = item.ItemId;
			if(id == Resource.Id.menu_posting) {
				Finish();
				return true;
			}

			return base.OnOptionsItemSelected(item);
		}
	}
}
