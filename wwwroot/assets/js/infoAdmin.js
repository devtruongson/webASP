function info() {
    let dataUser = JSON.parse(window.localStorage.getItem("dataUser"));
    if (dataUser && Array.isArray(dataUser)) {
        const inputEmail = document.querySelector(".input-email");
        const inputPassword = document.querySelector(".input-password");
        inputEmail.value = dataUser[0];
        inputPassword.value = dataUser[1];
    }
}

setTimeout(() => {
    info();
}, 500);
