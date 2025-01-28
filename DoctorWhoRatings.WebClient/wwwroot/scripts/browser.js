var Browser = /** @class */ (function () {
    function Browser() {
    }
    Browser.openTab = function (url, target) {
        window.open(url, target);
    };
    Browser.closeCurrentTab = function () {
        window.close();
    };
    return Browser;
}());
//# sourceMappingURL=browser.js.map