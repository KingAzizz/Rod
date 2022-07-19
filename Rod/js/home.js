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