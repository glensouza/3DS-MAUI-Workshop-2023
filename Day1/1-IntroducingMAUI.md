# Introducing `.NET MAUI`

`.NET MAUI` is Microsoft's development framework for that lets you create mobile and desktop apps from a common shared code base. We'll be focusing mainly on Windows, but you can also create Android, iOS, and macOS desktop apps at the same time and from the same code.

Most `.NET MAUI` developers are using it for Android and iOS apps. While you can use it for desktop apps, the framework is mobile first. It may not be the best choice for a desktop app.

This workshop is a *"big picture"* view of `.NET MAUI`, not an in‑depth workshop. We'll cover what `.NET MAUI` is, where it came from, and what it can do for you. We'll go over what you need to get started with doing `.NET MAUI` development. We'll take a *big picture* look at what makes up a `.NET MAUI` app and build a sample application. This workshop is intended to help you decide if `.NET MAUI` is the right tool for you and your team and to show you how it works.

Being that your shop is still new to C#, but you intend to move that direction, this workshop will also give you a good idea of what you can expect when you start doing C# development.

To leverage the power of `.NET MAUI`, you'll want to know the MVVM pattern and how to work with XAML. A quick overview of both MVVM and XAML will be covered in this workshop and you'll see them used in the demo.

## Pre-Requisites

- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/preview/vs2022/)
  - I use the Enterprise edition of Visual Studio, you can use any edition of Visual Studio 2022 to work with `.NET MAUI`. This includes the free Community edition.
- [.NET 7](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
  - If you have the latest version of Visual Studio installed, then you might already have this installed.
- [.NET MAUI 7](https://dotnet.microsoft.com/en-us/apps/maui)
  - The material that this workshop covers is applicable to `.NET MAUI` version 6 and 7. The material will be applicable to both versions, but we'll be using `.NET MAUI` 7.

The `.NET MAUI` workload for Visual Studio will need to be included when you install Visual Studio 2022. If you've already installed Visual Studio and didn't include it, you can just run the installer again and add that workload. If you're building Windows desktop apps with `.NET MAUI`, you'll need Visual Studio Windows. Creating `.NET MAUI` Windows apps on macOS is not supported.

If you're building iOS or macOS apps, you'll need access to a macOS device for building and debugging apps. The requirement to build apps with a Mac is an Apple legal requirement. It's not a technical limitation of Visual Studio. If you do not have access to a Mac, you'll still be able to build Android and Windows apps. For Android, you'll need the Android SDK version 23 or newer installed. For iOS, you'll need Xcode 14 or newer. Xcode 14 requires macOS Monterey 12.4 or newer. You'll also need an Apple Developer account to build iOS apps.

[Next: History of .NET MAUI](2-History.md)
