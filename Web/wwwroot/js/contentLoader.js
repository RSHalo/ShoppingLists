ready(function () {
    const loaders = document.querySelectorAll(".content-loader")

    loaders.forEach((loader) => {
        loader.addEventListener("click", () => load(loader));
    });

    function load(loader) {
        const url = loader.dataset.loadUrl;
        const targetSelector = loader.dataset.loadTarget;
        const target = document.querySelector(targetSelector);

        fetch(url)
            .then((response) => response.text())
            .then((content) => target.innerHTML = content);
    }
});