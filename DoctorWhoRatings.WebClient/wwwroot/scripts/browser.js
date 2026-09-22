document.addEventListener('fullscreenchange', () => {
    Browser.trackFullScreen();
});
class Browser {
    static openTab(url, target) {
        window.open(url, target);
    }
    static closeCurrentTab() {
        window.close();
    }
    static isFullScreenSupported() {
        const element = document.getElementById('fullScreenZone');
        if (!element)
            return;
        return element.requestFullscreen;
    }
    static enterFullScreen() {
        const element = document.getElementById('fullScreenZone');
        if (!element)
            return;
        if (element.requestFullscreen) {
            element.requestFullscreen();
        }
    }
    static exitFullScreen() {
        if (document.exitFullscreen) {
            document.exitFullscreen();
        }
    }
    static hasFullScreenZone() {
        if (!Browser.isFullScreenSupported())
            return false;
        return document.getElementById('fullScreenZone') != null;
    }
    static trackFullScreen() {
        if (document.fullscreenElement) {
            document.body.classList.add('full-screen-on');
        }
        else {
            document.body.classList.remove('full-screen-on');
        }
    }
}
window.Browser = Browser;
export {};
//# sourceMappingURL=browser.js.map