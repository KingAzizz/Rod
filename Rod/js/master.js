let dropdownContent = document.querySelector(".dropdownContent");
let profileMenu = document.getElementById("profileMenu");
let searchTextBox = document.getElementById("searchText")
searchInput.addEventListener("click", (event) => {
    event.stopPropagation();
    
    searchTextBox.classList.toggle("showTextBox")
    window.setTimeout(() => searchTextBox.focus(), 100);
})

if (profileMenu != null) {

profileMenu.addEventListener("click", (event) => {
    event.stopPropagation();
    dropdownContent.classList.toggle("drop")
    
});
}
document.addEventListener("click", function () {
    searchTextBox.classList.remove("showTextBox");
    if (profileMenu != null) {

    dropdownContent.classList.remove("drop")
    }
});