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
            checkbox.addEventListener("change", function() {
                let form = document.getElementById(this.dataset.form);
                let data = new FormData(form);
                data.set("toggleToOn", this.checked);
                let url = form.action;

                fetch(url, {
                    method: "POST",
                    body: data
                })
                    .then(() => Reloader.reload(listItemViewer));
            });
        })
    }
});