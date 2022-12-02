using System;
using System.Windows.Markup;

namespace XamlSetMarkupExtensionExample.MarkupExtensions
{
    [XamlSetMarkupExtension(nameof(ReceiveMarkupExtension))]
    [MarkupExtensionReturnType(typeof(string))]
    public class StaticTextExtension : MarkupExtension
    {
        public StaticTextExtension()
        {
        }

        public StaticTextExtension(StaticExtension resourceKey)
        {
            // this constructor is never called
            ResourceKey = resourceKey;
        }

        [ConstructorArgument(nameof(ResourceKey))]
        public StaticExtension ResourceKey { get; set; }
        
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            // the ProvideValue method returns a value based on the ResourceName property (omitted here for brevity)
            return $"Key={ResourceKey.Member}, Dictionary={ResourceKey.MemberType}";
        }

        public static void ReceiveMarkupExtension(object targetObject, XamlSetMarkupExtensionEventArgs eventArgs)
        {
            if (eventArgs == null)
            {
                throw new ArgumentNullException(nameof(eventArgs));
            }

            var resourceKey = eventArgs.MarkupExtension as StaticExtension;
            if (resourceKey == null)
            {
                return;
            }

            var staticTextExtension = targetObject as StaticTextExtension;
            if (staticTextExtension == null)
            {
                return;
            }

            staticTextExtension.ResourceKey = resourceKey;
            eventArgs.Handled = true;
        }
    }
}