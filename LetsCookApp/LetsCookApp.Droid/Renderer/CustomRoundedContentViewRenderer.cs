using Android.Content;
using Xamarin.Forms;
using LetsCookApp.Droid.Renderer;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;
using Android.Util;
using LetsCookApp.CustomViews;

[assembly: ExportRenderer(typeof(CustomRoundedContentView), typeof(CustomRoundedContentViewRenderer))]
namespace LetsCookApp.Droid.Renderer
{
    public class CustomRoundedContentViewRenderer : VisualElementRenderer<ContentView>
    {
        private float _cornerRadius;
        private RectF _bounds;
        private Path _path;

        Bitmap _offscreenBitmap;
        Canvas _offscreenCanvas;
        private Bitmap maskBitmap;
        private Paint paint, maskPaint;

        protected override void OnElementChanged(ElementChangedEventArgs<ContentView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                return;
            }

            var element = (CustomRoundedContentView)Element;

            _cornerRadius = TypedValue.ApplyDimension(ComplexUnitType.Dip, (float)element.CornerRadius,
                Context.Resources.DisplayMetrics);

            paint = new Paint(PaintFlags.AntiAlias);

            maskPaint = new Paint(PaintFlags.AntiAlias | PaintFlags.FilterBitmap);
            maskPaint.SetXfermode(new PorterDuffXfermode(PorterDuff.Mode.Clear));
        }

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);
            if (w != oldw && h != oldh)
            {
                _bounds = new RectF(0, 0, w, h);
            }

            //_path = new Path();
            //_path.Reset();
            //_path.AddRoundRect(_bounds, _cornerRadius, _cornerRadius, Path.Direction.Cw);
            //_path.Close();
        }

        //public override void Draw(Canvas canvas)
        //{
        //    canvas.Save();
        //    canvas.ClipPath(_path);
        //    base.Draw(canvas);
        //    canvas.Restore();
        //}

        public override void Draw(Canvas canvas)
        {
            if (canvas.Width > 0 && canvas.Height > 0)
            {
                if (_offscreenBitmap == null)
                    _offscreenBitmap = Bitmap.CreateBitmap(canvas.Width, canvas.Height, Bitmap.Config.Argb8888);

                if (_offscreenCanvas == null)
                    _offscreenCanvas = new Canvas(_offscreenBitmap);

                base.Draw(_offscreenCanvas);

                if (maskBitmap == null)
                    maskBitmap = createMask(canvas.Width, canvas.Height);

                _offscreenCanvas.DrawBitmap(maskBitmap, 0f, 0f, maskPaint);
                canvas.DrawBitmap(_offscreenBitmap, 0f, 0f, paint);
            }
        }
        private Bitmap createMask(int width, int height)
        {
            Bitmap _mask = Bitmap.CreateBitmap(width, height, Bitmap.Config.Alpha8);
            Canvas _canvas = new Canvas(_mask);
            Paint _paint = new Paint(PaintFlags.AntiAlias);

            _canvas.DrawRect(0, 0, width, height, _paint);

            _paint.SetXfermode(new PorterDuffXfermode(PorterDuff.Mode.Clear));
            _canvas.DrawRoundRect(_bounds, _cornerRadius, _cornerRadius, _paint);

            return _mask;
        }

    }
}