using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace XamlSetMarkupExtensionExample.MarkupExtensions
{
    [XamlSetMarkupExtension(nameof(ReceiveMarkupExtension))]
    [MarkupExtensionReturnType(typeof(string))]
    public class BindingPathExtension : MarkupExtension
    {
        public BindingPathExtension()
        {
        }

        public BindingPathExtension(Binding binding)
        {
            // this constructor is getting called
            BindingPath = binding.Path.Path;
            MemberType = binding.RelativeSource?.AncestorType;
        }

        [ConstructorArgument(nameof(BindingPath))]
        public string BindingPath { get; set; }

        public Type MemberType { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return $"Path={BindingPath}, Member={MemberType}";
        }

        public static void ReceiveMarkupExtension(object targetObject, XamlSetMarkupExtensionEventArgs eventArgs)
        {
            if (eventArgs == null)
            {
                throw new ArgumentNullException(nameof(eventArgs));
            }

            var binding = eventArgs.MarkupExtension as Binding;
            if (binding == null)
            {
                return;
            }

            var bindingPathExtension = targetObject as BindingPathExtension;
            if (bindingPathExtension == null)
            {
                return;
            }

            bindingPathExtension.BindingPath = binding.Path.Path;
            bindingPathExtension.MemberType = binding.RelativeSource?.AncestorType;
            eventArgs.Handled = true;
        }
    }
}