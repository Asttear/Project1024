using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Project1024.Wasm.Components.Swiper;

public partial class SwiperContainer : IAsyncDisposable
{
    private ElementReference _swiperContainer;
    private IJSObjectReference? _swiper;

    [Inject]
    private IJSRuntime JS { get; set; } = null!;

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public string Class { get; set; } = "";

    [Parameter]
    public string Style { get; set; } = "";

    [Parameter]
    public string Direction { get; set; } = "vertical";

    [Parameter]
    public bool MouseWheel { get; set; } = true;

    [Parameter]
    public bool Keyboard { get; set; } = true;

    [Parameter]
    public string FocusableElements { get; set; } = "button";

    [Parameter]
    public string NoSwipingSelector { get; set; } = ".m-btn";

    [Parameter]
    public bool Loop { get; set; } = true;

    public async Task SlidePrevAsync()
    {
        if (_swiper is not null)
            await _swiper.InvokeVoidAsync("slidePrev");
    }

    public async Task SlideNextAsync()
    {
        if (_swiper is not null)
            await _swiper.InvokeVoidAsync("slideNext");
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore().ConfigureAwait(false);
        GC.SuppressFinalize(this);
    }

    protected virtual async ValueTask DisposeAsyncCore()
    {
        if (_swiper is not null)
        {
            await _swiper.DisposeAsync().ConfigureAwait(false);
        }
        _swiper = null;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await using var module = await JS.InvokeAsync<IJSObjectReference>("import", "./Components/Swiper/SwiperContainer.razor.js");
        _swiper = await module.InvokeAsync<IJSObjectReference>("getSwiper", _swiperContainer);
    }
}
