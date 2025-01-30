document.addEventListener('fullscreenchange', () => {
    Browser.trackFullScreen();
});

class Browser {
    public static openTab(url: string, target: string): void {
        window.open(url, target);
    }

    public static closeCurrentTab(): void {
        window.close();
    }

    public static isFullScreenSupported() {
        let element: any = document.getElementById('fullScreenZone');

        if (!element) return;

        return element.requestFullscreen;
    }

    public static enterFullScreen() {
        let element = document.getElementById('fullScreenZone');

        if (!element) return;

        if (element.requestFullscreen) {
            element.requestFullscreen();
        }
    }

    public static exitFullScreen() {
        if (document.exitFullscreen) {
            document.exitFullscreen();
        }
    }

    public static hasFullScreenZone(): any {
        if (!Browser.isFullScreenSupported()) return false;

        return document.getElementById('fullScreenZone') != null;
    }

    public static trackFullScreen() {
        if (document.fullscreenElement) {
            document.body.classList.add('full-screen-on');
        } else {
            document.body.classList.remove('full-screen-on');
        }
    }
}