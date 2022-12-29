ready(function () {
    let listProductViewer;
    let listItemViewer;

    initialize();

    function initialize() {
        const container = document.querySelector(".list-index-container");
        if (container != null) {
            listProductViewer = document.querySelector(".list-product-viewer");
            listItemViewer = document.querySelector(".list-item-viewer");

            initializeCheckboxes();
            initializeProductAddedHandler();
        }
    };

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
        document.addEventListener(EventNames.productAdded, () => Reloader.reload(listProductViewer));
    }
});