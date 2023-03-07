using System.Windows;

namespace Proxy
{
    public class ProxyBridge : Freezable
    {
        protected override Freezable CreateInstanceCore()
            => new ProxyBridge();


        /// <summary>Источник значений.</summary>
        public object Source
        {
            get => GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        /// <summary><see cref="DependencyProperty"/> for property <see cref="Source"/>.</summary>
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register(nameof(Source), typeof(object), typeof(ProxyBridge), new PropertyMetadata(null, SourceChanged));

        private static void SourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ProxyBridge)d).Target = e.NewValue;
        }


        /// <summary>Цель для значений.</summary>
        public object Target
        {
            get => GetValue(TargetProperty);
            set => SetValue(TargetProperty, value);
        }

        /// <summary><see cref="DependencyProperty"/> для свойства <see cref="Target"/>.</summary>
        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.Register(nameof(Target), typeof(object), typeof(ProxyBridge), new FrameworkPropertyMetadata(null) { BindsTwoWayByDefault = true });
    }
}
