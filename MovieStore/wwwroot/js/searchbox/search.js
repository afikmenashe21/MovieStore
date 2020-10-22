$(function () { // Main search box
    $("#searchBox").autocomplete({
        source: "/Movies/DynamicSearch",
        minLength: 2,
        select: function (event, ui) {
            location.href = '/Movies/Details/' + ui.item.id;
        }
    });
    $(".ui-autocomplete").addClass("search-results scroll-hide");
});

//         <------------------------ Advanced Search ------------------------>


$("#clickme").click(function () { // dropdown - with the sliders
    $("#book").slideToggle("slow", function () {
        // Animation complete.
    });
});
jQuery(document).on('click', '.mega-dropdown', function (e) {
    e.stopPropagation()
})
var acb = [];

$(function () { // Release date slider - filter by yeares
    $("#release-date").slider({
        range: true,
        min: 1980,
        max: 2021,
        values: [1980, 2021],
        slide: function (event, ui) {
            $("#years").val(ui.values[0] + " - " + ui.values[1]);
            acb[0] = ui.values[0];
            acb[1] = ui.values[1];
        }
    });
    $("#years").val($("#release-date").slider("values", 0) +
        " - " + $("#release-date").slider("values", 1));
    //$("#release-date").click($("#advanced-search").val("True"));
    
});

$(function () { // Duration slider - filter by minutes
    $("#duration").slider({
        range: true,
        min: 0,
        max: 240,
        values: [0, 240],
        slide: function (event, ui) {
            $("#minutes").val(ui.values[0] + " - " + ui.values[1]);
        }
    });
    $("#minutes").val($("#duration").slider("values", 0) +
        " - " + $("#duration").slider("values", 1));
});

$(function () { // Rating slider - filter by grade 0-10
    $("#rating").slider({
        range: true,
        min: 0,
        max: 10,
        values: [0, 10],
        slide: function (event, ui) {
            $("#rating-grade").val(ui.values[0] + " - " + ui.values[1]);
        }
    });
    $("#rating-grade").val($("#rating").slider("values", 0) +
        " - " + $("#rating").slider("values", 1));
});


$("#reset-filters").click(function () { // Event listener - Reset button

        // < --------- reset the release-date slider --------->
    $("#release-date").slider('values', 0, 1980); 
    $("#release-date").slider('values', 1, 2021);

    $("#years").val($("#release-date").slider("values", 0) +
        " - " + $("#release-date").slider("values", 1));

    // < --------- reset the duration slider --------->
    $("#duration").slider('values', 0, 0); 
    $("#duration").slider('values', 1, 240);

    $("#minutes").val($("#duration").slider("values", 0) +
        " - " + $("#duration").slider("values", 1));

    // < --------- reset the rating slider --------->
    $("#rating").slider('values', 0, 0); 
    $("#rating").slider('values', 1, 10);

    $("#rating-grade").val($("#rating").slider("values", 0) +
        " - " + $("#rating").slider("values", 1));
})