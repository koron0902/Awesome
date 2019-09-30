
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
    EditText et_;
    protected override void OnCreate(Bundle savedInstanceState) {
      base.OnCreate(savedInstanceState);

      // Create your application here
      SetContentView(Resource.Layout.activity_post);

      Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar_post);
      toolbar.Title = "";
      SetSupportActionBar(toolbar);
      SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_mtrl_chip_close_circle);
      SupportActionBar.SetDisplayHomeAsUpEnabled(true);

      et_ = FindViewById<EditText>(Resource.Id.comment);

      FindViewById<Button>(Resource.Id.postButton).Click += (sender, e) => {
        SetResult(Result.Ok,
          new Intent().PutExtra("POSTED_COMMENT", et_.Text));
        Finish();
      };
    }

    public override bool OnCreateOptionsMenu(IMenu menu) {
      MenuInflater.Inflate(Resource.Menu.menu_post, menu);
      return true;
    }

    public override bool OnSupportNavigateUp() {
      SetResult(Result.Canceled);
      Finish();
      return base.OnSupportNavigateUp();
    }

    public override bool OnOptionsItemSelected(IMenuItem item) {
      int id = item.ItemId;

      return base.OnOptionsItemSelected(item);
    }
  }
}
