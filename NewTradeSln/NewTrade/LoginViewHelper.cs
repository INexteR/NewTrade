using System;
using System.Runtime.CompilerServices;
using System.Windows.Markup;

namespace NewTrade
{
    public class LoginViewHelper : MarkupExtension
    {
        private static readonly ConditionalWeakTable<DependencyObject, ControlTemplate> templates = new();
        private static readonly RoutedEventHandler OnLoginPaswordTBoxLoaded = (s, e) =>
        {
            TextBox textBox = (TextBox)s;
            ControlTemplate errorTemplate = Validation.GetErrorTemplate(textBox);
            templates.AddOrUpdate(textBox, errorTemplate);
            Validation.SetErrorTemplate(textBox, null);
            textBox.TextChanged += OnLoginPaswordTBoxUnscribe;
        };

        private static void OnLoginPaswordTBoxUnscribe(object s, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)s;
            textBox.TextChanged -= OnLoginPaswordTBoxUnscribe;
            if (templates.TryGetValue(textBox, out var template))
                Validation.SetErrorTemplate(textBox, template);
        }

        public LoginViewEnum MemeberName { get; set; }

        public LoginViewHelper(LoginViewEnum memeberName)
        {
            MemeberName = memeberName;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            switch (MemeberName)
            {
                case LoginViewEnum.LoginPaswordTBox:
                    return OnLoginPaswordTBoxLoaded;
                default:
                    throw new NotImplementedException();
            }
        }

        public enum LoginViewEnum
        {
            LoginPaswordTBox
        }
    }
}
