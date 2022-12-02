using System;
using System.Windows.Markup;

namespace XamlSetMarkupExtensionExample.MarkupExtensions
{
    [XamlSetMarkupExtension(nameof(ReceiveMarkupExtension))]
    [MarkupExtensionReturnType(typeof(string))]
    public class LocalizedTextExtension : MarkupExtension
    {
        public LocalizedTextExtension()
        {
        }

        public LocalizedTextExtension(string resourceName)
        {
            ResourceName = resourceName;
        }

        public LocalizedTextExtension(StaticExtension resourceKey)
        {
            // this constructor is never called
            ResourceName = resourceKey.Member;
            ResourceDictionary = resourceKey.MemberType;
        }

        [ConstructorArgument(nameof(ResourceName))]
        public string ResourceName { get; set; }
        
        public Type ResourceDictionary { get; set; }

        /// <summary>
        /// This property is only provided to show how the behavior changes, before and after refreshing the designer (as we're modifying this otherwise unused property)
        /// </summary>
        public string SomeOtherOption { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            // the ProvideValue method returns a value based on the ResourceName property (omitted here for brevity)
            return $"Key={ResourceName}, Dictionary={ResourceDictionary}";
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

            var languageBindingExtension = targetObject as LocalizedTextExtension;
            if (languageBindingExtension == null)
            {
                return;
            }

            languageBindingExtension.ResourceName = resourceKey.Member;
            languageBindingExtension.ResourceDictionary = resourceKey.MemberType;
            eventArgs.Handled = true;
        }
    }
}