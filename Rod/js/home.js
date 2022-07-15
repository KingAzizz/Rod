let triggerSideNav = document.getElementById("triggerSideNav")
let sidenav = document.getElementById("ContentPlaceHolder1_sidenav")


document.addEventListener('DOMContentLoaded', function () {
    
    triggerSideNav.addEventListener("click", () => {
       
        sidenav.classList.toggle("display")
        

    })
});