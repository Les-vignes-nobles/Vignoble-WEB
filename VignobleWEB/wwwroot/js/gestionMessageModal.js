function openModal() {
    $('#showmodalMessage').modal('show');
}

function closeModal() {
    document.querySelector("#showmodalMessage").remove();
    document.querySelector(".modal-backdrop.show").remove();

    document.querySelector("body").removeAttribute("style");
    document.querySelector("body").removeAttribute("class");
}

document.addEventListener("DOMContentLoaded", function (event) {
    if (document.getElementById("btnCloseModal") != null) {
        openModal();

        document.getElementById("btnCloseModal").addEventListener("click", function (event) {
            closeModal();
        });
    }    
});