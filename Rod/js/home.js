let triggerSideNav = document.getElementById("triggerSideNav")
let sidenav = document.getElementById("ContentPlaceHolder1_sidenav")
console.log('%c Developed By: Abdulaziz Fahad Alsunaydi ', 'background: #222; color: #bada55;font-size:15px;');

document.addEventListener('DOMContentLoaded', function () {
    
    triggerSideNav.addEventListener("click", (event) => {
        event.stopPropagation();
        sidenav.classList.toggle("display")
        

    })
});

document.addEventListener("click", function () {
    sidenav.classList.remove("display");
    
});