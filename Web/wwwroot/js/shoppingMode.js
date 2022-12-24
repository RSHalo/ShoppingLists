ready(function () {
    const container = document.querySelector(".shopping-mode");
    if (container != null) {
        initializePickButtons();
    }

    function initializePickButtons() {
        const buttons = document.querySelectorAll(".toggle-picked-button");
        buttons.forEach(button => {
            button.addEventListener("click", function () {
                const form = button.closest(".shopping-mode-items").querySelector("form");
                const data = new FormData(form);
                data.set("itemName", button.dataset.itemName);
                const url = form.action;

                fetch(url, {
                    method: "POST",
                    body: data
                })
                    .then(() => console.log("done it"));
            });
        });
    }
})