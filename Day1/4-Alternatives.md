# Alternatives to `.NET MAUI`

When it comes to using cross‑platform frameworks, `.NET MAUI` is not your only option. Let's take a look at some of the other options and compare them at a high level with `.NET MAUI`.

## Skia

![Skia](https://api.skia.org/logo.png)

Skia is an open-source 2D graphics library. Serves as the graphics engine for Google Chrome, ChromeOS, Android, Flutter, and many other products. [Source: skia.org](skia.org)

It doesn't come with any controls, you would use Skia to create the controls from graphics primitives.

You can even use Skia from .NET. It's a handy library for creating custom controls and will use hardware acceleration if it's available on the device.

## Flutter

![Flutter](https://storage.googleapis.com/cms-storage-bucket/ec64036b4eacc9f3fd73.svg)

[Flutter](https://flutter.dev) is a framework from Google. You write the UI as code, it doesn't use a markup language like `.NET MAUI`, it uses Dart as the programming language.

At design time, Dart is interpreted and it's compiled at runtime. All the controls are defined as Dart controls, they don't use the native controls.

The controls are created using Skia, while `.NET MAUI` uses the native platform controls.

Flutter|`.NET MAUI`
|--|--|
Based on Dart|Built with XAML & C#
Fast performance|Faster Performance
All controls rendered with Skia|Wrappers for native controls
Platform integration requires different languages|Can use C# for platform code
Relatively new|Relatively new

If you want to access platform‑specific features in Flutter, you'll need to install or write a plugin. That plugin will need to be written using the native code toolkits for that platform. That means knowing Objective‑C or Swift for iOS and Kotlin or Java for Android. With `.NET MAUI`, you can access the native functionality directly from the C# code. Both platforms are relatively new, but `.NET MAUI` was based on Xamarin and inherits that history.

## Uno Platform

![Uno Platform](https://uno-website-assets.s3.amazonaws.com/wp-content/uploads/2021/03/24151905/uno-logo-tm.svg)

[Uno Platform](https://platform.uno) is an open‑source framework that is very similar to `.NET MAUI` in terms of designing goals. Both are built with XAML and C#.

Because they're compiled, they both have good runtime performance.

Uno controls are rendered with Skia. Out of the box, Uno supports the MVVM and the MVU design patterns. MVVM is Model‑View‑ViewModel, and we'll cover that later in the workshop. MVU is Model‑View‑Update, and is a way of doing data binding where all the UI is handled in the code instead of using XAML views. `.NET MAUI` supports both MVVM and MVU, and also adds Blazor Hybrid and RxUI.

Uno apps are limited to a single window or form. `.NET MAUI` supports multiple windows, which is handy for desktop apps and with Android devices with multiple screens.

Uno Platform|`.NET MAUI`
|--|--|
Built with XAML & C#|Built with XAML & C#
Fast performance|Fast Performance
Controls drawn with Skia|Native controls
MVVM, MVU|MVVM, MVU, Blazor, RxUI
Single window|Multiple Windows
Relatively new|Relatively new

## Avalonia UI

Avalonia is another open‑source framework for doing cross‑pull formats with .NET.

As with `.NET MAUI`, you build your apps with a mixture of XAML markup and C# code.

The performance is good because it's compiled, the controls are drawn with Skia instead of mapping to the native controls.

Avalonia UI started out as a desktop development framework and mobile was added to it later on. With its roots in Xamarin.Forms, `.NET MAUI` was mobile‑first in design with desktop added to it.

Avalonia UI has been around for a while and is a mature product.

Avalonia UI|`.NET MAUI`
|--|--|
Built with XAML & C#|Built with XAML & C#
Fast performance|Fast Performance
Controls rendered via Skia|Native controls
Desktop focused|Mobile focused
Mature Product|Relatively new

## `.NET MAUI` is Microsoft’s framework for mobile

One thing to remember with the alternative frameworks is you need to know how much traction that product has and how much support it will get. While `.NET MAUI` is open source, it is Microsoft's framework for mobile and is fully backed by Microsoft for support and new functionality.

Each year, Apple and Google release major updates to their platforms and often have breaking changes to their SDKs, Microsoft‑tested resources in place to ensure timely updates to `.NET MAUI` to support those changes.

## Other frameworks worth mentioning

There are other frameworks that are worth mentioning, but we won't go into detail on them.

### NativeScript UI

![NativeScript UI](https://th.bing.com/th?id=OSK.50b893d9d65a0434cea394a3eaf66006&w=148&h=148&c=7&o=6&dpr=1.5&pid=SANGAM)

[NativeScript UI](https://www.progress.com/nativescript) is a set of UI components that can be used to build native mobile apps using the NativeScript framework. It is a collection of pre-built, high-quality UI components that can be used to quickly and easily build responsive user interfaces. It is based on the NativeScript framework, which is a cross-platform framework that allows developers to build native mobile apps using JavaScript. NativeScript UI components are rendered using native controls on each platform, which ensures that they look and feel like native apps.

### Ionic Framework

![Ionic Framework](https://th.bing.com/th?id=OSK.0987536dd9d5d5c58c88ef875a82b531&w=188&h=132&c=7&o=6&dpr=1.5&pid=SANGAM)

[Ionic Framework](https://ionicframework.com/) is an open-source UI toolkit for building performant, high-quality mobile apps, desktop apps, and progressive web apps using web technologies such as HTML, CSS, and JavaScript. It is based on Angular, but it can also be used with other JavaScript frameworks such as React and Vue.

### React Native
![React Native](https://th.bing.com/th?id=OSK.0f748c6f3d8bc1a6ec9bcac171056e98&w=188&h=132&c=7&o=6&dpr=1.5&pid=SANGAM)

[React Native](https://reactnative.dev) is a framework from Facebook based on React, which is a popular JavaScript library for building user interfaces. React Native uses React to create a virtual DOM that is then rendered to native UI components on each platform. This allows React Native apps to look and feel like native apps, while still being written in JavaScript.

### Summary

| Feature | NativeScript UI | Ionic Framework | React Native |
|---|---|---|---|
| Technology | JavaScript | HTML, CSS, JavaScript | JavaScript |
| Framework | NativeScript | AngularJS, React, Vue | React |
| Platforms | iOS, Android, web | iOS, Android, desktop, web | iOS, Android, web |

[Next: 5-Requirements.md](5-Requirements.md)
