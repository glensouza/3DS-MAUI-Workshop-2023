# Adding functionality to the "Check Splitter" app (Part 2)

Now we can finish up with the visual parts.

## Shell and Navigation

Let's start with a slight change to the app shell.

Shell is the default way of defining the page navigation in a .NET MAUI app. The default UI has a navigation menu. Since this app only has a single page, we don't need the navigation menu. There's no other page to navigate to. Let's open up `AppShell.xaml` and add a line to say `Shell.NavBarIsVisible="false"`. That one little switch turns off the navigation menu.

## UI

Now we can do the UI. Most of this will be in the XAML markup, but I will add some C# code as well. We open up `MainPage.xaml`.

We will be removing most of the markup because we're doing our own app here. We'll remove the reference to the `CounterBtn` in the code‑behind after we define the view.

I'm going to add the namespace for the community toolkit so that we can use it on this view.

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="CheckSplitter.MainPage"
             xmlns:toolkit="http://schemas.microsoft.com/dotnet/2022/maui/toolkit"
             xmlns:vm="clr-namespace:CheckSplitter.ViewModels">

    <ScrollView>
    </ScrollView>

</ContentPage>
```

Then, I add the namespace for the view model, and with that namespace, I can add a BindingContext for the view model. This will create an instance of the view model when the view is instantiated.

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="CheckSplitter.MainPage"
             xmlns:toolkit="http://schemas.microsoft.com/dotnet/2022/maui/toolkit"
             xmlns:vm="clr-namespace:CheckSplitter.ViewModels">

    <ContentPage.BindingContext>
        <vm:CheckViewModel />
    </ContentPage.BindingContext>

    <ScrollView>
    </ScrollView>

</ContentPage>
```

And I'll use the community toolkit to set the StatusBarColor.

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="CheckSplitter.MainPage"
             xmlns:toolkit="http://schemas.microsoft.com/dotnet/2022/maui/toolkit"
             xmlns:vm="clr-namespace:CheckSplitter.ViewModels">

    <ContentPage.BindingContext>
        <vm:CheckViewModel />
    </ContentPage.BindingContext>

    <ContentPage.Behaviors>
        <toolkit:StatusBarBehavior StatusBarColor="#8cd3ed" />
    </ContentPage.Behaviors>

    <ScrollView>
    </ScrollView>

</ContentPage>
```

I'm going to paste in some styles under ContentPage.Resources.

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="CheckSplitter.MainPage"
             xmlns:toolkit="http://schemas.microsoft.com/dotnet/2022/maui/toolkit"
             xmlns:vm="clr-namespace:CheckSplitter.ViewModels">

    <ContentPage.BindingContext>
        <vm:CheckViewModel />
    </ContentPage.BindingContext>

    <ContentPage.Behaviors>
        <toolkit:StatusBarBehavior StatusBarColor="#8cd3ed" />
    </ContentPage.Behaviors>

    <ContentPage.Resources>
        <Style x:Key="NumericEntry" TargetType="Entry">
            <Setter Property="PlaceholderColor" Value="#1a82a8" />
            <Setter Property="FontSize" Value="Medium" />
            <Setter Property="Keyboard" Value="Numeric" />
            <Setter Property="VerticalOptions" Value="Center" />
            <Setter Property="HorizontalTextAlignment" Value="End" />
        </Style>
        <Style x:Key="NumericPicker" TargetType="Picker">
            <Setter Property="FontSize" Value="Medium" />
            <Setter Property="VerticalOptions" Value="Center" />
            <Setter Property="HorizontalTextAlignment" Value="End" />
        </Style>
        <Style x:Key="NumericLabel" TargetType="Label">
            <Setter Property="VerticalOptions" Value="Center" />
            <Setter Property="FontSize" Value="Medium" />
            <Setter Property="TextColor" Value="Black" />
            <Setter Property="HorizontalOptions" Value="End" />
        </Style>
        <Style x:Key="NumericGrid" TargetType="Grid">
            <Setter Property="ColumnSpacing" Value="25" />
            <Setter Property="RowSpacing" Value="15" />
            <Setter Property="Padding" Value="0" />
            <Setter Property="ColumnDefinitions" Value="*,*" />
            <Setter Property="RowDefinitions" Value="*,*" />
        </Style>
    </ContentPage.Resources>

    <ScrollView>
    </ScrollView>

</ContentPage>
```

