// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const btnSearch = document.querySelector("#icon-search");
const btnCloseSearch = document.querySelector("#icon-close");
const formSearch = document.querySelector(".form-search");
const inputSearch = document.querySelector("#input-search")

let textSearch;


    btnSearch.addEventListener("click", () => {
        btnSearch.classList.add("display-none");
        btnCloseSearch.classList.remove("display-none")
        formSearch.classList.remove("display-none")
        inputSearch.focus();

    })


    btnCloseSearch.addEventListener("click", () => {
        btnSearch.classList.remove("display-none");
        btnCloseSearch.classList.add("display-none");
        formSearch.classList.add("display-none");
    })


    const debounce = (func, time) => {
    let timeoutId;
    return function (...args) {

        clearTimeout(timeoutId);

        timeoutId = setTimeout(() => {
            func.apply(this, args);
        }, time);

    };
}


const debouncedInputChange = debounce((item) => {
    console.log("text:", item.target.value);
}, 1000);

inputSearch.addEventListener("input", debouncedInputChange);
