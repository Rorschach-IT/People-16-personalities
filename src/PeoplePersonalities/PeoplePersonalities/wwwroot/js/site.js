// External method for disabling select
function disableSelect() {
    document.getElementById("filterForm").addEventListener("submit", function () {
        const select = document.getElementById("typeSelect");

        if (!select.value || select.value.trim() === "") {
            select.disabled = true;
        }
    });
}

/* 
   Validation methods for formula
*/
