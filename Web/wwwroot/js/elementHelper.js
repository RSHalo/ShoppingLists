class ElementHelper {

    static show(element) {
        element.classList.remove("d-none");
    }

    static hide(element) {
        element.classList.add("d-none");
    }

    static toggle(element, predicate) {
        if (predicate()) {
            this.show(element);
        } else {
            this.hide(element);
        }
    }
}