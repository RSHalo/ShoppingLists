ready(function () {
    const container = document.querySelector(".shopping-mode");
    if (container != null) {
        initialize();
    }

    function initialize() {
        container.addEventListener("click", (event) => {
            if (event.target.matches(".toggle-picked-button")) {
                pickButtonHandler(event.target);
            }
        });
    }

    function pickButtonHandler(button) {
        const form = button.closest(".shopping-mode-items").querySelector("form");
        const data = new FormData(form);
        data.set("itemName", button.dataset.itemName);
        const url = form.action;

        fetch(url, {
            method: "POST",
            body: data
        })
            .then(() => {
                console.log("done it");
                Reloader.reload(container);
            });
    };
})