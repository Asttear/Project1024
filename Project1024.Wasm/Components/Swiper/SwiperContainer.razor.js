let element, helper;

export function init(el, dotnetHelper) {
    helper = dotnetHelper;
    element = el;
    element.addEventListener("swiperrealindexchange", onRealIndexChange);
    return element.swiper;
}

export function dispose() {
    element.removeEventListener("swiperrealindexchange", onRealIndexChange);
}

async function onRealIndexChange(e) {
    await helper.invokeMethodAsync("OnIndexChanged", e.detail[0].realIndex);
}