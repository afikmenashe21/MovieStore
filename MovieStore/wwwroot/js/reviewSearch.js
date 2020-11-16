$("#fromYears, #toYears, #user, #content").keyup(function () {

    var li, i, userName, changedText, invokedElement, reviewContent, fromYearAsString, toYearAsString, fromYear, toYear;

    invokedElement = $(this).get(0).id;

    changedText = $(this).val(); //The inserted value by user

    li = document.getElementsByClassName('media'); //List of the reviews

    // Loop through all list items, and hide those who don't match the search query
    for (i = 0; i < li.length; i++) {

        switch (invokedElement) {
            case 'fromYears':
                /*Get the published time of the review*/
                fromYearAsString = li[i].children[2].children[0].textContent.replace('Published: ', '');
                fromYearAsString = fromYearAsString.slice(6, 10);
                fromYear = Number.parseInt(fromYearAsString);
                changedText = Number.parseInt(changedText);//Convert the user text to number
                /*Verify that the number inserted is year*/
                if ($(this).val().length === 4) {
                    if (fromYear >= changedText || isNaN(changedText)) {
                        li[i].style.display = "";
                    } else {
                        li[i].style.display = "none";
                    }
                } else {
                    //In case the number is XYZ then display all reviews
                    li[i].style.display = "";
                }
                break;

            case 'toYears':
                toYearAsString = li[i].children[2].children[0].textContent.replace('Published: ', '');//Get the author name
                toYearAsString = toYearAsString.slice(6, 10);
                toYear = Number.parseInt(toYearAsString);
                changedText = Number.parseInt(changedText);
                if ($(this).val().length === 4) {
                    if (changedText >= toYear || isNaN(changedText)) {
                        li[i].style.display = "";
                    } else {
                        li[i].style.display = "none";
                    }
                } else {
                    li[i].style.display = "";
                }
                break;

            case 'user':
                userName = li[i].children[4].textContent.replace('Written by: ', '');//Get the author name
                if (userName.includes(changedText)) {
                    li[i].style.display = "";
                } else {
                    li[i].style.display = "none";
                }
                break;

            case 'content':
                reviewContent = li[i].children[2].children[1].textContent;//Get the review content
                if (reviewContent.includes(changedText)) {
                    li[i].style.display = "";
                } else {
                    li[i].style.display = "none";
                }
                break;

        }
    }
});
