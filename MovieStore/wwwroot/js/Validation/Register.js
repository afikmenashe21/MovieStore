$(document).ready(function () {

    $('#register').validate({ // initialize the plugin
        rules: {
            Address: {
                required: true,
                pattren: "^([\w\-\.]+)@(\w+)\.(\w+)$",
                //email: true
            }
        },
        submitHandler: function (form) { // for demo
            alert('valid form submitted'); // for demo
            return false; // for demo
        }
    });

});