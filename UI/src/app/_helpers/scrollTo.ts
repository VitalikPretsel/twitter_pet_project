export function scrollTo(selector: string): void {
    let element = document.querySelector(selector);
    if (element) {
        element.scrollIntoView();
    }
}