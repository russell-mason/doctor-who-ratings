class Browser {
    public static openTab(url: string, target: string): void {
        window.open(url, target);
    }

    public static closeCurrentTab(): void {
        window.close();
    }
}