let helper;

export function init(el, dotnetHelper) {
    helper = dotnetHelper;
    el.addEventListener("swiperrealindexchange", onRealIndexChange);
    return el.swiper;
}

export function destroy() {
    el.removeEventListener("swiperrealindexchange", onRealIndexChange);
}

async function onRealIndexChange(e) {
    if (true) {
        await helper.invokeMethodAsync("OnIndexChanged", e.detail[0].realIndex);
    }
}