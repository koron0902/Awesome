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
    public Action<awesome.Utilities.Model.UI.timeLineRow> onRowClicked;

    public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
      ((ViewHolder.timeLine)(holder)).created_.Text = DateTime.Parse(rows_[position].createdAt_).ToString("HH:mm:ss");
      ((ViewHolder.timeLine)(holder)).content_.Text = rows_[position].content_;
      ((ViewHolder.timeLine)(holder)).content_.Enabled = rows_[position].enabled;
      ((ViewHolder.timeLine)(holder)).content_.Click += (sender, e) => {
        TableLayout tableLayout = new TableLayout(activity_.ApplicationContext);
        tableLayout.SetGravity(GravityFlags.Bottom | GravityFlags.CenterHorizontal);
        tableLayout.SetBackgroundColor(Color.Argb(100, 160, 160, 160));
        activity_.AddContentView(tableLayout,
          new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent,
              ViewGroup.LayoutParams.MatchParent));


        WindowManagerLayoutParams layoutParams = new WindowManagerLayoutParams(
          600,
          600,
          WindowManagerTypes.SystemOverlay,
          WindowManagerFlags.NotTouchable |
          WindowManagerFlags.NotFocusable,
          Android.Graphics.Format.Translucent);
        ImageView imageView = new ImageView(activity_.ApplicationContext);
        imageView.SetImageResource(awesome.Resource.Drawable.met);
        imageView.SetPadding(0, 0, 0, 50);



        tableLayout.AddView(imageView, layoutParams);
        var upAnimation = new Utilities.Animation.Horizontal()
                                      .StartPos(imageView.GetX(),
                                                imageView.GetY())
                                      .MoveDistance(-80)
                                      .Duration(100)
                                      .Build();
        var downAnimation = new Utilities.Animation.Horizontal()
                                      .StartPos(imageView.GetX(),
                                                imageView.GetY() - 80)
                                      .MoveDistance(80)
                                      .Duration(100)
                                      .Build();
        imageView.StartAnimation(upAnimation);
        upAnimation.AnimationEnd += (_1, _2) => {
          imageView.StartAnimation(downAnimation);
        };

        downAnimation.AnimationEnd += (_1, _2) => {
          imageView.StartAnimation(upAnimation);
        };


        ((Android.Widget.TextView)sender).Enabled = false;
        rows_[position].enabled = false;


        new Handler().PostDelayed(() => {
          ((ViewGroup)tableLayout.Parent).RemoveView(tableLayout);
        }, 500);

        onRowClicked(rows_[position]);
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
