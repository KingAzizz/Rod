let uploadImageButton = document.getElementById("uploadImageLabel");
let inputFile = document.getElementById("ContentPlaceHolder1_imageUpload")

console.log('%c Developed By: Abdulaziz Fahad Alsunaydi ', 'background: #222; color: #bada55; font-size:15px;');

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