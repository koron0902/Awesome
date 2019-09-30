using System;
using Android.Views.Animations;

namespace awesome.Utilities.Animation {
  public class Horizontal {
    public struct pos {
      public float startX_;
      public float startY_;
      public float endX_;
      public float endY_;
    }

    pos pos_;
    int duration_;

    public Horizontal() {
    }

    public Horizontal StartPos(float _x, float _y) {
      pos_.startX_ = _x;
      pos_.startY_ = _y;
      pos_.endX_ = _x;
      return this;
    }

    public Horizontal MoveDistance(float _px) {
      pos_.endY_ = pos_.startY_ + _px;
      return this;
    }

    public Horizontal Duration(int _duration) {
      duration_ = _duration;
      return this;
    }

    public TranslateAnimation Build() {
      var anim = new TranslateAnimation(pos_.startX_,
        pos_.endX_,
        pos_.startY_,
        pos_.endY_);
      anim.Duration = duration_;

      return anim;
    }
  }
}
