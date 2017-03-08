$(function(){
    $('.carousel').carousel();

    $('a.left').click(function(){
        $('#photo-carousel').carousel('prev');
    })

    $('a.right').click(function(){
        $('#photo-carousel').carousel('next');
    })

    $(window).scroll(function () {
        //if you hard code, then use console
        //.log to determine when you want the
        //nav bar to stick.
        console.log($(window).scrollTop())
        if ($(window).scrollTop() > 230) {
            $('#my-nav').addClass('navbar-fixed-top');
        }
        if ($(window).scrollTop() < 230) {
            $('#my-nav').removeClass('navbar-fixed-top');
        }
    });

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
