$(() => {

    $(document).ajaxStart(function () {
        $("#qloader").removeClass("d-none");
    }).ajaxStop(function () {
        $("#qloader").addClass("d-none");
    });

    //$("#qloader").hide();
    //$(document).ajaxStart(function () {
    //    $("#qloader").show();
    //}).ajaxStop(function () {
    //    $("#qloader").hide('slow');
    //});
})