This allows me to set some common properties for the UI controls. We have a ScrollView, which allows the contents inside it to scroll. Then we have a VerticalStackLayout control. This allows us to add controls and it will place them from top to bottom. As part of the layout, I'm adding a custom background. It will be a linear gradient using two colors and going from top to bottom. Now I'm adding a label for the name of the app. We set the FontFamily to the custom font that we'd added to the project. Then we set the FontSize, the Text, and the TextColor. I add a Frame for the first set of controls. A frame is a container that makes it easy to group controls together. Next, we have a Grid defined for three rows, and we set the Style. We add the Amount label. The default cell on a grid is row 0, column 0. We only need to set these properties when the row or column is not 0. Then, the Amount Entry field. It gets Column=1. We set the Text field to bind it to the amount property of the ViewModel. Then I add the Label and Entry field for the tip percentage. Then I add a new Frame and Grid and add labels for the Total and the Tip. Their binding uses string format to format the values as currency. That will handle the currency symbol and how the decimal place is formatted for the current locale of the device. Now, we're going to add a control that will need to be initialized from the C# code for this view. We'll add another Frame and Grid, a Label for the number of people, and a Picker control. This control will display a list of choices when you tap it. I set the name of the control. This will allow us to reference it from the code‑behind. I add a SelectedIndexChanged event handler. This will be called when the user picks a value. And finally, a label for the split amount each person would pay.
Here is how the final state of the file looks like:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="CheckSplitter.MainPage"
             xmlns:toolkit="http://schemas.microsoft.com/dotnet/2022/maui/toolkit"
             xmlns:vm="clr-namespace:CheckSplitter.ViewModels">

    <ContentPage.BindingContext>
        <vm:CheckViewModel />
    </ContentPage.BindingContext>

    <ContentPage.Behaviors>
        <toolkit:StatusBarBehavior StatusBarColor="#8cd3ed" />
    </ContentPage.Behaviors>

    <ContentPage.Resources>
        <Style x:Key="NumericEntry" TargetType="Entry">
            <Setter Property="PlaceholderColor" Value="#1a82a8" />
            <Setter Property="FontSize" Value="Medium" />
            <Setter Property="Keyboard" Value="Numeric" />
            <Setter Property="VerticalOptions" Value="Center" />
            <Setter Property="HorizontalTextAlignment" Value="End" />
        </Style>
        <Style x:Key="NumericPicker" TargetType="Picker">
            <Setter Property="FontSize" Value="Medium" />
            <Setter Property="VerticalOptions" Value="Center" />
            <Setter Property="HorizontalTextAlignment" Value="End" />
        </Style>
        <Style x:Key="NumericLabel" TargetType="Label">
            <Setter Property="VerticalOptions" Value="Center" />
            <Setter Property="FontSize" Value="Medium" />
            <Setter Property="TextColor" Value="Black" />
            <Setter Property="HorizontalOptions" Value="End" />
        </Style>
        <Style x:Key="NumericGrid" TargetType="Grid">
            <Setter Property="ColumnSpacing" Value="25" />
            <Setter Property="RowSpacing" Value="15" />
            <Setter Property="Padding" Value="0" />
            <Setter Property="ColumnDefinitions" Value="*,*" />
            <Setter Property="RowDefinitions" Value="*,*" />
        </Style>
    </ContentPage.Resources>

    <ScrollView>
        <VerticalStackLayout Padding="20,0" Spacing="10">
            <VerticalStackLayout.Background>
                <LinearGradientBrush EndPoint="0,1">
                    <GradientStop Offset="0.1" Color="#8cd3ed" />
                    <GradientStop Offset="1.0" Color="#197ea3" />
                </LinearGradientBrush>
            </VerticalStackLayout.Background>
            <Label
                FontFamily="KastoreBold"
                FontSize="44"
                Text="CheckSplitter"
                TextColor="#197ea3" />
            <Frame BackgroundColor="#8888d2ec">
                <Grid RowDefinitions="*,*,*" Style="{StaticResource NumericGrid}">
                    <Label Style="{StaticResource NumericLabel}" Text="Amount" />
                    <Entry
                        Grid.Column="1"
                        Placeholder="Enter the check amount"
                        Style="{StaticResource NumericEntry}"
                        Text="{Binding Amount}" />
                    <Label
                        Grid.Row="1"
                        Style="{StaticResource NumericLabel}"
                        Text="Tip %" />
                    <Entry
                        Grid.Row="1"
                        Grid.Column="1"
                        Placeholder="Tip as percent"
                        Style="{StaticResource NumericEntry}"
                        Text="{Binding TipRate}" />
                </Grid>
            </Frame>

            <Frame BackgroundColor="#8888d2ec">
                <Grid Style="{StaticResource NumericGrid}">
                    <Label Style="{StaticResource NumericLabel}" Text="Tip" />
                    <Label
                        Grid.Column="1"
                        Style="{StaticResource NumericEntry}"
                        Text="{Binding Tip, StringFormat='{0:C}'}" />
                    <Label
                        Grid.Row="1"
                        Style="{StaticResource NumericLabel}"
                        Text="Total" />
                    <Label
                        Grid.Row="1"
                        Grid.Column="1"
                        Style="{StaticResource NumericEntry}"
                        Text="{Binding Total, StringFormat='{0:C}'}" />
                </Grid>
            </Frame>
            <Frame BackgroundColor="#8888d2ec" HasShadow="False">
                <Grid RowDefinitions="Auto,*" Style="{StaticResource NumericGrid}">
                    <Label Style="{StaticResource NumericLabel}" Text="# of people" />
                    <Picker
                        x:Name="NumberOfPeople"
                        Grid.Column="1"
                        SelectedIndexChanged="NumberOfPeople_SelectedIndexChanged"
                        Style="{StaticResource NumericPicker}" />
                    <Label
                        Grid.Row="1"
                        Style="{StaticResource NumericLabel}"
                        Text="Each pays" />
                    <Label
                        Grid.Row="1"
                        Grid.Column="1"
                        Style="{StaticResource NumericEntry}"
                        Text="{Binding PersonalAmount, StringFormat='{0:C}'}" />
                </Grid>
            </Frame>
        </VerticalStackLayout>
    </ScrollView>

