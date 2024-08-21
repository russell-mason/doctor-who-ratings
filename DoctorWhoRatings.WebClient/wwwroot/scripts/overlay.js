var Overlay = /** @class */ (function () {
    function Overlay() {
    }
    Overlay.pollForContent = function (overlayId, pollSelector) {
        var intervalInMilliseconds = 10;
        var timeoutInMilliseconds = 10000;
        var intervalId = setInterval(function () {
            try {
                var hostElement = document.querySelector(pollSelector);
                if (hostElement && hostElement.innerHTML.trim() !== "") {
                    document.getElementById(overlayId).style.display = 'none';
                    Overlay.reset(intervalId, timeoutId);
                }
            }
            catch (error) {
                console.error('Error occurred when polling for overlay content:', error);
                Overlay.reset(intervalId, timeoutId);
            }
        }, intervalInMilliseconds);
        var timeoutId = setTimeout(function () {
            console.warn('Timeout occurred when polling for overlay content');
            Overlay.reset(intervalId, timeoutId);
        }, timeoutInMilliseconds);
    };
    Overlay.reset = function (intervalId, timeoutId) {
        clearInterval(intervalId);
        clearTimeout(timeoutId);
    };
    return Overlay;
}());
//# sourceMappingURL=overlay.js.map