$(function () {

    $('#genreDP').click(function () {
        $.ajax({
            url: "/Movies/GetGenresDP",
            success: function (data) {
                $('#genreDP-list').html(data);
                //$('tbody').html('');
                //$('#restuls').tmpl(data).appendTo('tbody');
            }
        });

        })
    })