ready(function () {
    let listItemViewer;

    initialize();

    function initialize() {
        const container = document.querySelector(".list-index-container");
        if (container != null) {
            listItemViewer = document.querySelector(".list-item-viewer");

            initializeCheckboxes();
        }
    };

    function initializeCheckboxes() {
        const checkboxes = document.querySelectorAll(".list-product-viewer .form-check-input");
        checkboxes.forEach((checkbox) => {
            checkbox.addEventListener("change", () => {
                console.log("changed!");
                Reloader.reload(listItemViewer);
            });
        })
    }
});