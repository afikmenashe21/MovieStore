$(function () {
    $("#searchBox").autocomplete({
        source: "/Movies/DynamicSearch",
        minLength: 2,
        select: function (event, ui) {
            location.href = '/Movies/Details/' + ui.item.id;
        }
        , messages: {
            noResults: 'no results',
            results: function (amount) {
                return amount + 'results.'
            }
        }
    });
});