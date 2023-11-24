using Microsoft.AspNetCore.Components;

namespace ECommerceWeb.Client.Shared;

public partial class LoadingComponent
{
    [Parameter]
    public bool IsLoading { get; set; }
}