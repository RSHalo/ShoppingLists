ready(function () {
    let listProductViewer;
    let clearSearchButton;
    let searchInput;
    let listPreviewButton;
    let listPreviewContainer;

    initialize();

    function initialize() {
        const container = document.querySelector(".list-index-container");
        if (container != null) {
            listProductViewer = document.querySelector(".list-product-viewer");
            clearSearchButton = listProductViewer.querySelector("#clearProductSearchButton");
            searchInput = listProductViewer.querySelector("#productSearchInput");
            listPreviewButton = document.querySelector(".list-preview-button");
            listPreviewContainer = document.querySelector(".list-preview");

            initializePreview();
            initializeSearch();
            initializeCheckboxes();
            initializeProductAddedHandler();
        }
    };

    function initializePreview() {
        listPreviewButton.addEventListener("click", () => {
            listPreviewButton.classList.toggle("previewing")
            listPreviewContainer.classList.toggle("d-none");
        });
    }

    function initializeSearch() {
        searchInput.addEventListener("input", () => {
            // Show the products that contain the search term, by checking their data-upperName values.
            const searchTerm = searchInput.value.trim().toUpperCase();
            const allCheckboxes = listProductViewer.querySelectorAll(".checkbox-container");
            if (searchTerm.length > 0) {
                allCheckboxes.forEach(checkbox => {
                    const upperName = checkbox.dataset.upperName;
                    ElementHelper.toggle(checkbox, () => upperName.includes(searchTerm));
                });
            } else {
                // Search term is empty or whitespace, so show all products.
                allCheckboxes.forEach(checkbox => ElementHelper.show(checkbox));
            }
        });

        clearSearchButton.addEventListener("click", clearSearch);
    }

    function clearSearch() {
        searchInput.value = "";
        searchInput.dispatchEvent(new Event('input'));
    }

    function initializeCheckboxes() {
        listProductViewer.addEventListener("click", event => {
            // Check if the clicked item was inside a checkbox-container.
            const container = event.target.closest(".checkbox-container");
            if (container) {
                checkboxHandler(container);
            }
        });
    };

    function checkboxHandler(checkboxContainer) {
        // Read the checked state from from the data-is-checked attribute. Parse to a bool for easier processing.
        let isChecked = JSON.parse(checkboxContainer.dataset.isChecked);
        isChecked = isChecked === false;
        checkboxContainer.dataset.isChecked = isChecked;

        let form = document.getElementById(checkboxContainer.dataset.form);
        let data = new FormData(form);
        data.set("toggleToOn", isChecked);
        let url = form.action;

        fetch(url, {
            method: "POST",
            body: data
        })
            .then(() => Reloader.reload(listPreviewContainer));
    }

    // When a product is added via a modal, reload the product list.
    function initializeProductAddedHandler() {
        document.addEventListener(EventNames.productAdded, () => {
            Reloader.reload(listProductViewer);
            clearSearch();
        });
    }
});