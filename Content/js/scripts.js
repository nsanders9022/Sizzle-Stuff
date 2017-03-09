$(function(){
    $('.carousel').carousel();
    $("#product-table").tablesorter();
    $('.collapse').collapse();

// Button hide/showing for the managers
    $(".manager-heading").click(function() {
        $(this).next().toggle();
    });

// Button hide/showing on the admin products page
    $(".admin-edit-button").click(function() {
        $(this).parent().parent().next().show();
        $(this).parent().parent().hide();
    });
    $(".cancel-edits").click(function() {
        $(this).parent().parent().prev().show();
        $(this).parent().parent().hide();
    });

// Button hide/showing on the singular admin products page
    $(".single-edit-button").click(function() {
        $(this).hide();
        $(".edit-values-product").show();
        $(".single-cancel-button").show();
    });
    $(".single-cancel-button").click(function() {
        $(this).hide();
        $(".edit-values-product").hide();
        $(".single-edit-button").show();
    });
});
