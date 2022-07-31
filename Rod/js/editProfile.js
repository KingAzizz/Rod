let uploadImageButton = document.getElementById("uploadImageLabel");
let inputFile = document.getElementById("ContentPlaceHolder1_imageUpload")

console.log("up and running");

uploadImageButton.addEventListener("click", () => {
    inputFile.click();
    console.log(inputFile.type);
})


let triggerSideNav = document.getElementById("triggerSideNav")
let sidenav = document.getElementById("ContentPlaceHolder1_sidenav")


document.addEventListener('DOMContentLoaded', function () {

    triggerSideNav.addEventListener("click", (event) => {
        event.stopPropagation();
        sidenav.classList.toggle("display")


    })
});

document.addEventListener("click", function () {
    sidenav.classList.remove("display");

});