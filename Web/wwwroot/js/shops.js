ready(function () {
    const productAddedEventName = "productAdded";

    initialize();

    function initialize() {
        const containers = document.querySelectorAll(".shop-container");
        containers.forEach((container) => {
            // Wire up modal functionality.
            const modal = container.querySelector(".register-product-modal");
            const submitButton = modal.querySelector("button[type=submit]");
            submitButton.addEventListener("click", () => registerNewProduct(modal, submitButton));

            // When a "productAdded" event is raised, refresh the product list via a content-loader.
            container.addEventListener(productAddedEventName, () => refreshProducts(container));

            refreshProducts(container);
        });

        function refreshProducts(container) {
            const refreshButtons = container.querySelectorAll(".refresh-products");
            refreshButtons.forEach((button) => button.click());
        }
    };

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

        container.querySelector("input[type=text]").value = "";

        const modal = bootstrap.Modal.getInstance(container);
        modal.hide();
    }
});