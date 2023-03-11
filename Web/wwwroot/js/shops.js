ready(function () {
    const productAddedEventName = EventNames.productAdded;

    initialize();

    function initialize() {
        const containers = document.querySelectorAll(".shop-container");
        containers.forEach((container) => {
            // When a "productAdded" event is raised, refresh the product list via a content-loader.
            container.addEventListener(productAddedEventName, () => refreshProducts(container));

            // Refresh the products immediately to ensure that the product lists are correct upon page load.
            refreshProducts(container);

            // Wire up "delete product" functionality.
            initializeDeleteProductModal(container);
        });

        // Wire up "register product" functionality.
        // This is is used in various parts of the application, so don't wire up inside the ".shop-container" initialization.
        const registerProductModals = document.querySelectorAll(".register-product-modal");
        registerProductModals.forEach(modal => {
            const submitButton = modal.querySelector("button[type=submit]");
            submitButton.addEventListener("click", () => registerNewProduct(modal, submitButton));
        });
    };

    function refreshProducts(container) {
        const refreshButtons = container.querySelectorAll(".refresh-products");
        refreshButtons.forEach((button) => button.click());
    }

    function registerNewProduct(container, submitButton) {
        const newProductName = container.querySelector("input[type=text]").value;
        const addToStart = container.querySelector("input[type=checkbox]").checked;

        let nextProductName;
        if (addToStart) {
            // The new product needs to be added to the start.
            // The product currently at the start is the new products "next" product.
            const firstProduct = container.querySelector(".product-options option");
            nextProductName = firstProduct.value;
        } else {
            // Get the "next" product by inspecting the data- attribute of the selected option.
            const option = container.querySelector(".product-options").selectedOptions[0];
            nextProductName = option.dataset.next;
        }

        const shopName = submitButton.dataset.shopName;
        const data = { shopName, newProductName, nextProductName };
        const url = submitButton.dataset.url;

        fetch(url, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(data),
        })
            .then(() => productRegistered(container));
    }

    function productRegistered(container) {
        const event = new CustomEvent(productAddedEventName, { bubbles: true });
        container.dispatchEvent(event);

        // Clear the text input, so that it's empty when the modal opens again in future.
        const input = container.querySelector("input[type=text]");
        clearRequiredElement(input, true);

        const modal = bootstrap.Modal.getInstance(container);
        modal.hide();
    }

    function initializeDeleteProductModal(container) {
        container.addEventListener("show.bs.modal", event => {
            if (event.target.matches(".delete-product-modal")) {
                let product = event.relatedTarget.closest(".product");
                let productName = product.dataset.productName;
                let modal = event.target;
                // Populate the placeholder.
                modal.querySelector(".product-placeholder").innerText = productName;
                // Set the hidden form input.
                let input = modal.querySelector("input[name=productName]");
                input.value = productName;
            };
        });

        container.addEventListener("click", event => {
            if (event.target.matches(".delete-product-modal button[type=submit]")) {
                const modal = event.target.closest(".delete-product-modal"); 
                const form = modal.querySelector("form");
                const data = new FormData(form);

                fetch(form.action, {
                    method: "POST",
                    body: data
                })
                    .then(() => {
                        refreshProducts(container);
                        bootstrap.Modal.getInstance(modal).hide();
                    });
            };
        });
    };
});