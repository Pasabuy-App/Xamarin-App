﻿using Android.Graphics.Drawables;
using PasaBuy.App.CustomRenderers;
using PasaBuy.App.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Application = Android.App.Application;

[assembly: ExportRenderer(typeof(RoundedEntry), typeof(RoundedEntryRendererAndroid))]
namespace PasaBuy.App.Droid.Renderers
{
    public class RoundedEntryRendererAndroid : EntryRenderer
    {
        public RoundedEntryRendererAndroid() : base(Application.Context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {

                var gradientDrawable = new GradientDrawable();
                gradientDrawable.SetCornerRadius(30f);
                gradientDrawable.SetStroke(5, Android.Graphics.Color.Orange);
                gradientDrawable.SetColor(Android.Graphics.Color.White);
                Control.SetBackground(gradientDrawable);

                Control.SetPadding(50, Control.PaddingTop, Control.PaddingRight, Control.PaddingBottom);
            }
        }

    }
}


