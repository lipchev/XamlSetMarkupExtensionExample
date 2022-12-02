# XamlSetMarkupExtensionExample
Given the following XAML:

        <GroupBox Header="1. This should display the value 'Example'">
            <TextBlock Text="{x:Static properties:Resources.ExampleKey}"/>
        </GroupBox>

        <GroupBox Header="2. This should display the key 'ExampleKey'">
            <TextBlock Text="{local:LocalizedText ResourceName={x:Static properties:Resources.ExampleKey}, SomeOtherOption=changing-this-causes-the-resource-name-to-be-resolved}"/>
        </GroupBox>

        <GroupBox Header="3. This should display the key 'ExampleKey'">
            <TextBlock Text="{local:LocalizedText {x:Static properties:Resources.ExampleKey}}"/>
        </GroupBox>

        <GroupBox Header="4. This should display 'Header'">
            <TextBlock Text="{local:BindingPath BindingPath={Binding RelativeSource={RelativeSource AncestorType=GroupBox}, Path=Header}}"/>
        </GroupBox>
        
        <GroupBox Header="5. This should display 'ExampleKey'">
            <TextBlock Text="{local:BindingPath BindingPath={Binding RelativeSource={RelativeSource AncestorType=properties:Resources}, Path=ExampleKey}}"/>
        </GroupBox>

        <GroupBox Header="6. This should display 'ExampleKey'">
            <TextBlock Text="{local:BindingPath {Binding RelativeSource={RelativeSource AncestorType=properties:Resources}, Path=ExampleKey}}"/>
        </GroupBox>

        <GroupBox Header="7. This should display 'CustomPath'">
            <TextBlock Text="{local:BindingPath BindingPath=CustomPath, MemberType=TextBlock}"/>
        </GroupBox>

        <GroupBox Header="8. This should display 'Header'">
            <TextBlock Text="{local:BindingReference Binding={Binding RelativeSource={RelativeSource AncestorType=GroupBox}, Path=Header}}"/>
        </GroupBox>
        
        <GroupBox Header="9. This should display 'ExampleKey'">
            <TextBlock Text="{local:BindingReference Binding={Binding RelativeSource={RelativeSource AncestorType=properties:Resources}, Path=ExampleKey}}"/>
        </GroupBox>

        <GroupBox Header="10. This should display 'ExampleKey'">
            <TextBlock Text="{local:BindingReference {Binding RelativeSource={RelativeSource AncestorType=properties:Resources}, Path=ExampleKey}}"/>
        </GroupBox>
        
Here is what we get in the designer (the left side matches the runtime behavior):
![image](https://user-images.githubusercontent.com/5253184/205306108-d9d11854-f749-48d6-9652-1ea184be5f1e.png)

As we start modifying the `SomeOtherOption` on example N.2 the {x:Static} reference is resolved to the actual value (again- not calling our ReceiveMarkupExtension)
![image](https://user-images.githubusercontent.com/5253184/205307748-ec4ab58b-0ffc-4b85-b4c0-026bf6b14ea8.png)

Refreshing the designer brings us back to square one.
