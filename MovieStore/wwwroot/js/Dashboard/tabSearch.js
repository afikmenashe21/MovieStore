
$("#myInput-users").keyup(
    function myFunction() {
        // Declare variables
        var input, filter, ul, li, a, i, txtValue;
        input = $(this);
        filter = $("#myInput-users").val().toLowerCase();
        li = document.getElementsByClassName('myBTN-users');

        // Loop through all list items, and hide those who don't match the search query
        for (i = 0; i < li.length; i++) {
            //a = li[i].getElementsByTagName("a")[0];
            txtValue = li[i].textContent || li[i].innerText;
            if (txtValue.toLowerCase().indexOf(filter) > -1) {
                li[i].style.display = "";
            } else {
                li[i].style.display = "none";
            }
        }
    })

$("#myInput-movies").keyup(
    function myFunction() {
        // Declare variables
        var input, filter, ul, li, a, i, txtValue;
        input = $(this);
        filter = $("#myInput-movies").val().toLowerCase();
        li = document.getElementsByClassName('myBTN-movies');

        // Loop through all list items, and hide those who don't match the search query
        for (i = 0; i < li.length; i++) {
            //a = li[i].getElementsByTagName("a")[0];
            txtValue = li[i].textContent || li[i].innerText;
            if (txtValue.toLowerCase().indexOf(filter) > -1) {
                li[i].style.display = "";
            } else {
                li[i].style.display = "none";
            }
        }
    })

$("#myInput-actors").keyup(
    function myFunction() {
        // Declare variables
        var input, filter, ul, li, a, i, txtValue;
        input = $(this);
        filter = $("#myInput-actors").val().toLowerCase();
        li = document.getElementsByClassName('myBTN-actors');

        // Loop through all list items, and hide those who don't match the search query
        for (i = 0; i < li.length; i++) {
            //a = li[i].getElementsByTagName("a")[0];
            txtValue = li[i].textContent || li[i].innerText;
            if (txtValue.toLowerCase().indexOf(filter) > -1) {
                li[i].style.display = "";
            } else {
                li[i].style.display = "none";
            }
        }
    })


$("#myInput-genres").keyup(
    function myFunction() {
        // Declare variables
        var input, filter, ul, li, a, i, txtValue;
        input = $(this);
        filter = $("#myInput-genres").val().toLowerCase();
        li = document.getElementsByClassName('myBTN-genres');

        // Loop through all list items, and hide those who don't match the search query
        for (i = 0; i < li.length; i++) {
            //a = li[i].getElementsByTagName("a")[0];
            txtValue = li[i].textContent || li[i].innerText;
            if (txtValue.toLowerCase().indexOf(filter) > -1) {
                li[i].style.display = "";
            } else {
                li[i].style.display = "none";
            }
        }
    })