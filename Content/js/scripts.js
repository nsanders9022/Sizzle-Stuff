


$(function(){
    $('.carousel').carousel();
    $("#product-table").tablesorter();
    $('.collapse').collapse()

    $(".admin-edit-button").click(function() {
        $(this).parent().parent().next().show();
        $(this).parent().parent().hide();

        console.log("Clicked it!");
    });

    $(".cancel-edits").click(function() {
        $(this).parent().parent().prev().show();
        $(this).parent().parent().hide();
    });
});
