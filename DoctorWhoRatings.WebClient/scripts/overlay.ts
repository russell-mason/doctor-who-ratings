class Overlay {
    public static pollForContent(overlayId: string, pollSelector: string): void {
        const intervalInMilliseconds = 10;
        const timeoutInMilliseconds = 10000;

        const intervalId = setInterval(() => {
            try {
                const hostElement = document.querySelector(pollSelector);

                if (hostElement && hostElement.innerHTML.trim() !== "") {
                    document.getElementById(overlayId).style.display = 'none';

                    Overlay.reset(intervalId, timeoutId);
                }
            } catch (error) {
                console.error('Error occurred when polling for overlay content:', error);

                Overlay.reset(intervalId, timeoutId);
            }
        }, intervalInMilliseconds);

        const timeoutId = setTimeout(() => {
            console.warn('Timeout occurred when polling for overlay content');

            Overlay.reset(intervalId, timeoutId);
        }, timeoutInMilliseconds);
    }

    private static reset(intervalId, timeoutId) {
        clearInterval(intervalId);
        clearTimeout(timeoutId);
    }
}