</ContentPage>
```

## Code Behind

Now we jump to the code‑behind for this view. I remove the sample code. I add the ViewModels to the namespace.

```csharp
using CheckSplitter.ViewModels;

namespace CheckSplitter;

public partial class MainPage : ContentPage
{
  public MainPage()
  {
    InitializeComponent();
  }
}
```

Then I add a CheckViewModel property to the view. In the constructor, I assign the BindingContext of the view to our ViewModel property. Now I can populate the list for the picker. We have a loop that goes from 1 to 15. I'll add the loop index as a string to the picker's items collection. This is a quick way to give us a list from 1 to 15. Then we set the picker's SelectedIndex to 0. That will set it to the first value in the list when the app starts up.

Our final step is to define the SelectedItemChanged event for the picker. We cast a sender argument to the local Picker variable. If the selectedIndex is not ‑1, it means we have a selection. We set the vm.NumPeople to the index +1 because the index starts at 0 and the items in the list start at 1. But updating the NumPeople property of the view model, the personal amount property will be updated.

```csharp
using CheckSplitter.ViewModels;

namespace CheckSplitter;

public partial class MainPage : ContentPage
{
    readonly CheckViewModel vm;

    public MainPage()
    {
        InitializeComponent();

        vm = (CheckViewModel)BindingContext;

        for (int i = 1; i < 16; i++)
        {
            NumberOfPeople.Items.Add(i.ToString());
        }

        NumberOfPeople.SelectedIndex = 0;
    }

    private void NumberOfPeople_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;

        if (selectedIndex != -1)
        {
            vm.NumPeople = selectedIndex + 1;
        }
    }
}
```

Now we can run the app. You can see that the label for the name of the app is using the custom font. I'll type in an amount for the check. As I type the numbers, you can see the total and a split amount update at the same time. The MVVM design pattern makes it easy to use data binding to update the UI as the data changes. I'll set the tip percentage, and you can see the calculated field update on the screen. I'll tap on the number of people. We get the scrolling list for how many people to split to check with. I'll pick 3, and you can see the total split 3 ways. I'll tap it again and change the number to 5. The split amount changes again.
