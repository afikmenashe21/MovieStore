

    var jobCount = $('#list-tabs').children('.li-tabs').length;
    $('.list-count-tabs').text(jobCount + ' items');


    $("#search-text-tabs").keyup(function () {
        //$(this).addClass('hidden');

        var searchTerm = $("#search-text-tabs").val();
        var listItem = $('#list-tabs').children('.li-tabs');


        var searchSplit = searchTerm.replace(/ /g, "'):containsi('")

        //extends :contains to be case insensitive
        $.extend($.expr[':'], {
            'containsi': function (elem, i, match, array) {
                return (elem.textContent || elem.innerText || '').toLowerCase()
                    .indexOf((match[3] || "").toLowerCase()) >= 0;
            }
        });


        $("#list .li-tabs").not(":containsi('" + searchSplit + "')").each(function (e) {
            $(this).addClass('hiding-tabs out').removeClass('in');
            setTimeout(function () {
                $('.out').addClass('hidden-tabs');
            }, 300);
        });

        $("#list .li-tabs:containsi('" + searchSplit + "')").each(function (e) {
            $(this).removeClass('hidden-tabs out').addClass('in');
            setTimeout(function () {
                $('.in').removeClass('hiding-tabs');
            }, 1);
        });


        var jobCount = $('#list.in').length;
        $('.list-count-tabs').text(jobCount + ' items');

        //shows empty state text when no jobs found
        if (jobCount == '0') {
            $('#list').addClass('empty-tabs');
        }
        else {
            $('#list').removeClass('empty-tabs');
        }

    });



    /*  
    An extra! This function implements
    jQuery autocomplete in the searchbox.
    Uncomment to use :)
    
    
function searchList() {                
   //array of list items
   var listArray = [];
 
    $("#list li").each(function() {
   var listText = $(this).text().trim();
     listArray.push(listText);
   });
   
   $('#search-text').autocomplete({
       source: listArray
   });
   
   
 }
                                  
 searchList();
*/








