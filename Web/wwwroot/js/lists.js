ready(function () {
    let listProductViewer;
    let clearSearchButton;
    let searchInput;
    let listItemViewer;

    initialize();

    function initialize() {
        const container = document.querySelector(".list-index-container");
        if (container != null) {
            listProductViewer = document.querySelector(".list-product-viewer");
            searchInput = listProductViewer.querySelector("#productSearchInput");
            clearSearchButton = listProductViewer.querySelector("#clearProductSearchButton");
            listItemViewer = document.querySelector(".list-item-viewer");

            initializeSearch();
            initializeCheckboxes();
            initializeProductAddedHandler();
        }
    };

    function initializeSearch() {
        searchInput.addEventListener("input", () => {
            // Show the products that contain the search term, by checking their data-upperName values.
            const searchTerm = searchInput.value.trim().toUpperCase();
            const allCheckboxes = listProductViewer.querySelectorAll(".form-check");
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
        listProductViewer.addEventListener("change", event => {
            if (event.target.matches(".list-product-viewer .form-check-input")) {
                checkboxHandler(event.target)
            }
        });
    };

    function checkboxHandler(checkbox) {
        checkbox.disabled = true;
        let form = document.getElementById(checkbox.dataset.form);
        let data = new FormData(form);
        data.set("toggleToOn", checkbox.checked);
        let url = form.action;

        fetch(url, {
            method: "POST",
            body: data
        })
            .then(() => Reloader.reload(listItemViewer));

        checkbox.disabled = false;
    }

    // When a product is added via a modal, reload the product list.
    function initializeProductAddedHandler() {
        document.addEventListener(EventNames.productAdded, () => {
            Reloader.reload(listProductViewer);
            clearSearch();
        });
    }
});