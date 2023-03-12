// Disables and enables elements based on the value of other elements.
// Usage: Place data-required-for="<selector>" on an input element such that the selected element will be disabled when the input
// element has no value.
const inputEventType = "input";
ready(function () {
    const requiredElements = document.querySelectorAll("[data-required-for]");

    requiredElements.forEach((requiredElement) => {
        const selector = requiredElement.dataset.requiredFor;
        const targets = document.querySelectorAll(selector);

        requiredElement.addEventListener(inputEventType, () => ensureEnabledStatus(requiredElement, targets));

        // Dispatch an input event immediately to ensure the targets have the correct state upon page load.
        requiredElement.dispatchEvent(new Event(inputEventType));
    });

    function ensureEnabledStatus(requiredElement, targets) {
        targets.forEach((target) => {
            if (requiredElement.value.trim().length === 0) {
                target.setAttribute("disabled", "");
            } else {
                target.removeAttribute("disabled");
            }
        });
    };
});

function clearRequiredElement(element, triggerInputEvent = false) {
    element.value = "";

    if (triggerInputEvent) {
        element.dispatchEvent(new Event(inputEventType));
    }
}