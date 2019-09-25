using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace awesome {
	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
	public class MainActivity : AppCompatActivity {
		static readonly int POST_REQUEST_CODE_ = 0x01;
		Utilities.SQLite.TimeLine timeLine_;

		protected override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);
			Xamarin.Essentials.Platform.Init(this, savedInstanceState);
			SetContentView(Resource.Layout.activity_main);

			Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
			SetSupportActionBar(toolbar);

			FindViewById<FloatingActionButton>(Resource.Id.fab).Click += (sender, e) => {
				StartActivityForResult(new Android.Content.Intent(ApplicationContext, typeof(Activities.Post)), POST_REQUEST_CODE_);
			};

			timeLine_ = new Utilities.SQLite.TimeLine(ApplicationContext);
		}

		public override bool OnCreateOptionsMenu(IMenu menu) {
			MenuInflater.Inflate(Resource.Menu.menu_main, menu);
			return true;
		}

		public override bool OnOptionsItemSelected(IMenuItem item) {
			int id = item.ItemId;
			if(id == Resource.Id.action_settings) {
				return true;
			}

			return base.OnOptionsItemSelected(item);
		}

		protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data) {
			base.OnActivityResult(requestCode, resultCode, data);
			if(requestCode == POST_REQUEST_CODE_) {
				if(resultCode == Result.Ok) {
					var posted = data.GetStringExtra("POSTED_COMMENT");
					var now = DateTime.Now.ToLocalTime().ToString("HH:mm:ss");
					Utilities.Model.SQLite.TimeLine.column column = new Utilities.Model.SQLite.TimeLine.column();
					column.name_ = "adad";
					column.text_ = posted;
					column.time_ = now;

					timeLine_.write(column);
				}
			}

		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults) {
			Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}
	}
}