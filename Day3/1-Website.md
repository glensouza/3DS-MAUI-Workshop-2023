# Let's visualize on website

## Models for views

1. In the **Shared** project, right-click the **Models** folder and select **Add > Class**.
1. Name the file **VoteModel** and click **Add**.
1. Replace the contents of the file with the following code:

```csharp
namespace Shared.Models;

public class VoteModel
{
    public string PicId1 { get; set; } = string.Empty;
    public string Name1 { get; set; } = string.Empty;
    public string PictureUri1 { get; set; } = string.Empty;
    public string PictureSmallUri1 { get; set; } = string.Empty;
    public string PicId2 { get; set; } = string.Empty;
    public string Name2 { get; set; } = string.Empty;
    public string PictureUri2 { get; set; } = string.Empty;
    public string PictureSmallUri2 { get; set; } = string.Empty;
}
```

1. Right-click the **Models** folder and select **Add > Class**.
1. Name the file **PictureModel** and click **Add**.
1. Replace the contents of the file with the following code:

```csharp
namespace Shared.Models;

public class PictureModel
{
    public string PicId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string PictureUri { get; set; } = string.Empty;
    public double Rating { get; set; } = 1200;
    public DateTime LastUpdated { get; set; }
}
```

## Views

1. In the **Components** project, delete the **Component1.razor** and **ExampleJsInterop.cs** files.
1. Right-click the **Components** project and select **Add > New Item**.
1. Name the file **PictureDisplay.razor** and click **Add**.
1. Name the file **PicturesDisplay.razor** and click **Add**.

1. Open the **Program.cs** file in the **Web** project and add the following code after the **builder.Services.AddScoped...** line:

    ```csharp
    const string connectionString = "";
    const string partitionKey = Constants.CarTablePartitionKey;
    builder.Services.AddSingleton(s => new PictureTable(connectionString, partitionKey));
    builder.Services.AddSingleton(s => new VoteTable(connectionString,  partitionKey));
    ```

1. Open the **_Imports.razor** file and add the following code to the end of the file:

    ```csharp
    @using global::Shared.Models
    @using global::Shared.Services
    @using global::Shared.Interfaces
    @using global::Shared.Utilities
    ```

1. In the **Web** project, open the **Pages** folder and remove both **Counter.razor** and **FetchData.razor**.
1. Open the **Index.razor** file and replace the contents with the following code:

```html

```