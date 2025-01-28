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
    private static tracking: boolean = true;
    private static shiftKeyState: boolean;

    public static isShiftKeyDown(): boolean {
        return KeyboardEvents.shiftKeyState;
    }

    public static setKeyState(event: KeyboardEvent): void {
        KeyboardEvents.setTrackingState(event.shiftKey);
    }

    public static track(): void {
        KeyboardEvents.tracking = true;
        KeyboardEvents.setTrackingState(false);
    }

    public static untrack(): void {
        KeyboardEvents.tracking = false;
        KeyboardEvents.setTrackingState(false);
    }

    public static setTrackingState(on: boolean): void {
        KeyboardEvents.shiftKeyState = on;

        if (on) {
            document.body.classList.add('shift-on');
        } else {
            document.body.classList.remove('shift-on');
        }
    }
}