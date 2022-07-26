let triggerSideNav = document.getElementById("triggerSideNav")
let sidenav = document.getElementById("sidenav")
let cropQuestionText = document.getElementById("ContentPlaceHolder1_QuestionsDataList_questionTitleText_0");
let cropAnswerText = document.getElementById("ContentPlaceHolder1_AnswersDataList_answerTitleText_0");
document.addEventListener('DOMContentLoaded', function () {

    triggerSideNav.addEventListener("click", (event) => {
        event.stopPropagation();
        sidenav.classList.toggle("display")


    })

    document.addEventListener("click", function () {
        sidenav.classList.remove("display");

    });
    function truncate(str, n) {
        return str.length > n ? str.substr(0, n - 1) + '...' : str;
    }

    if (cropQuestionText != null) {

        cropQuestionText.innerText = truncate(cropQuestionText.innerText, 20)
    }
    if (cropAnswerText != null) {

        cropAnswerText.innerText = truncate(cropAnswerText.innerText, 20)
    }
    
});


