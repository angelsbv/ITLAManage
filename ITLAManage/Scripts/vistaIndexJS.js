document.addEventListener("DOMContentLoaded", () => {
    var rowBtnsUnselected = document.querySelectorAll(".actions")
    document.querySelectorAll("tr")
        .forEach(fila => {
            fila.addEventListener("click", () => {

                rowBtnsUnselected
                    .forEach(btnUnselected => { btnUnselected.setAttribute("style", "display:none;") })

                let rowBtnsSelected = fila.querySelectorAll(".actions")
                rowBtnsSelected
                    .forEach(btn => { btn.setAttribute("style", "") })
            });
        });
});

document.querySelectorAll(".borrar")
    .forEach(b => {
        b.addEventListener("click", (e) => {
            if (!confirm(msg)) {
                e.preventDefault();
            }
        });
    })