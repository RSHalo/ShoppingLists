// Disables and enables elements based on the value of other elements.
ready(function () {
    const inputEventType = "input";
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