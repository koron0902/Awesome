using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;

namespace awesome {
	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
	public class MainActivity : AppCompatActivity {
		static readonly int POST_REQUEST_CODE_ = 0x01;
		Utilities.SQLite.TimeLine timeLine_;
		List<Utilities.Model.UI.timeLineRow> rows_;
		Adapter.timeLine adapter_;
		LinearLayoutManager manager_;
		DateTime dateTime_;

		protected override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);
			Xamarin.Essentials.Platform.Init(this, savedInstanceState);
			SetContentView(Resource.Layout.activity_main);

			Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
			toolbar.Title = "";
			SetSupportActionBar(toolbar);

			FindViewById<FloatingActionButton>(Resource.Id.fab).Click += (sender, e) => {
				StartActivityForResult(new Android.Content.Intent(ApplicationContext, typeof(Activities.Post)), POST_REQUEST_CODE_);
			};

			timeLine_ = new Utilities.SQLite.TimeLine(ApplicationContext);
			var recycler = FindViewById<RecyclerView>(Resource.Id.timeLine);

			dateTime_ = DateTime.Today;
			FindViewById<TextView>(Resource.Id.year).Text = dateTime_.Year.ToString("D4") + "年";
			FindViewById<TextView>(Resource.Id.month).Text = dateTime_.Month.ToString("D2") + "月";
			FindViewById<TextView>(Resource.Id.day).Text = dateTime_.Day.ToString("D2") + "日";

			/// TODO:
			/// onCreate()で重い処理を実行することは
			/// 起動時間的にも懸念事項となるので将来的に
			/// は別のところで実行をする．
			rows_ = new List<Utilities.Model.UI.timeLineRow>();
			rows_.AddRange(timeLine_.search(_at:dateTime_.ToLocalTime().ToString("yyyy-MM-dd")));
			adapter_ = new Adapter.timeLine(this, rows_);
			manager_ = new LinearLayoutManager(this);
			recycler.HasFixedSize = false;
			recycler.SetLayoutManager(manager_);
			recycler.SetAdapter(adapter_);


			FindViewById<RelativeLayout>(Resource.Id.date).Click += (sender, e) => {
				var c = new Fragment.Calendar(this);
				c.onDataSelectChanged += (e2) => {
					dateTime_ = e2.Date;
					FindViewById<TextView>(Resource.Id.year).Text = dateTime_.Year.ToString("D4") + "年";
					FindViewById<TextView>(Resource.Id.month).Text = dateTime_.Month.ToString("D2") + "月";
					FindViewById<TextView>(Resource.Id.day).Text = dateTime_.Day.ToString("D2") + "日";
					rows_.Clear();
					rows_.AddRange(timeLine_.search(_at: dateTime_.ToLocalTime().ToString("yyyy-MM-dd")));
					adapter_.NotifyDataSetChanged();
				};
				if(c != null) {
					var manager = this.FragmentManager;
					if(manager != null) {
						c.Show(manager, "aaa");
					}
				}
			};
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
					///  TODO:
					///  そのうちサーバにプッシュしてグローバルIDを取得するようにする．
					///  IntentServiceあたりでいいかな．
					var posted = data.GetStringExtra("POSTED_COMMENT");
					var dateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
          
					Utilities.Model.SQLite.TimeLine.column column = new Utilities.Model.SQLite.TimeLine.column();
					column.name_ = "adad";
					column.text_ = posted;
					column.time_ = dateTime;

					timeLine_.write(column);

					if (dateTime.Split(' ')[0] == dateTime_.Date.ToString("yyyy-MM-dd")){
						rows_.Add(new Utilities.Model.UI.timeLineRow(column.time_, column.text_));
						adapter_.NotifyDataSetChanged();
					}
				}
			}

		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults) {
			Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}
	}
}