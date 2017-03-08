$(function(){
    $('.carousel').carousel();

    $('a.left').click(function(){
         $('#photo-carousel').carousel('prev');
    })

    $('a.right').click(function(){
         $('#photo-carousel').carousel('next');
    })

});
