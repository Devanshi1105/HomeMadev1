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

namespace LetsCookApp.Droid.Helper
{
    public class MyGallery : Gallery
    {
        private int slop;
        private float initialX;
        private float initialY;

        public MyGallery(Context context) : base(context)
        {
            slop = ViewConfiguration.Get(context).ScaledTouchSlop;
        }

        public override bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
        {
            return false;
        }

        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {
            switch (ev.Action)
            {
                case MotionEventActions.Down:
                    OnTouchEvent(ev);

                    initialX = ev.GetX();
                    initialY = ev.GetY();

                    return false;
                case MotionEventActions.Move:
                    float distX = Math.Abs(ev.GetX() - initialX);
                    float distY = Math.Abs(ev.GetY() - initialY);

                    if (distY > distX && distY > slop)
                        return false;

                    return distX > slop;

                default:
                    return false;
            }
        }
    }
}