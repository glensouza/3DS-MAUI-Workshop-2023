using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Shared.Data;
using Shared.Interfaces;
using Shared.Models;
using Web;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

const string connectionString = "DefaultEndpointsProtocol=https;AccountName=3dsdemo20230628;AccountKey=800QuK4RXFmVbYZKf9BMmasMQvaOiOQDB55wSB8ryc8guaP7EpXKP2oVSK1lEOO9/vcJr9Wvx8e++AStEqY9pw==;EndpointSuffix=core.windows.net";
string partitionKey = Constants.CarTablePartitionKey.ToLower();
builder.Services.AddScoped<IData<PictureEntity>, PictureTable>(s => new PictureTable(connectionString, partitionKey));
builder.Services.AddScoped<IData<VoteEntity>, VoteTable>(s => new VoteTable(connectionString, partitionKey));

await builder.Build().RunAsync();
