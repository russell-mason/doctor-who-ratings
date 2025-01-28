window.addEventListener('focus', function () {
    KeyboardEvents.track();
});
document.addEventListener('keydown', function (event) {
    KeyboardEvents.setKeyState(event);
});
document.addEventListener('keyup', function (event) {
    KeyboardEvents.setKeyState(event);
});
var KeyboardEvents = /** @class */ (function () {
    function KeyboardEvents() {
    }
    KeyboardEvents.isShiftKeyDown = function () {
        return KeyboardEvents.shiftKeyState;
    };
    KeyboardEvents.setKeyState = function (event) {
        KeyboardEvents.setTrackingState(event.shiftKey);
    };
    KeyboardEvents.track = function () {
        KeyboardEvents.tracking = true;
        KeyboardEvents.setTrackingState(false);
    };
    KeyboardEvents.untrack = function () {
        KeyboardEvents.tracking = false;
        KeyboardEvents.setTrackingState(false);
    };
    KeyboardEvents.setTrackingState = function (on) {
        KeyboardEvents.shiftKeyState = on;
        if (on) {
            document.body.classList.add('shift-on');
        }
        else {
            document.body.classList.remove('shift-on');
        }
    };
    KeyboardEvents.tracking = true;
    return KeyboardEvents;
}());
//# sourceMappingURL=keyboard-events.js.map