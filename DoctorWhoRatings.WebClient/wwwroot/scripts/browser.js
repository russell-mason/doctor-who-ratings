document.addEventListener('fullscreenchange', function () {
    Browser.trackFullScreen();
});
var Browser = /** @class */ (function () {
    function Browser() {
    }
    Browser.openTab = function (url, target) {
        window.open(url, target);
    };
    Browser.closeCurrentTab = function () {
        window.close();
    };
    Browser.isFullScreenSupported = function () {
        var element = document.getElementById('fullScreenZone');
        if (!element)
            return;
        return element.requestFullscreen;
    };
    Browser.enterFullScreen = function () {
        var element = document.getElementById('fullScreenZone');
        if (!element)
            return;
        if (element.requestFullscreen) {
            element.requestFullscreen();
        }
    };
    Browser.exitFullScreen = function () {
        if (document.exitFullscreen) {
            document.exitFullscreen();
        }
    };
    Browser.hasFullScreenZone = function () {
        if (!Browser.isFullScreenSupported())
            return false;
        return document.getElementById('fullScreenZone') != null;
    };
    Browser.trackFullScreen = function () {
        if (document.fullscreenElement) {
            document.body.classList.add('full-screen-on');
        }
        else {
            document.body.classList.remove('full-screen-on');
        }
    };
    return Browser;
}());
//# sourceMappingURL=browser.js.map