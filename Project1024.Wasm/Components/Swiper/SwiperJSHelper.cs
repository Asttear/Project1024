using Microsoft.JSInterop;

namespace Project1024.Wasm.Components.Swiper;

public class SwiperJSHelper
{
    private readonly SwiperContainer _swiper;

    public SwiperJSHelper(SwiperContainer swiper)
    {
        _swiper = swiper;
    }

    [JSInvokable]
    public async Task OnIndexChanged(int index)
    {
        await _swiper.IndexChanged.InvokeAsync(index);
    }
}
