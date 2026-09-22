window.addEventListener('focus', () => {
    KeyboardEvents.track();
});
document.addEventListener('keydown', (event) => {
    KeyboardEvents.setKeyState(event);
});
document.addEventListener('keyup', (event) => {
    KeyboardEvents.setKeyState(event);
});
class KeyboardEvents {
    static isShiftKeyDown() {
        return KeyboardEvents.shiftKeyState;
    }
    static setKeyState(event) {
        KeyboardEvents.setTrackingState(event.shiftKey);
    }
    static track() {
        KeyboardEvents.tracking = true;
        KeyboardEvents.setTrackingState(false);
    }
    static untrack() {
        KeyboardEvents.tracking = false;
        KeyboardEvents.setTrackingState(false);
    }
    static setTrackingState(on) {
        KeyboardEvents.shiftKeyState = on;
        if (on) {
            document.body.classList.add('shift-on');
        }
        else {
            document.body.classList.remove('shift-on');
        }
    }
}
KeyboardEvents.tracking = true;
window.KeyboardEvents = KeyboardEvents;
export {};
//# sourceMappingURL=keyboard-events.js.map