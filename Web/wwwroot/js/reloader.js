class Reloader {

    static reload(container) {
        const elements = container.querySelectorAll(".reloadable");
        elements.forEach(element => {
            const url = element.dataset.reloadUrl;
            const method = element.dataset.reloadMethod;
            if (method === null) {
                method = "GET";
            }

            fetch(url, {
                method: method
            })
                .then((response) => response.text())
                .then((content) => element.innerHTML = content);
        });
    }
}