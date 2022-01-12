const UPLOAD_BUTTON = document.getElementById("upload-button");
const FILE_INPUT = document.querySelector("input[type=file]");
const AVATAR = document.getElementById("avatar");

UPLOAD_BUTTON.addEventListener("click", () => FILE_INPUT.click());

FILE_INPUT.addEventListener("change", event => {
    const file = event.target.files[0];

    const reader = new FileReader();
    reader.readAsDataURL(file);

    reader.onloadend = () => {
        AVATAR.setAttribute("aria-label", file.name);
        AVATAR.style.background = `url(${reader.result}) center center/cover`;
    };
});