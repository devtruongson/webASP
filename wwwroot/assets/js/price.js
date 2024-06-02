function price() {
    const iconClose = document.querySelector(".icon-close-modal");
    const modalsUpdate = document.querySelector(".modal-update");
    const btnUpdate = document.querySelectorAll(".button-update");
    const inputUpdate = document.querySelector(".input-update");
    const inputModel = document.querySelector(".input-model");

    btnUpdate.forEach((button, index) => {
        button.addEventListener("click", (event) => {
            modalsUpdate.classList.remove("display-none");
            const row = event.target.closest("tr");
            const inputModel = row.querySelector(".input-model");
            const inputPrice = row.querySelector(".input-price");

            const modelValue = inputModel.value;
            const priceValue = inputPrice.value;

            const inputModelUpdate = document.querySelector(
                ".input-model-update"
            );
            const inputUpdate = document.querySelector(".input-update");
            const textModel = document.querySelector(".text-model");

            textModel.innerHTML = modelValue;
            inputModelUpdate.value = modelValue;
            inputUpdate.value = priceValue;
        });
    });

    //check trạng thái
    // const url = new URL(window.location.href);

    // const params = new URLSearchParams(url.search);

    // if (params.has("changePrice")) {
    //     if (params.get("changePrice") === "true") {
    //         alert("Cập nhật giá xe thành công");
    //         window.location.href = "/price";
    //     }
    //     if (params.get("changePrice") === "false") {
    //         alert("Đã xảy ra lỗi vui lòng thử lại sau !");
    //         window.location.href = "/price";
    //     }
    // }

    // Đóng form update giá
    iconClose.addEventListener("click", () => {
        modalsUpdate.classList.add("display-none");
    });
}

setTimeout(() => {
    price();
}, 1000);
