﻿#if IOS
using Avalonia.Layout;
using Avalonia.Maui.Controls;
using Microsoft.Maui.Handlers;
using Avalonia.Maui.Platforms.iOS;
using AvaloniaControl = Avalonia.Controls.Control;

namespace Avalonia.Maui.Handlers
{
    public partial class AvaloniaViewHandler : ViewHandler<AvaloniaView, MauiAvaloniaView>
    {
        protected override MauiAvaloniaView CreatePlatformView()
        {
            return new MauiAvaloniaView(VirtualView);
        }

        protected override void ConnectHandler(MauiAvaloniaView platformView)
        {
            base.ConnectHandler(platformView);

            platformView.Content = VirtualView.Content as AvaloniaControl;
        }

        protected override void DisconnectHandler(MauiAvaloniaView platformView)
        {
            platformView.Dispose();

            base.DisconnectHandler(platformView);
        }

        public static void MapContent(AvaloniaViewHandler handler, AvaloniaView view)
        {
            handler.PlatformView?.UpdateContent();
        }

        public override Microsoft.Maui.Graphics.Size GetDesiredSize(double widthConstraint, double heightConstraint)
        {
            if (VirtualView.Content is Layoutable control)
            {
                control.Measure(new Size(widthConstraint, heightConstraint));

                var size = new Size(control.DesiredSize.Width, control.DesiredSize.Height);

                base.GetDesiredSize(size.Width, size.Height);

                return new Microsoft.Maui.Graphics.Size(size.Width, size.Height);
            }
            else
            {
                return base.GetDesiredSize(widthConstraint, heightConstraint);
            }
        }
    }
}
#endif