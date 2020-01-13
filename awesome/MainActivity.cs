using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace awesome {
  [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
  public class MainActivity : AppCompatActivity {
    static readonly int POST_REQUEST_CODE_ = 0x01;
    Utilities.SQLite.TimeLine timeLine_;
    Utilities.SQLite.UserInfo userInfo_;
    List<Utilities.Model.UI.timeLineRow> rows_;
    Adapter.timeLine adapter_;
    LinearLayoutManager manager_;
    DateTime dateTime_;

    public static int point_;

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
      userInfo_ = new Utilities.SQLite.UserInfo(ApplicationContext);
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
      rows_.AddRange(timeLine_.search(_at: dateTime_.ToLocalTime().ToString("yyyy-MM-dd")));
      adapter_ = new Adapter.timeLine(this, rows_);
      manager_ = new LinearLayoutManager(this);
      recycler.HasFixedSize = false;
      recycler.SetLayoutManager(manager_);
      recycler.SetAdapter(adapter_);

      adapter_.onRowClicked += (_row) => {
        if(!_row.enabled)
          return;
        else
          _row.enabled = false;

        point_++;
        userInfo_.update(point_.ToString());
        FindViewById<TextView>(Resource.Id.point).Text = point_.ToString();
        timeLine_.update(_row);
      };

      point_ = userInfo_.read();
      if(point_ == -1) {
        point_ = 0;
        userInfo_.write();
      }
      FindViewById<TextView>(Resource.Id.point).Text = point_.ToString();


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

    protected override void OnPause() {
      base.OnPause();
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
          column.localId_ = dateTime.GetHashCode().ToString("X") + posted.GetHashCode().ToString("X");
          column.name_ = "adad";
          column.text_ = posted;
          column.time_ = dateTime;
          column.enabled_ = true.ToString();

          timeLine_.write(column);

          if(dateTime.Split(' ')[0] == dateTime_.Date.ToString("yyyy-MM-dd")) {
            rows_.Add(new Utilities.Model.UI.timeLineRow(column.time_, column.text_, column.localId_));
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
