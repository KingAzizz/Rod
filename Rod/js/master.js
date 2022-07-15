let dropdownContent = document.querySelector(".dropdownContent");
let profileMenu = document.getElementById("profileMenu");
let searchTextBox = document.getElementById("searchText")
searchInput.addEventListener("click", (event) => {
    event.stopPropagation();
    
    searchTextBox.classList.toggle("showTextBox")
})
profileMenu.addEventListener("click", (event) => {
    event.stopPropagation();
    dropdownContent.classList.toggle("drop")
    
});
document.addEventListener("click", function () {
    searchTextBox.classList.remove("showTextBox");
    dropdownContent.classList.remove("drop")
});