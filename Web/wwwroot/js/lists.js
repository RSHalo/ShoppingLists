ready(function () {
    initialize();

    function initialize() {
        const container = document.querySelector(".list-index-container");
        if (container != null) {
            initializeCheckboxes();
        }
    };

    function initializeCheckboxes() {
        const checkboxes = document.querySelectorAll(".list-product-viewer .form-check-input");
        checkboxes.forEach((checkbox) => {
            checkbox.addEventListener("change", () => {
                console.log("changed!");
            });
        })
    }
});