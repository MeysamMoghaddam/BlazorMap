# BlazorMap

Using this Blazor library, you can select the desired location and get its address, longitude and latitude.
## Install
`
Install-Package Blazor.Map -Version x.x.x
`

or 
[Latest Version](https://www.nuget.org/packages/Blazor.Map/)

## Use
### Register Service In DI Container
```
builder.Services.AddBlazorMap();
```

### Use Component 
```
<Map Identifier="mymap" @bind-Lat="@Lat" @bind-Lng="@Lng" @bind-Address="@Address" ViewHeight="50" />
```
