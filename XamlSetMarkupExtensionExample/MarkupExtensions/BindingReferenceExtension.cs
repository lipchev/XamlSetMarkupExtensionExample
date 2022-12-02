using System;
using System.Windows.Data;
using System.Windows.Markup;

namespace XamlSetMarkupExtensionExample.MarkupExtensions
{
    [MarkupExtensionReturnType(typeof(string))]
    public class BindingReferenceExtension : MarkupExtension
    {
        public BindingReferenceExtension()
        {
        }

        public BindingReferenceExtension(Binding binding)
        {
            // this constructor is getting called
            Binding = binding;
        }

        [ConstructorArgument(nameof(Binding))]
        public Binding Binding { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return $"Path={Binding.Path.Path}, Member={Binding.RelativeSource?.AncestorType}";
        }
    }
}