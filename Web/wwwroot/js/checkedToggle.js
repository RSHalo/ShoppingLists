ready(function () {
    const containers = document.querySelectorAll(".toggle-checkbox-container");

    containers.forEach((container) => {
        const checkbox = container.querySelector(".toggle-checkbox");
        const targets = container.querySelectorAll(".toggle-checkbox-target");
        if (checkbox == null || targets.length === 0) {
            return;
        }

        function toggleTargets() {
            targets.forEach((target) => {
                let hideOnChecked = target.classList.contains("hide-on-checked");
                if (checkbox.checked) {
                    target.classList.toggle("d-none", hideOnChecked);
                } else {
                    target.classList.toggle("d-none", hideOnChecked === false);
                }
            });
        }

        checkbox.addEventListener("change", toggleTargets);
    });
});