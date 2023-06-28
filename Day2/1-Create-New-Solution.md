# Create New Solution

We will be creating a solution with re-usable code in different projects that will run on multiple platforms. We will be using Visual Studio 2022 for this workshop. You can download it from [here](https://visualstudio.microsoft.com/downloads/).

## Class Library for shared code

1. Right-click on the Solution
1. Select **Add > New Project**
1. Select **Class Library** from the list of templates
1. Name the project **Shared**
1. Make sure the Framework is set to .NET 7.0
1. Click **Create**
1. Right-click on the **TestHarness** project
1. Select **Add > Project Reference**
1. Select the **Shared** project
1. Click **OK**

## Web project for easy visualization

1. Right-click on the Solution
1. Select **Add > New Project**
1. Select **Blazor WebAssembly App** from the list of templates
1. Name the project **Web**
1. Make sure the Framework is set to .NET 7.0
1. Make sure the checkboxes:
    - "*Configure for HTTPS*" is ***checked***
    - "*ASP.NET Core hosted*" is ***unchecked***
    - "*Progressive Web Application*" is ***checked***
    - "*Do not use top-level statements*" is ***unchecked***
1. Click **Create**
1. Right-click on the **Web** project
1. Select **Add > Project Reference**
1. Select the **Shared** project
1. Click **OK**

## Add .NET MAUI Blazor App

1. Right-click on the Solution
1. Select **Add > New Project**
1. Select **.NET MAUI Blazor App** from the list of templates
1. Name the project **MauiBlazor**
1. Make sure the Framework is set to .NET 7.0
1. Click **Create**
1. Right-click on the **MauiBlazor** project
1. Select **Add > Project Reference**
1. Select the **Shared** project
1. Click **OK**

## Add shared component project

1. Right-click on the Solution
1. Select **Add > New Project**
1. Select **Razor Class Library** from the list of templates
1. Name the project **Components**
1. Make sure the Framework is set to .NET 7.0
1. Leave the "*Support pages and views*" checkbox ***unchecked***
1. Click **Create**
1. Right-click on the **Web** project
1. Select **Add > Project Reference**
1. Select the **Components** project
1. Click **OK**
1. Right-click on the **MauiBlazor** project
1. Select **Add > Project Reference**
1. Select the **Components** project
1. Click **OK**

You should have 4 projects in your solution.

[Next: Set up and test the Shared project](2-Shared-Project.md)
